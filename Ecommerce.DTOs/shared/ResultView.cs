using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.shared
{
    public class ResultView<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public ResultView(T data)
        {
            Success = true;
            Data = data;
        }

        public ResultView(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }
}
