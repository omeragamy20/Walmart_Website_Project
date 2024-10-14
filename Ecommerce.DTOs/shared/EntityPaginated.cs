using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.shared
{
    public class EntityPaginated<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }
    }
}
