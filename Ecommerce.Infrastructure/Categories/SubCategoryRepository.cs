using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Context;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Categories
{
    public class SubCategoryRepository:GenricReposatiry<SubCategory,int>,ISubCategoryRepository
    {
        private readonly EcommerceContext _Context;
        public SubCategoryRepository(EcommerceContext context):base(context)
        {
            _Context = context;
        }
        public Task<IQueryable<GetAllSubCategoryDTOs>> GetAllSubcategoryAsync()
        {
           var allsebcategory= _Context.SubCategories.Select(sc => new GetAllSubCategoryDTOs
            {
                Id = sc.Id,
                Name_ar = sc.Name_ar,
                Name_en = sc.Name_en,
                CategryName_ar = sc.Category!.Name_ar,
                CategryName_en = sc.Category!.Name_en,
                CategoryId=sc.CategoryId,
                Image=sc.Image
            });
            return Task.FromResult(allsebcategory);
        }
    }
}
