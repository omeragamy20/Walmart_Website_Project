﻿using Ecommerce.DTOs.Product;
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
         Task<List<GetAllproductDTO>> GetAllAsync();

         Task<List<GetAllproductEnDTO>> GetAllEnAsync();
         Task<List<GetAllProductArDTO>> GetAllArAsync();
         Task<EntityPaginated<GetAllproductEnDTO>> GetAllPaginationEnAsync(int PageNumber, int Count);
         Task<EntityPaginated<GetAllProductArDTO>> GetAllPaginationArAsync(int PageNumber, int Count);
         Task<List<GetAllproductDTO>> SearchByNameAsync(string ProductName);
         Task<CreateAndUpdateProductDTO> GetById(int id);
         Task DeleteAsync(int id);
         Task<ResultView<List<GetAllproductDTO>>> GetPrdBySubCat(int id);
    }
}
