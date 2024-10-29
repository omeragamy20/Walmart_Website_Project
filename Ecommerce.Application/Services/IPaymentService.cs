
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DTOs.shared;
using Ecommerce.DTOs.Shipment;
using Ecommerce.DTOs.Shipment;
using Ecommerce.DTOs.Payment;
namespace Ecommerce.Application.Services
{

    public interface IPaymentService
    {
     
        public Task<CreateDtos> CreatePaymentAsync(CreateDtos paymentDto);
        public Task<UpdateDTOs> UpdatePaymentAsync(UpdateDTOs paymentDto);
        public Task<bool> DeletepaymentAsync(int id);
        public Task<IEnumerable<GetAllDtos>> GetAllPaymentsAsync();
        public Task<UpdateDTOs> GetPaymentByIdAsync(int id);

    }

}



