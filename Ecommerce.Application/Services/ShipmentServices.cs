using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Shipment;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShaipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public ShipmentService(IShaipmentRepository shipmentRepository, IMapper mapper)
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

        public async Task<GetAllDto> UpdateShipmentAsync(GetAllDto shipmentDto)
        {
            var shipment = _mapper.Map<Shipment>(shipmentDto);
            await _shipmentRepository.UpdateAsync(shipment);
            await _shipmentRepository.SaveChanges();
            return shipmentDto;
        }

        public async Task<bool> DeleteShipmentAsync(int id)
        {
            var shipment = await _shipmentRepository.GetOneAsync(id);
            if (shipment == null) return false;
            await _shipmentRepository.DeleteAsync(shipment);
            await _shipmentRepository.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<GetAllDto>> GetAllShipmentsAsync()
        {
            var shipments = await _shipmentRepository.GetAllAsync();
            return shipments.Select(s => _mapper.Map<GetAllDto>(s)).ToList();
        }

        public async Task<GetAllDto> GetShipmentByIdAsync(int id)
        {
            var shipment = await _shipmentRepository.GetOneAsync(id);
            return _mapper.Map<GetAllDto>(shipment);
        }

        public Task<CreateDto> UpdateShipmentAsync(CreateDto shipmentDto)
        {
            throw new NotImplementedException();
        }

        //public Task<CreateDto> CreateShipmentAsync(CreateDto shipmentDto)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<CreateDto> UpdateShipmentAsync(CreateDto shipmentDto)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<GetAllDto>> IShipmentService.GetAllShipmentsAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<GetAllDto> IShipmentService.GetShipmentByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
    


