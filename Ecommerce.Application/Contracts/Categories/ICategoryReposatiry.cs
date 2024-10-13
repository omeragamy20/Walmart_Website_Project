using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts.Categories
{
    public interface ICategoryReposatiry:IGenericReposatiry<Category, int>
    {
    }
}
