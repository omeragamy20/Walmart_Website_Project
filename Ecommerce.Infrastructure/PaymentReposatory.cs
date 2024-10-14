using Ecommerce.Application.Contracts;
using Ecommerce.Application.Services;
using Ecommerce.Context;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class PaymentRepository : GenricReposatiry<Payment, int>, IPaymentRepoistory
    {
        public PaymentRepository(EcommerceContext context) : base(context)
        {
        }

        public IQueryable<Payment> SearchByTransactionId(string transactionId)
        {
            throw new NotImplementedException();
        }

        //public IQueryable<Payment> SearchOption(string keyword)
        //{
        //    return context.Where(p => p.PaymentMethod_en.Contains(keyword) ||
        //                             (p.PaymentMethod_ar != null && p.PaymentMethod_ar.Contains(keyword)));
        //}
    }

}
