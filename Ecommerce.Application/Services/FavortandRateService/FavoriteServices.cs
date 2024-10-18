using AutoMapper;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Application.Contracts.FavortandRateRepo;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.DTOsFavoritandRate;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.FavortandRateService
{
    public class FavoriteServices : IFavoriteServices
    {
        private readonly IFavoriteRepository favoriterepository;
        private readonly IMapper mapper;
        public FavoriteServices(IFavoriteRepository _favoriterepository, IMapper _mapper)
        {
            favoriterepository = _favoriterepository;
            mapper = _mapper;
        }
        public async Task<ResultView<CreateorUpdateFavoritDTO>> AddToFavoritAsync(CreateorUpdateFavoritDTO Entity)
        {
            ResultView<CreateorUpdateFavoritDTO> result = new ResultView<CreateorUpdateFavoritDTO>();
            try
            {
                var favorit = mapper.Map<Favorite>(Entity);
                var AddedEntity = await favoriterepository.CreateAsync(favorit);
                await favoriterepository.SaveChanges();
                var returnedfavort = mapper.Map<CreateorUpdateFavoritDTO>(AddedEntity);
                result = new ResultView<CreateorUpdateFavoritDTO>()
                {
                    Entity = returnedfavort,
                    IsSuccess = true,
                    Message = "You Add new Fivorit Successfuly"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error HAppen" + ex.Message
                };
                return result;
            }
        }
    
        public async Task<ResultView<CreateorUpdateFavoritDTO>> DeleteFavoritAsync(CreateorUpdateFavoritDTO Entity)
        {
            
            ResultView<CreateorUpdateFavoritDTO> result;
            try
            {
                var deltedfavorit = mapper.Map<Favorite>(Entity);
                await favoriterepository.DeleteAsync(deltedfavorit);
                await favoriterepository.SaveChanges();
                result = new ResultView<CreateorUpdateFavoritDTO>()
                {
                    Entity = null,
                    IsSuccess = true,
                    Message = "The Favorite Deleted Successfuly"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<CreateorUpdateFavoritDTO>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error: " + ex.Message
                };
                return result;
            }
        }
        
        // list of favorit for user
        public async Task<List<GetAllFavoritDTO>> AllFavoritbycustomer(string userid)
        {
            var userfavorit= (await favoriterepository.GetAllAsync())
                            .Where(f=>f.CustomerId==userid)
                            .Select(u => new GetAllFavoritDTO
                            {
                                Id = u.Id,
                                CustomerId = u.CustomerId,
                                ProductId = u.ProductId,
                                ProductName_ar = u.Product.Title_ar,
                                ProductName_en = u.Product.Title_en,
                                ProductDescription_en = u.Product.Description_en,
                                ProductDescription_ar = u.Product.Description_ar,
                                Productprice = u.Product.Price
                            }).ToList();
            //.Include(p=>p.Product).ToList();

            //var returnedfavoritlist = mapper.Map<List<GetAllFavoritDTO>>(userfavorit);
            return userfavorit;
        }
        public async Task<List<GetAllFavoritDTO>> ThemostFavoritproducts()
        {
            var mostfavoeitproduct = (await favoriterepository.GetAllAsync())
                                .Select(p=>new GetAllFavoritDTO
                                {
                                    ProductName_en=p.Product.Title_en,
                                    ProductName_ar=p.Product.Title_ar,
                                    ProductDescription_en=p.Product.Description_en,
                                    ProductDescription_ar=p.Product.Description_ar,
                                    Productprice = p.Product.Price
                                }).Distinct().ToList();
            return mostfavoeitproduct;
        }
    }
}
