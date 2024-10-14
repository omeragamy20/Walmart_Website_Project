using AutoMapper;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.ServicesCategories
{
    public class CategoryServices : ICategoryService
    {
        private readonly ICategoryReposatiry categoryReposatiry;
        private readonly IMapper mapper;
        public CategoryServices(ICategoryReposatiry _categoryReposatiry, IMapper _mapper)
        {
            categoryReposatiry = _categoryReposatiry;
            mapper = _mapper;
        }

        //create new category
        public async Task<ResultView<CreateorUpdatedCategoryDTOs>> CreateCategpryAsync(CreateorUpdatedCategoryDTOs Entity)
        {
            ResultView < CreateorUpdatedCategoryDTOs > result=new ResultView<CreateorUpdatedCategoryDTOs> ();
            try
            {
                bool exist=  (await categoryReposatiry.GetAllAsync()).Any(c=>c.Name_en==Entity.Name_en && c.Name_ar == Entity.Name_ar);
                if (exist)
                {
                    result = new()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "The Category is already Exist in DB"
                    };
                    return result;
                }
                var category = mapper.Map<Category>(Entity);
                var AddedEntity=await categoryReposatiry.CreateAsync(category);
                await categoryReposatiry.SaveChanges();
                var returnedCategory = mapper.Map<CreateorUpdatedCategoryDTOs>(AddedEntity);
                result = new ResultView<CreateorUpdatedCategoryDTOs>()
                {
                    Entity = returnedCategory,
                    IsSuccess = true,
                    Message = "The Category is Created Successfuly"
                };
                return result;
            }
            catch(Exception ex)
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

        // delete category
        public async Task<ResultView<GetAllCategoryDTOs>> DeleteCategoryAsync(int catid)
        {
            ResultView<GetAllCategoryDTOs> result;
            try
            {
                var category = (await categoryReposatiry.GetAllAsync()).FirstOrDefault(cat => cat.Id == catid);
                await categoryReposatiry.DeleteAsync(category);
                await categoryReposatiry.SaveChanges();
                result = new ResultView<GetAllCategoryDTOs>()
                {
                    Entity = null,
                    IsSuccess = true,
                    Message = "Category Deleted Successfuly"
                };
                return result;
            }catch(Exception ex)
            {
                result = new ResultView<GetAllCategoryDTOs>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error: " + ex.Message
                };
                return result;
            }
        }

        // return all gategory in DB
        public async Task<List<CreateorUpdatedCategoryDTOs>> GetAllCategoriesAsync()
        {
            var AllCategories=(await categoryReposatiry.GetAllAsync()).ToList();
            var returnedcategory=mapper.Map<List<CreateorUpdatedCategoryDTOs>>(AllCategories);
            return returnedcategory;
        }

        //return Pagenation of Category based on page number and Count in every page
        public async Task<EntityPaginated<GetAllCategoryDTOs>> GetAllCategoriesPaginatedAsync(int pagenumber, int count)
        {
            var paginatedcategory = (await categoryReposatiry.GetAllAsync()).Skip((pagenumber - 1) * count).Take(count)
                .Select(cat => new GetAllCategoryDTOs
                {
                    Id = cat.Id,
                    Name_ar = cat.Name_ar,
                    Name_en = cat.Name_en,
                }).ToList();
            var countofcategory = (await categoryReposatiry.GetAllAsync()).Count();
            return new EntityPaginated<GetAllCategoryDTOs>()
            {
                Data=paginatedcategory,
                Count=countofcategory
            };
        }

        // return all category that have the name
        public async Task<List<GetAllCategoryDTOs>> SearchByNameAsync(string categoryname)
        {
            var categories = (await categoryReposatiry.GetAllAsync())
                .Where(cat => cat.Name_en.Contains(categoryname) || cat.Name_ar.Contains(categoryname)).ToList();
            var searchresult=mapper.Map<List<GetAllCategoryDTOs>>(categories);
            return searchresult;
        }

        // updated category 
        public async Task<ResultView<CreateorUpdatedCategoryDTOs>> UpdatedCategpryAsync(CreateorUpdatedCategoryDTOs Entity)
        {
            ResultView<CreateorUpdatedCategoryDTOs> result = new ResultView<CreateorUpdatedCategoryDTOs>();
            try
            {
                var category = mapper.Map<Category>(Entity);
                var AddedEntity = await categoryReposatiry.UpdateAsync(category);
                await categoryReposatiry.SaveChanges();

                var returnedCategory = mapper.Map<CreateorUpdatedCategoryDTOs>(AddedEntity);
                result = new ResultView<CreateorUpdatedCategoryDTOs>()
                {
                    Entity = returnedCategory,
                    IsSuccess = true,
                    Message = "The Category is Updated Successfuly"
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
    }
}
