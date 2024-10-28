using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.ServicesCategories
{
    public interface ISubCategoryServices
    {
        public Task<ResultView<CreateorUpdatedSubCategoryDTOs>> CreateSubCategpryAsync(CreateorUpdatedSubCategoryDTOs Entity);
        public Task<ResultView<CreateorUpdatedSubCategoryDTOs>> UpdatedSubCategpryAsync(CreateorUpdatedSubCategoryDTOs Entity);
        public Task<List<GetAllSubCategoryDTOs>> GetAllSubCategoriesAsync();
        public Task<EntityPaginated<GetAllSubCategoryDTOs>> GetAllSubCategoriesPaginatedAsync(int pagenumber, int count);
        public Task<List<GetAllSubCategoryDTOs>> SearchByNameAsync(string Subcategoryname);
        public Task<ResultView<GetAllSubCategoryDTOs>> DeleteSubCategoryAsync(int subcatid);
    }
}
