using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class FacilityService : IFacillityService
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly IMapper mapper;
        public FacilityService(IFacilityRepository _facilityRepository,IMapper _mapper) 
        {
            facilityRepository = _facilityRepository;
            mapper = _mapper;
        }
        public async Task<ResultView<FacilityDTO>> CreateAsync(FacilityDTO entity)
        {
            ResultView<FacilityDTO> result = new ResultView<FacilityDTO>();
            try
            {
                var facility = (await facilityRepository.GetAllAsync()).Any(f => f.Name_en == entity.Name_en || f.Name_ar == entity.Name_ar);
                if (facility)
                {
                    result = new()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "This Facility Already Exist"
                    };
                    return result;
                }
                else
                {

                    var newfacility = mapper.Map<Facility>(entity);
                    var success=(await facilityRepository.CreateAsync(newfacility));
                    await facilityRepository.SaveChanges();
                    var returned = mapper.Map<FacilityDTO>(success);
                    result = new()
                    {
                        Entity = returned,
                        IsSuccess = true,
                        Message = "Created Successfully"
                    };
                    return result;
                }
            }
            catch(Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var facility = await facilityRepository.GetOneAsync(id);
            await facilityRepository.DeleteAsync(facility);
            await facilityRepository.SaveChanges();
        }

        public async Task<List<FacilityDTO>> GetAllAsync()
        {
            
                var data = (await facilityRepository.GetAllAsync())
                    .Include(p => p.product);
                
                    var facilities = mapper.Map<List<FacilityDTO>>(data);
                    
                    return facilities;   
        }

        public async Task<FacilityDTO> GetByIdAsync(int id)
        {
             var data = await facilityRepository.GetOneAsync(id);
               
                var returned=mapper.Map<FacilityDTO>(data);
                  
                return returned;            
        }

       

        public async Task<ResultView<FacilityDTO>> UpdateAsync(FacilityDTO entity)
        {
            ResultView<FacilityDTO> result = new ResultView<FacilityDTO>();
            try
            {
                var oldone = (await facilityRepository.GetAllAsync())
                 .Include(p=>p.product)
                 .FirstOrDefault(p => p.Id == entity.Id);
                mapper.Map(entity, oldone);
                var updated = await facilityRepository.UpdateAsync(oldone);
                await facilityRepository.SaveChanges();
                var success = mapper.Map<FacilityDTO>(updated);
                var msg = "Updated Successfully";
                result = new()
                {
                    Entity = success,
                    IsSuccess = true,
                    Message = msg
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    Message = "Error " + ex,
                    IsSuccess = false
                };
                return result;
            }
        }
    }
}
