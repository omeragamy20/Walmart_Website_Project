using AutoMapper;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.ServicesCategories
{
    public class SubCategoryServices : ISubCategoryServices
    {
        public readonly ISubCategoryRepository subCategoryRepository;
        private readonly IMapper mapper;
        public SubCategoryServices(ISubCategoryRepository _subCategoryRepository, IMapper _mapper)
        {
            subCategoryRepository= _subCategoryRepository;
            mapper= _mapper;
        }
        //Add new subcategory
        public async Task<ResultView<CreateorUpdatedSubCategoryDTOs>> CreateSubCategpryAsync(CreateorUpdatedSubCategoryDTOs Entity)
        {
            ResultView<CreateorUpdatedSubCategoryDTOs> result = new ResultView<CreateorUpdatedSubCategoryDTOs>();
            try
            {
                bool exist = (await subCategoryRepository.GetAllAsync()).Any(c => c.Name_en == Entity.Name_en && c.Name_ar == Entity.Name_ar);
                if (exist)
                {
                    result = new()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "The SubCategory is already Exist in DB"
                    };
                    return result;
                }
                var subcategory = mapper.Map<SubCategory>(Entity);
                var AddedEntity = await subCategoryRepository.CreateAsync(subcategory);
                await subCategoryRepository.SaveChanges();
                var returnedCategory = mapper.Map<CreateorUpdatedSubCategoryDTOs>(AddedEntity);
                result = new ResultView<CreateorUpdatedSubCategoryDTOs>()
                {
                    Entity = returnedCategory,
                    IsSuccess = true,
                    Message = "The SubCategory is Created Successfuly"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error Happen" + ex.Message
                };
                return result;
            }
        }

        // Delete subcaegory
        public async Task<ResultView<GetAllSubCategoryDTOs>> DeleteSubCategoryAsync(int subcatid)
        {
            ResultView<GetAllSubCategoryDTOs> result;
            try
            {
                var subcategory = (await subCategoryRepository.GetAllAsync()).FirstOrDefault(cat => cat.Id == subcatid);
                await subCategoryRepository.DeleteAsync(subcategory);
                await subCategoryRepository.SaveChanges();
                result = new ResultView<GetAllSubCategoryDTOs>()
                {
                    Entity = null,
                    IsSuccess = true,
                    Message = "SubCategory Deleted Successfuly"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<GetAllSubCategoryDTOs>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error: " + ex.Message
                };
                return result;
            }
        }

        // Get All Subcategory
        public async Task<List<GetAllSubCategoryDTOs>> GetAllSubCategoriesAsync()
        {
            var AllsubCategories = (await subCategoryRepository.GetAllSubcategoryAsync()).ToList();
            //var returnedsubcategory = mapper.Map<List<GetAllSubCategoryDTOs>>(AllsubCategories);
            return AllsubCategories;
        }

        //Get All subcategory pagination
        public async Task<EntityPaginated<GetAllSubCategoryDTOs>> GetAllSubCategoriesPaginatedAsync(int pagenumber, int count)
        {
            var paginatedsubcategory = (await subCategoryRepository.GetAllSubcategoryAsync())
                            .Skip((pagenumber - 1) * count)
                            .Take(count)
                            .ToList();
            var countofsubcategory = (await subCategoryRepository.GetAllAsync()).Count();
            return new EntityPaginated<GetAllSubCategoryDTOs>()
            {
                Data = paginatedsubcategory,
                Count = countofsubcategory
            };
        }

        // Search about Subcategory by name 
        public async Task<List<GetAllSubCategoryDTOs>> SearchByNameAsync(string Subcategoryname)
        {
            var subcategories = (await subCategoryRepository.GetAllAsync())
                            .Where(cat => cat.Name_en.Contains(Subcategoryname) || cat.Name_ar.Contains(Subcategoryname)).ToList();
            var searchresult = mapper.Map<List<GetAllSubCategoryDTOs>>(subcategories);
            return searchresult;
        }
        //updated Subcategory
        public async Task<ResultView<CreateorUpdatedSubCategoryDTOs>> UpdatedSubCategpryAsync(CreateorUpdatedSubCategoryDTOs Entity)
        {
            ResultView<CreateorUpdatedSubCategoryDTOs> result = new ResultView<CreateorUpdatedSubCategoryDTOs>();
            try
            {
                var subcategory = mapper.Map<SubCategory>(Entity);
                var updatedEntity = await subCategoryRepository.UpdateAsync(subcategory);
                await subCategoryRepository.SaveChanges();

                var returnedsubCategory = mapper.Map<CreateorUpdatedSubCategoryDTOs>(updatedEntity);
                result = new ResultView<CreateorUpdatedSubCategoryDTOs>()
                {
                    Entity = returnedsubCategory,
                    IsSuccess = true,
                    Message = "The SubCategory is Updated Successfuly"
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
