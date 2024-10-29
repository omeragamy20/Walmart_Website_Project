using Ecommerce.DTOs.DTOsFavoritandRate;
using Ecommerce.DTOs.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.FavortandRateService
{
    public interface IRateServices
    {
        public Task<ResultView<CreateorUpdateRateDTO>> CreateorUpdatedRateAsync(CreateorUpdateRateDTO Entity);
        public Task<ResultView<GetAllRateDTO>> DeleteRateAsync(CreateorUpdateRateDTO Entity);
        public Task<List<GetAllRateDTO>> GetAllRateAsync();
        public Task<double> GetAVGProductRateAsync(int PrudctId);
    }
}
