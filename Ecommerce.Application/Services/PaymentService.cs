using Ecommerce.DTOs.Payment;
using Ecommerce.DTOs.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DTOs.shared;
using Ecommerce.Application.Contracts;
namespace Ecommerce.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentService _paymentRepository;

        public PaymentService(PaymentService paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<ResultView<Create_DTO>> AddPaymentAsync(Create_DTO paymentDto)
        {
           

            return await _paymentRepository.AddPaymentAsync(paymentDto);
        }

        public async Task<ResultView<Create_DTO>> UpdatePaymentAsync(Create_DTO paymentDto)
        {
           

            return await _paymentRepository.UpdatePaymentAsync(paymentDto);
        }

        public async Task<ResultView<Read_DTO>> DeletePaymentAsync(Read_DTO paymentDto)
        {
            

            return await _paymentRepository.DeletePaymentAsync(paymentDto);
        }

        public async Task<IEnumerable<Read_DTO>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }
        public async Task<ResultView<Read_DTO>> GetOneAsync(int id)
        {
            return await _paymentRepository.GetOneAsync(id);
        }

     
    }

   
}
