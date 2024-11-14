using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository productRebository;
        private readonly IProductSubCategoryRepository prdctsubCatRepository;
        private readonly IMapper mapper;
        private readonly IOrderItemsReposatiry orderItemsReposatiry;
        public ProductService(IProductRepository _productRepository, IMapper _mapper,
            IProductSubCategoryRepository _prdctsubCatRepository, IOrderItemsReposatiry _orderItemsReposatiry)
        {
            
            productRebository = _productRepository;
            mapper = _mapper;
            prdctsubCatRepository = _prdctsubCatRepository;
            orderItemsReposatiry = _orderItemsReposatiry;
        }
        public async Task<ResultView<CreateAndUpdateProductDTO>> CreateAsync(CreateAndUpdateProductDTO entity)
        {
            ResultView<CreateAndUpdateProductDTO> result = new ResultView<CreateAndUpdateProductDTO>();
            try
            {
                bool Exist = (await productRebository.GetAllAsync()).Any(p => p.Title_en == entity.Title_en || p.Title_ar == entity.Title_ar);
                if (Exist)
                {
                    result = new()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "This Product Already Exist"
                    };
                    return result;
                }
                else
                {
                    var product = mapper.Map<Product>(entity);

                    //product.ProductFacilities = entity.Facilities.Select(facility => new ProductFacility
                    //{
                    //    Value_en=facility
                    //}).ToList();
                    //product.productSubCategory = entity.SubCategoryIds?.Select(id => new ProductSubCategory
                    //{
                    //    SubcategoryId = id
                    //}).ToList();
                    var success = (await productRebository.CreateAsync(product));
                    await productRebository.SaveChanges();
                    for(int i = 0; i < entity.SubCategoryIds?.Count; i++)
                    {
                        ProductSubCategory PS=new ProductSubCategory()
                        {
                            ProductId=success.Id,
                            SubcategoryId = entity.SubCategoryIds[i],
                        };
                        await prdctsubCatRepository.CreateAsync(PS);
                        await prdctsubCatRepository.SaveChanges();
                    }
                    var returnProduct = mapper.Map<CreateAndUpdateProductDTO>(success);
                    var msg = "Created Successfully";
                    result = new ResultView<CreateAndUpdateProductDTO>()
                    {
                        IsSuccess = true,
                        Message = msg,
                        Entity = returnProduct
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }
        }
        public async Task<ResultView<GetAllproductDTO>> DeleteAsync(int id)
        {
            ResultView<GetAllproductDTO> result;
            try
            {
                var oldone = (await productRebository.GetOneAsync(id));
                var allorder = await orderItemsReposatiry.GetAllAsync();
                var order=await allorder.AnyAsync(o => o.ProductId == id);
                if (order)
                {
                    result = new ResultView<GetAllproductDTO>()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "Can't delete it, there is related Order"
                    };
                    return result;

                }
                await productRebository.DeleteAsync(oldone);
                await productRebository.SaveChanges();

                result = new ResultView<GetAllproductDTO>()
                {
                    Entity = null,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
                return result;
            }
          
            catch (Exception ex)
            {
                result = new ResultView<GetAllproductDTO>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }          
        }
        public async Task<List<GetAllproductEnDTO>> GetAllEnAsync()
        {
            var data = (await productRebository.GetAllAsync())
                .Include(i => i.Images)
                .Include(s => s.productSubCategory); 

            var products = mapper.Map<List<GetAllproductEnDTO>>(data);
            return products;
        }

        public async Task<EntityPaginated<GetAllproductDTO>> GetAllPaginationAsync(int Subcatid,int PageNumber, int Count, string? searchTerm, decimal? price)
        {
            var data = (await prdctsubCatRepository.GetAllAsync()).Where(sc => sc.SubcategoryId == Subcatid)
                .Select(p => new GetAllproductDTO
                {
                    Id = p.Product.Id,
                    Description_ar = p.Product.Description_ar,
                    Description_en = p.Product.Description_en,
                    Price = p.Product.Price,
                    Stock = p.Product.Stock,
                    Title_ar = p.Product.Title_ar,
                    Title_en = p.Product.Title_en,
                    Rate = p.Product.Rates.Average(r => r.Rating),
                    TotalRate = p.Product.Rates.Count(),
                    SubCategoryNames = p.Product.productSubCategory.Select(p => p.SubCategory.Name_en).ToList(),
                    SubCategoryNamesAr = p.Product.productSubCategory.Select(p => p.SubCategory.Name_ar).ToList(),
                    ImageUrls = p.Product.Images.Select(i => i.Image).ToList(),
                    Facilities = p.Product.ProductFacilities.Select(f => f.Value_en).ToList(),
                    Facilities_Ar = p.Product.ProductFacilities.Select(f => f.Value_ar).ToList(),
                    Values = p.Product.ProductFacilities.Select(f => f.facility.Name_en).ToList(),
                    Values_Ar = p.Product.ProductFacilities.Select(f => f.facility.Name_ar).ToList(),
                }).Where(p=>p.Id!=null).ToList();          
            var c = (await prdctsubCatRepository.GetAllAsync()).Where(sc => sc.SubcategoryId == Subcatid).Count();
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        data = data.Where(p => p.Title_en.Contains(searchTerm) ||
                                                             p.Title_ar.Contains(searchTerm) ||
                                                             p.Description_en.Contains(searchTerm) ||
                                                             p.Description_ar.Contains(searchTerm) ||
                                                             p.Facilities.Any(f => f.Contains(searchTerm)) ||
                                                             p.Facilities_Ar.Any(f => f.Contains(searchTerm))).ToList();
                    }          
             c = data.Count();
            var paginatedProducts = data.Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            EntityPaginated<GetAllproductDTO> GetAllResult = new()
            {
                Data = paginatedProducts,
                Count = c


            };
            return GetAllResult;

        }
        public async Task<CreateAndUpdateProductDTO> GetById(int id)
        {

            var data = (await productRebository.GetOneAsync(id));
            var pro = new CreateAndUpdateProductDTO
            {
                Id = data.Id,
                Description_ar = data.Description_ar,
                Description_en = data.Description_en,
                Price = data.Price,
                Stock = data.Stock,
                Title_ar = data.Title_ar,
                Title_en = data.Title_en,
                ImagesUrl = data.Images?.Select(i => i.Image).ToList(),
                Facilities = data.ProductFacilities?.Select(f => f.Value_en).ToList(),
                Facilities_Ar = data.ProductFacilities?.Select(f => f.Value_ar).ToList(),
                  };

            return pro;
        }
        public async Task<List<GetAllproductDTO>> GetByIdApi(int id)
        { 
            var data = (await productRebository.GetAllAsync())
                .Select(p => new GetAllproductDTO
            {
                    Id = p.Id,
                    Description_ar = p.Description_ar,
                    Description_en = p.Description_en,
                    Price = p.Price,
                    Stock = p.Stock,
                    Title_ar = p.Title_ar,
                    Title_en = p.Title_en,
                    Rate = p.Rates.Average(r => r.Rating),
                    TotalRate = p.Rates.Count(),
                    Rates=p.Rates.Select(r=>r.Rating).ToList(),
                    ImageUrls = p.Images.Select(i => i.Image).ToList(),
                    Facilities = p.ProductFacilities.Select(f => f.Value_en).ToList(),
                    Facilities_Ar = p.ProductFacilities.Select(f => f.Value_ar).ToList(),
                    Values = p.ProductFacilities.Select(f => f.facility.Name_en).ToList(),
                    Values_Ar = p.ProductFacilities.Select(f => f.facility.Name_ar).ToList(),
                    }).Where(p =>p.Id==id).ToList();

            return data;   
        }
        public async Task<List<int?>?> GetSubcatbyPrdId(int id)
        {
            var res= (await prdctsubCatRepository.GetAllAsync())
                .Where(p=>p.ProductId==id).Select(psc => psc.SubcategoryId).ToList();
            return res;
        }
        public async Task<EntityPaginated<GetAllproductDTO>> SearchByNameAsync(int PageNumber, int Count, string? ProductName, decimal? price)
        {
            var data = (await productRebository.GetAllAsync())
                .Select(p => new GetAllproductDTO
                {
                    Id = p.Id,
                    Description_ar = p.Description_ar,
                    Description_en = p.Description_en,
                    Price = p.Price,
                    Stock = p.Stock,
                    Title_ar = p.Title_ar,
                    Title_en = p.Title_en,
                    Rate = p.Rates.Average(r => r.Rating),
                    TotalRate = p.Rates.Count(),
                    Rates = p.Rates.Select(r => r.Rating).ToList(),
                    ImageUrls = p.Images.Select(i => i.Image).ToList(),
                    Facilities = p.ProductFacilities.Select(f => f.Value_en).ToList(),
                    Facilities_Ar = p.ProductFacilities.Select(f => f.Value_ar).ToList(),
                    Values = p.ProductFacilities.Select(f => f.facility.Name_en).ToList(),
                    Values_Ar = p.ProductFacilities.Select(f => f.facility.Name_ar).ToList(),
                    SubCategoryNames = p.productSubCategory.Select(p => p.SubCategory.Name_en).ToList(),
                    SubCategoryNamesAr = p.productSubCategory.Select(p => p.SubCategory.Name_ar).ToList(),
                   
                })
                .Where(p => p.Title_en.Contains(ProductName) ||
                            p.Title_ar.Contains(ProductName) ||
                            p.Description_en.Contains(ProductName) ||
                            p.Description_ar.Contains(ProductName) ||
                            p.Facilities.Any(f => f.Contains(ProductName)) ||
                            p.Facilities_Ar.Any(f => f.Contains(ProductName))||
                            p.Price.Equals(price)
                           )
                .ToList();
            var c = data.Count();
            var paginatedProducts = data.Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            EntityPaginated<GetAllproductDTO> GetAllResult = new()
            {
                Data = paginatedProducts,
                Count = c


            };
            return GetAllResult;

        }
        

        public async Task<ResultView<CreateAndUpdateProductDTO>> UpdateAsync(CreateAndUpdateProductDTO entity)
        {
            ResultView<CreateAndUpdateProductDTO> result = new ResultView<CreateAndUpdateProductDTO>();
            try
            {
                var oldone = (await productRebository.GetAllAsync())
                 .Include(f => f.ProductFacilities)
                 .Include(i => i.Images)
                 .Include(s => s.productSubCategory)
                 .FirstOrDefault(p => p.Id == entity.Id);
                mapper.Map(entity, oldone);
                oldone.productSubCategory.Clear();
                oldone.productSubCategory = entity.SubCategoryIds.Select(id => new ProductSubCategory
                {
                    SubcategoryId = id
                }).ToList();
               
                var updated = await productRebository.UpdateAsync(oldone);
                await productRebository.SaveChanges();
                var success = mapper.Map<CreateAndUpdateProductDTO>(updated);
                var msg = "Updated Successfully";
                result = new()
                {
                    Entity = success,
                    IsSuccess = true,
                    Message = msg
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    Message = "Error " + ex,
                    IsSuccess = false
                };
                return result;
            }
        }

        public async Task<List<GetAllProductArDTO>> GetAllArAsync()
        {

            var data = (await productRebository.GetAllAsync()).Include(f => f.ProductFacilities).Include(i => i.Images); ;

            var products = mapper.Map<List<GetAllProductArDTO>>(data);
            return products;
        }

        public async Task<EntityPaginated<GetAllproductEnDTO>> GetAllPaginationEnAsync(int PageNumber, int Count)
        {
            var data = (await productRebository.GetAllAsync()).Skip(Count * (PageNumber - 1)).Take(Count)
                .Select(p => new GetAllproductEnDTO
                {
                    Id = p.Id,
                    Title_en = p.Title_en
                }).ToList();
            var c = (await productRebository.GetAllAsync()).Count();

            EntityPaginated<GetAllproductEnDTO> GetAllResult = new()
            {
                Data = data,
                Count = c
            };
            return GetAllResult;
        }

        public async Task<ResultView<List<GetAllproductDTO>>> GetPrdBySubCat(int id)
        {
            ResultView<List<GetAllproductDTO>> result = new ResultView<List<GetAllproductDTO>>();
            try
            {
                var data = (await productRebository.GetPrdBySubCat(id));
                if (data == null)
                {
                    result = new()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "There Is No Products"
                    };
                    return result;
                }
                else
                {
                    //var prd=await data.Include(f => f.Facilities).Include(i => i.Images);
                    var products = mapper.Map<List<GetAllproductDTO>>(data);
                    result = new()
                    {
                        Entity = products,
                        IsSuccess = true,
                        Message = "Getting Successfully"
                    };
                    return result;
                }


            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }

        }

        public async Task<EntityPaginated<GetAllproductDTO>> GetAllAsync(int PageNumber, int Count)
       {
            //var data = (await productRebository.GetAllAsync()).Include(i => i.Images)
            //    .Include(ps => ps.productSubCategory).ThenInclude(s => s.SubCategory)
            //    .ThenInclude(sf=>sf.subCatFacility).ThenInclude(f=>f.facility).ToList();

         //   var data = (await productRebository.GetAllAsync()).Include(i => i.Images)
         //.Include(ps => ps.productSubCategory).ThenInclude(s => s.SubCategory)
         //.ThenInclude(sf => sf.subCatFacility).ThenInclude(f => f.facility).ToList();

            var data = (await productRebository.GetAllAsync())
                .Select(p => new GetAllproductDTO
                {
                    Id = p.Id,
                    Description_ar = p.Description_ar,
                    Description_en = p.Description_en,
                    Price = p.Price,
                    Stock = p.Stock,
                    Title_ar = p.Title_ar,
                    Title_en = p.Title_en,
                    SubCategoryNames = p.productSubCategory.Select(p => p.SubCategory.Name_en).ToList(),
                    SubCategoryNamesAr = p.productSubCategory.Select(p => p.SubCategory.Name_ar).ToList(),
                    ImageUrls = p.Images.Select(i => i.Image).ToList(),
                    //Facilities=p.productSubCategory.Select(s=>s.SubCategory.subCatFacility.Select(f=>f.facility.Name_en)).ToList(),

                    Facilities = p.ProductFacilities.Select(f=>f.Value_en).ToList(),
                    Facilities_Ar = p.ProductFacilities.Select(f=>f.Value_ar).ToList()
                });

            // var products = mapper.Map<List<GetAllproductDTO>>(data);
            var products = data.Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            var c = data.Count();
            EntityPaginated<GetAllproductDTO> GetAllResult = new()
            {
                Data = products,
                Count = c,
                CurrentPage = PageNumber,
                PageSize=Count
            };
            return GetAllResult;
        }
        
        public async Task<List<GetAllproductEnDTO>> GetAllProductPaginationEnBySubCatIdAsync(int Subcatid)
        {
            //.Skip(Count * (PageNumber - 1)).Take(Count)
            var result=(await prdctsubCatRepository.GetAllAsync()).Where(sc=>sc.SubcategoryId==Subcatid)
                .Select(p=> new GetAllproductEnDTO
            {
                Id=p.Product.Id,
                Title_en=p.Product.Title_en,
                Description_en=p.Product.Description_en,
                Price = p.Product.Price,
                Stock=p.Product.Stock,
                ImageUrls=p.Product.Images.FirstOrDefault().Image
                
            }).Where(p=>p.Id!=null).ToList();
            return result;
            //if (result != null)
            //{
            //    return result;
            //}
            //else
            //{
            //    return new List<GetAllproductEnDTO>();
            //}
            //return result;
        }
    }
}

