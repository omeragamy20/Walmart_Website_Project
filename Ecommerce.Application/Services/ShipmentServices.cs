using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Shipment;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ShipmentServices : IShipmentService
    {
        private readonly IShaipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public ShipmentServices(IShaipmentRepository shipmentRepository, IMapper mapper)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }

        public async Task<CreateDto> CreateShipmentAsync(CreateDto shipmentDto)
        {
            var shipment = _mapper.Map<Shipment>(shipmentDto);
            await _shipmentRepository.CreateAsync(shipment);
            await _shipmentRepository.SaveChanges();
            return _mapper.Map<CreateDto>(shipment);
        }




        ////////////////////////////////////////////////////////////
        public async Task<UpdateDTO> UpdateShipmentAsync(UpdateDTO shipmentDto)
        {
            


            var shipment = _mapper.Map<Shipment>(shipmentDto);
            var updatedshapment = await _shipmentRepository.UpdateAsync(shipment);
            await _shipmentRepository.SaveChanges();
            var Dtoshipment = _mapper.Map<UpdateDTO>(updatedshapment);
            return Dtoshipment;
        }


        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        public async Task<bool> DeleteShipmentAsync(int id)
        {
            var existingShipment = await _shipmentRepository.GetOneAsync(id);
            //if (existingShipment == null) return false; 

            await _shipmentRepository.DeleteAsync(existingShipment);
            await _shipmentRepository.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<GetAllDto>> GetAllShipmentsAsync()
        {
            var shipments = (await _shipmentRepository.GetAllAsync()).ToList();
            return shipments.Select(s => _mapper.Map<GetAllDto>(s)).ToList();
        }

        public async Task<UpdateDTO> GetShipmentByIdAsync(int id)
        {
            var shipment = await _shipmentRepository.GetOneAsync(id);
            var data = _mapper.Map<UpdateDTO>(shipment);
            return data;
        }
    }
}
