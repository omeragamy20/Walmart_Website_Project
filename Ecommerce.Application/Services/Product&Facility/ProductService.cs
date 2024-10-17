using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository productRebository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository _productRepository, IMapper _mapper)
        {
            
            productRebository = _productRepository;
            mapper = _mapper;
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
                    
                   
                    product.productSubCategory = entity.SubCategoryIds?.Select(id => new ProductSubCategory
                    {
                        SubcategoryId = id
                    }).ToList();
                    var success = (await productRebository.CreateAsync(product));
                    await productRebository.SaveChanges();
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
        public async Task DeleteAsync(int id)
        {
            var oldone = (await productRebository.GetOneAsync(id));
            await productRebository.DeleteAsync(oldone);
            await productRebository.SaveChanges();
        }
        public async Task<List<GetAllproductEnDTO>> GetAllEnAsync()
        {
            var data = (await productRebository.GetAllAsync())
                .Include(i => i.Images)
                .Include(s => s.productSubCategory); 

            var products = mapper.Map<List<GetAllproductEnDTO>>(data);
            return products;
        }

        public async Task<EntityPaginated<GetAllProductArDTO>> GetAllPaginationArAsync(int PageNumber, int Count)
        {
            var data = (await productRebository.GetAllAsync()).Skip(Count * (PageNumber - 1)).Take(Count)
                .Select(p => new GetAllProductArDTO
                {
                    Id = p.Id,
                    Title_ar = p.Title_ar
                }).ToList();
            var c = (await productRebository.GetAllAsync()).Count();

            EntityPaginated<GetAllProductArDTO> GetAllResult = new()
            {
                Data = data,
                Count = c
            };
            return GetAllResult;
        }

        public async Task<CreateAndUpdateProductDTO> GetById(int id)
        {
               var data = (await productRebository.GetOneAsync(id));
               var products = mapper.Map<CreateAndUpdateProductDTO>(data);
            return products;      
        }

        public async Task<List<GetAllproductDTO>> SearchByNameAsync(string ProductName)
        {
           
                var data = (await productRebository.GetAllAsync())
                    .Include(f => f.ProductFacilities)
                    .Include(i => i.Images)
                    .Include(s => s.productSubCategory)
                    
                    .Where(p => p.Title_en == ProductName || p.Title_ar == ProductName);

                   var product = mapper.Map<List<GetAllproductDTO>>(data);
            return product;
        }

        public async Task<ResultView<CreateAndUpdateProductDTO>> UpdateAsync(CreateAndUpdateProductDTO entity)
        {
            ResultView<CreateAndUpdateProductDTO> result = new ResultView<CreateAndUpdateProductDTO>();
            try
            {
                var oldone = (await productRebository.GetAllAsync())
                 .Include(f => f.ProductFacilities)
                 .Include(i => i.Images)
                 .Include(s=>s.productSubCategory)
                 .FirstOrDefault(p => p.Id == entity.Id);
                mapper.Map(entity, oldone);
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

        public async Task<List<GetAllproductDTO>> GetAllAsync()
       {
            var data = (await productRebository.GetAllAsync()).Include(f => f.ProductFacilities).Include(i => i.Images)
                .Include(ps => ps.productSubCategory).ThenInclude(s => s.SubCategory).ToList();
               
            var products = mapper.Map<List<GetAllproductDTO>>(data);
           
            return products;
        }
    }
}

