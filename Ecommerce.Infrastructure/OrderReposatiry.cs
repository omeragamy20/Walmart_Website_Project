using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class OrderReposatiry : GenricReposatiry<Order, int>,IOrderReposatiry
    {
        private readonly EcommerceContext context;
        public OrderReposatiry(EcommerceContext _Context) : base (_Context)
        {
            context = _Context;
        }


        public List<GetCustomerOrders> GetAllByCusIdAsync(string id)
        {
            var orders = context.TheOrders.Where(o => o.CustomerId == id).Select(o => new GetCustomerOrders
            {

                Id = o.Id,
                OrderDate = o.OrderDate.ToShortDateString(),
                TotalPrice = o.TotalPrice,
                PaymentId = o.PaymentId,
                Payment_ar = o.Payment.PaymentMethod_ar ,
                Payment_en = o.Payment.PaymentMethod_en,
                ShipmentId = o.ShipmentId,
                OrderItems = o.OrderItems.Select(p => p.ProductId ?? 0).ToList(),
                OrderItemsQuantity = o.OrderItems.Select(p => p.Quantity).ToList(),
                ShipmentAddress = o.Shipment.Address,
                Status = o.Status
            })
                          .ToList();
            return orders;
     
        }
    }
}
