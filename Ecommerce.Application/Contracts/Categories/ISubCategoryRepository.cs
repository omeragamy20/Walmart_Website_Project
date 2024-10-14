using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts.Categories
{
    public interface ISubCategoryRepository:IGenericReposatiry<SubCategory,int>
    {
        public Task<IQueryable<GetAllSubCategoryDTOs>> GetAllSubcategoryAsync();
    }
}
