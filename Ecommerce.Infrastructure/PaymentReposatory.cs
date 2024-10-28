using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class PaymentRepository : GenricReposatiry<Payment, int>, IPaymentRepoistory
    {
        private readonly EcommerceContext _ecommerce;

        public PaymentRepository(EcommerceContext ecommerce) : base(ecommerce)
        {
            _ecommerce = ecommerce;
        }

        public IQueryable<Payment> SearchOption(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> GetOneByidCodeAsync(int id)
        {
            return await _ecommerce.Payments
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<Payment> SearchByTransactionId(string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
