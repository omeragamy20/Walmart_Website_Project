using AutoMapper;
using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Product_Facility
{
    public class SubCatFacilityService: ISubCatFacilityService
    {
        private readonly ISubCatFacilityRepository subCatFacilityRepository;
        private readonly IMapper mapper;
        public SubCatFacilityService(IMapper _mapper, ISubCatFacilityRepository _subCatFacilityRepository)
        {
            subCatFacilityRepository = _subCatFacilityRepository;
            mapper = _mapper;
        }
        public async Task<List<CreatorUpdateFacilityDTO>> Getallfacilitybyubcatid(List<int>? subcatids)
        {
            List<CreatorUpdateFacilityDTO> Returnfaciltyids = new List<CreatorUpdateFacilityDTO>();
            for (int i = 0; i < subcatids.Count; i++)
            {
                var lstids = (await subCatFacilityRepository.GetAllAsync())
                    .Where(sf => sf.SubCategoryID == subcatids[i])
                    .Select(fs =>new CreatorUpdateFacilityDTO
                    { Name_ar = fs.facility.Name_ar,
                    Name_en = fs.facility.Name_en,
                    Id = fs.facility.Id,
                              
                }).ToList();
                Returnfaciltyids.AddRange(lstids);
            }
            //HashSet<CreatorUpdateFacilityDTO> uniqueids = new HashSet<CreatorUpdateFacilityDTO>(Returnfaciltyids);
            List<CreatorUpdateFacilityDTO> result = Returnfaciltyids.DistinctBy(fs => fs.Name_en).ToList();
            return result;

        }
    }
}
