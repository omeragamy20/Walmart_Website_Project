using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class OrderItemsReposatiry : GenricReposatiry<OrderItem,int>,IOrderItemsReposatiry
    {
        private readonly EcommerceContext Context;
        public OrderItemsReposatiry(EcommerceContext _Context) : base (_Context)
        {
            Context = _Context;
        }

        public void UpdatePrd(CreateOrUpdateOrderItemDTOs Dto)
        {
            var prd = Context.Products.Include(prd=>prd.productSubCategory)
                                       .Include(p=>p.ProductFacilities)
                                       .Include(p=>p.Images)
                                       .FirstOrDefault(p => p.Id == Dto.ProductId);
            if (prd != null)
            {
                prd.Stock -= Dto.Quantity;
                Context.Products.Update(prd);
                Context.SaveChanges();
            }


        }




    }
}
