using Ecommerce.DTOs.DTOsFavoritandRate;
using Ecommerce.DTOs.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.FavortandRateService
{
    public interface IFavoriteServices
    {
        public Task<ResultView<CreateorUpdateFavoritDTO>> AddToFavoritAsync(CreateFavoriteDTOs Entity);
        public Task<ResultView<CreateorUpdateFavoritDTO>> DeleteFavoritAsync(string customerId, int productId);
        public Task<List<GetAllFavoritDTO>> AllFavoritbycustomer(string userid);
        public Task<List<GetAllFavoritDTO>> ThemostFavoritproducts();
        public Task<CreateorUpdateFavoritDTO> GetOneFavoritCustomerProduct(string customerId, int productId);


    }
}
