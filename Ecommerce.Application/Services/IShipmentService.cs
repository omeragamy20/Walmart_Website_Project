using Ecommerce.DTOs.Shipment;
//using Ecommerce.DTOs.Shipment.Ecommerce.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IShipmentService
    {
        Task<CreateDto> CreateShipmentAsync(CreateDto shipmentDto);
        Task<CreateDto> UpdateShipmentAsync(CreateDto shipmentDto);
        Task<bool> DeleteShipmentAsync(int id);
        Task<IEnumerable<GetAllDto>> GetAllShipmentsAsync();
        Task<GetAllDto> GetShipmentByIdAsync(int id);
    }


}
