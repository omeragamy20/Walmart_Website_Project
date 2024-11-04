using AutoMapper;
using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Product_Facility
{
    public class ProductFacilityServices : IProductFacilityServices
    {
        private readonly IProductFacilityRepository productfacilityRepository;

        private readonly IMapper mapper;
        public ProductFacilityServices(IProductFacilityRepository _productfacilityRepository, IMapper _mapper)
        {
            productfacilityRepository = _productfacilityRepository;
            mapper = _mapper;
        }
        public async Task<List<CreateorUpdatePrdctFaciltyDTOs>> CreatePrdFaciltyAsync(PrdFacilitySubCategory Entity)
        {
            List<CreateorUpdatePrdctFaciltyDTOs> result = new List<CreateorUpdatePrdctFaciltyDTOs>();
            for (int i = 0;i<Entity.FacilityIds?.Count;i++)
            {
                ProductFacility PrdFac=new ProductFacility()
                {
                    facilityID = Entity.FacilityIds[i],
                    ProductID=Entity.ProductId,
                    Value_en = Entity.Values_En[i],
                    Value_ar = Entity.Values_Ar[i],
                    
                };
                var createdenity=await productfacilityRepository.CreateAsync(PrdFac);
                await productfacilityRepository.SaveChanges();
                var data=mapper.Map<CreateorUpdatePrdctFaciltyDTOs>(createdenity);
                result.Add(data);
            }
            return result;
        }

        public async Task<List<CreateorUpdatePrdctFaciltyDTOs>> UpdatePrdFaciltyAsync(PrdFacilitySubCategory Entity)
        {
            var oldentity=(await productfacilityRepository.GetAllAsync()).Where(pf=>pf.ProductID==Entity.ProductId).ToList();

            List<CreateorUpdatePrdctFaciltyDTOs> result = new List<CreateorUpdatePrdctFaciltyDTOs>();
            for (int i = 0; i < Entity.FacilityIds?.Count; i++)
            {
                //ProductFacility PrdFac = new ProductFacility()
                //{
                //    facilityID = Entity.FacilityIds[i],
                //    ProductID = Entity.ProductId,
                //    Value_en = Entity.Values_En[i],
                //    Value_ar = Entity.Values_Ar[i]
                //};
                oldentity[i].facilityID = Entity.FacilityIds[i];
                oldentity[i].ProductID = Entity.ProductId;
                oldentity[i].Value_en = Entity.Values_En[i];
                oldentity[i].Value_ar = Entity.Values_Ar[i];
                
                var createdenity = await productfacilityRepository.UpdateAsync(oldentity[i]);
                await productfacilityRepository.SaveChanges();
                var data = mapper.Map<CreateorUpdatePrdctFaciltyDTOs>(createdenity);
                result.Add(data);
            }
            return result;
        }

        public async Task<PrdFacilitySubCategory> GetFacilitesByPrdIdAsync(int prdID)
        {
           var r =  (await productfacilityRepository.GetAllAsync()).Where(p=>p.ProductID ==prdID)
                .ToList();

            PrdFacilitySubCategory res = new PrdFacilitySubCategory();
            res.ProductId = prdID;
            foreach (var ritem in r)
            {
             //   res.FacilityIds.Add(ritem.facilityID ?? 0);
                res.Values_Ar.Add(ritem.Value_ar);
                res.Values_En.Add(ritem.Value_en);
               

            }

            return res ?? new();
        }
    }
}
