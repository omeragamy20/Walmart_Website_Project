using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Service;
using Ecommerce.Application.Services;
using Ecommerce.DTOs.Payment;
using Ecommerce.DTOs.Shipment;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Service
{
    public class Paymentservice : IPaymentService
    {
        private readonly Contracts.IPaymentRepoistory _paymentRepository;
        private readonly IMapper _mapper;

        public Paymentservice(Contracts.IPaymentRepoistory paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<CreateDtos> CreatePaymentAsync(CreateDtos paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            await _paymentRepository.CreateAsync(payment);
            await _paymentRepository.SaveChanges();
            return _mapper.Map<CreateDtos>(payment);
        }

        public async Task<UpdateDTOs> UpdatePaymentAsync(UpdateDTOs paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            var updatedPayment = await _paymentRepository.UpdateAsync(payment);
            await _paymentRepository.SaveChanges();
            var dtoPayment = _mapper.Map<UpdateDTOs>(updatedPayment);
            return dtoPayment;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var existingPayment = await _paymentRepository.GetOneAsync(id);
            if (existingPayment == null) return false;

            await _paymentRepository.DeleteAsync(existingPayment);
            await _paymentRepository.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<GetAllDtos>> GetAllPaymentsAsync()
        {
            var payments = (await _paymentRepository.GetAllAsync()).ToList();
            return payments.Select(p => _mapper.Map<GetAllDtos>(p)).ToList();
        }

        public async Task<UpdateDTOs> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetOneAsync(id);
            var dtoPayment = _mapper.Map<UpdateDTOs>(payment);
            return dtoPayment;
        }

        public async Task<bool> DeletepaymentAsync(int id)
        {
            var existingPayment = await _paymentRepository.GetOneAsync(id);
            if (existingPayment == null) return false;

            await _paymentRepository.DeleteAsync(existingPayment);
            await _paymentRepository.SaveChanges();
            return true;
        }

    }
}
