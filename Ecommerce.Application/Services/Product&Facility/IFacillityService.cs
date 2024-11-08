using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IFacillityService
    {
        Task<ResultView<CreatorUpdateFacilityDTO>> CreateAsync(CreatorUpdateFacilityDTO entity);
        Task<ResultView<CreatorUpdateFacilityDTO>> UpdateAsync(CreatorUpdateFacilityDTO entity);
        Task DeleteAsync(int id);
        Task<List<FacilityDTO>> GetAllAsync();

        Task<List<FacilityDTO>> GetAllBySubIdAsync(int subId);
        Task<CreatorUpdateFacilityDTO> GetByIdAsync(int id);
    }
}
