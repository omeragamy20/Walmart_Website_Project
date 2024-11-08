using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.product_Facillity;
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
        private readonly ISubCatFacilityRepository subcatfacilityRepository;
        private readonly IMapper mapper;
        public FacilityService(IFacilityRepository _facilityRepository,IMapper _mapper, ISubCatFacilityRepository _subcatfacilityRepository) 
        {
            facilityRepository = _facilityRepository;
            mapper = _mapper;
            subcatfacilityRepository = _subcatfacilityRepository;
        }
        public async Task<List<FacilityDTO>> GetAllBySubIdAsync(int subId)
        {
            var data=(await subcatfacilityRepository.GetAllAsync()).Where(s=>s.SubCategoryID==subId)
                .Select(f=>new FacilityDTO
                {
                    Id=f.facility.Id,
                    Name_en=f.facility.Name_en,
                    Name_ar=f.facility.Name_ar,
                    Values_En=f.facility.ProductFacilities.Select(v=>v.Value_en).ToList(),
                    Values_Ar=f.facility.ProductFacilities.Select(v=>v.Value_ar).ToList()
                }).ToList();
            return data;
        }
        public async Task<ResultView<CreatorUpdateFacilityDTO>> CreateAsync(CreatorUpdateFacilityDTO entity)
        {
            ResultView<CreatorUpdateFacilityDTO> result = new ResultView<CreatorUpdateFacilityDTO>();
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
                    //newfacility.subCatFacility = entity.subcategoriesId.Select(id => new subCatFacility
                    //{
                    //    SubCategoryID = id
                    //}).ToList();
                    for(int i = 0; i < entity.subcategoriesId.Count; i++)
                    {
                        subCatFacility SCF=new subCatFacility() {
                            facilityId=success.Id,
                            SubCategoryID= entity.subcategoriesId[i]
                        };
                        await subcatfacilityRepository.CreateAsync(SCF);
                        await subcatfacilityRepository.SaveChanges();
                    }
                    var returned = mapper.Map<CreatorUpdateFacilityDTO>(success);
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
                .Select(f=>new FacilityDTO
                {
                    Id=f.Id,
                    Name_en=f.Name_en,
                    Name_ar=f.Name_ar,
                    SubCategoryNames=f.subCatFacility.Select(s=>s.subCategory.Name_en).ToList(),
                    SubCategoryNamesAr= f.subCatFacility.Select(s => s.subCategory.Name_ar).ToList()
                }).ToList();
                
            //var facilities = mapper.Map<List<FacilityDTO>>(data);
                    
            return data;   
        }

        public async Task<CreatorUpdateFacilityDTO> GetByIdAsync(int id)
        {
             var data = await facilityRepository.GetOneAsync(id);
               
                var returned=mapper.Map<CreatorUpdateFacilityDTO>(data);
                  
                return returned;            
        }

       

        public async Task<ResultView<CreatorUpdateFacilityDTO>> UpdateAsync(CreatorUpdateFacilityDTO entity)
        {
            ResultView<CreatorUpdateFacilityDTO> result = new ResultView<CreatorUpdateFacilityDTO>();
            try
            {
                //var oldone = (await facilityRepository.GetAllAsync())
                // .Include(p=>p.ProductFacilities)
                //  .Include(p => p.subCatFacility)
                // .FirstOrDefault(p => p.Id == entity.Id);
                //mapper.Map(entity, oldone);
                //oldone.subCatFacility.Clear();
                //oldone.subCatFacility = entity.SubCategoryIds.Select(id => new subCatFacility
                //{
                //    SubCategoryID = id
                //}).ToList();
                List<subCatFacility> SCF=new List<subCatFacility>();
                SCF=(await subcatfacilityRepository.GetAllAsync()).Where(sf=>sf.facilityId==entity.Id).ToList();
                foreach (var facility in SCF)
                {
                    await subcatfacilityRepository.DeleteAsync((subCatFacility) facility);
                }
                var updatedentity=mapper.Map<Facility>(entity);
                var newupdated = await facilityRepository.UpdateAsync(updatedentity);
                await facilityRepository.SaveChanges();
                for (int i = 0; i < entity.subcategoriesId.Count; i++)
                {
                    subCatFacility sf = new subCatFacility()
                    {
                        facilityId = newupdated.Id,
                        SubCategoryID = entity.subcategoriesId[i]
                    };
                    await subcatfacilityRepository.CreateAsync(sf);
                    await subcatfacilityRepository.SaveChanges();
                }
                var success = mapper.Map<CreatorUpdateFacilityDTO>(newupdated);
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
