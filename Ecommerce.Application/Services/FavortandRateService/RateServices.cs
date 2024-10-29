using AutoMapper;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Application.Contracts.FavortandRateRepo;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.DTOsFavoritandRate;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.FavortandRateService
{
    public class RateServices:IRateServices
    {
        private readonly IRateRepository raterepository;
        private readonly IMapper mapper;
        public RateServices(IRateRepository _raterepository, IMapper _mapper)
        {
            raterepository = _raterepository;
            mapper = _mapper;
        }
        public async Task<ResultView<CreateorUpdateRateDTO>> CreateorUpdatedRateAsync(CreateorUpdateRateDTO Entity)
        {
            ResultView<CreateorUpdateRateDTO> result = new ResultView<CreateorUpdateRateDTO>();
            try
            {
                bool exist = (await raterepository.GetAllAsync()).Any(R=>R.ProductId==Entity.ProductId&&R.CustomerId==Entity.CustomerId);
                if (exist)
                {
                    var rate = mapper.Map<Rate>(Entity);
                    var EditedEntity = await raterepository.UpdateAsync(rate);
                    await raterepository.SaveChanges();
                    var returnedCategory = mapper.Map<CreateorUpdateRateDTO>(EditedEntity);
                    result = new ResultView<CreateorUpdateRateDTO>()
                    {
                        Entity = returnedCategory,
                        IsSuccess = true,
                        Message = "The Rate is Changed Successfuly"
                    };
                    return result;
                }
                else {
                    var rate = mapper.Map<Rate>(Entity);
                    var AddedEntity = await raterepository.CreateAsync(rate);
                    await raterepository.SaveChanges();
                    var returnedCategory = mapper.Map<CreateorUpdateRateDTO>(AddedEntity);
                    result = new ResultView<CreateorUpdateRateDTO>()
                    {
                        Entity = returnedCategory,
                        IsSuccess = true,
                        Message = "The Rate is Created Successfuly"
                    };
                    return result;
                }

                
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error HAppen" + ex.Message
                };
                return result;
            }
        }

        public async Task<ResultView<GetAllRateDTO>> DeleteRateAsync(CreateorUpdateRateDTO Entity)
        {
            ResultView<GetAllRateDTO> result;
            try
            {
                var rate = mapper.Map<Rate>(Entity);
                await raterepository.DeleteAsync(rate);
                await raterepository.SaveChanges();
                result = new ResultView<GetAllRateDTO>()
                {
                    Entity = null,
                    IsSuccess = true,
                    Message = "The Rate Deleted Successfuly"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<GetAllRateDTO>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Error: " + ex.Message
                };
                return result;
            }
        }

        public async Task<List<GetAllRateDTO>> GetAllRateAsync()
        {
            var AllRates = (await raterepository.GetAllAsync()).ToList();
            var returnedcategory = mapper.Map<List<GetAllRateDTO>>(AllRates);
            return returnedcategory;
        }
        public async Task<double> GetAVGProductRateAsync(int PrudctId)
        {
            var AllRates = (await raterepository.GetAllAsync()).Where(r=>r.ProductId==PrudctId).Average(r=>r.Rating);
            //var returnedcategory = mapper.Map<List<GetAllRateDTO>>(AllRates);
            return AllRates;
        }
    }
}
