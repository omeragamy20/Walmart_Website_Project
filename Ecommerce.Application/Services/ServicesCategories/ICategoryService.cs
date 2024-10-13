using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.ServicesCategories
{
    public interface ICategoryService
    {
        public Task<ResultView<CreateorUpdatedCategoryDTOs>> CreateCategpryAsync(CreateorUpdatedCategoryDTOs Entity);
        public Task<ResultView<CreateorUpdatedCategoryDTOs>> UpdatedCategpryAsync(CreateorUpdatedCategoryDTOs Entity);
        public Task<List<CreateorUpdatedCategoryDTOs>> GetAllCategoriesAsync();
        public Task<EntityPaginated<GetAllCategoryDTOs>> GetAllCategoriesPaginatedAsync(int pagenumber,int count);
        public Task<List<GetAllCategoryDTOs>> SearchByNameAsync(string categoryname);
        public Task<ResultView<GetAllCategoryDTOs>> DeleteCategoryAsync(int catid);
    }
}
