using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IProductService
    {
         Task<ResultView<CreateAndUpdateProductDTO>> CreateAsync(CreateAndUpdateProductDTO entity);
         Task<ResultView<CreateAndUpdateProductDTO>> UpdateAsync(CreateAndUpdateProductDTO entity);
        Task<EntityPaginated<GetAllproductDTO>> GetAllAsync(int PageNumber, int Count);

         Task<List<GetAllproductEnDTO>> GetAllEnAsync();

         Task<List<GetAllProductArDTO>> GetAllArAsync();
         Task<EntityPaginated<GetAllproductEnDTO>> GetAllPaginationEnAsync(int PageNumber, int Count);
         Task<EntityPaginated<GetAllproductDTO>> GetAllPaginationAsync(int Subcatid,int PageNumber, int Count, string? searchTerm, decimal? price);
         Task<EntityPaginated<GetAllproductDTO>> SearchByNameAsync(int PageNumber, int Count,string? ProductName, decimal? price);
         Task<CreateAndUpdateProductDTO> GetById(int id);
        Task<List<GetAllproductDTO>> GetByIdApi(int id);
        Task<ResultView<GetAllproductDTO>> DeleteAsync(int id);
         Task<ResultView<List<GetAllproductDTO>>> GetPrdBySubCat(int id);
        public Task<List<int?>?> GetSubcatbyPrdId(int id);

        public Task<List<GetAllproductEnDTO>> GetAllProductPaginationEnBySubCatIdAsync(int Subcatid);


    }
}
