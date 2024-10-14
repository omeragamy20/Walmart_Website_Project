using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.shared
{
    public class ResultView<T>
    {
        public bool IsSuccess { get; set; }
        public T? Entity { get; set; }
        public string? Message { get; set; }

        //public ResultView(T data)
        //{
        //    Success = true;
        //    Data = data;
        //}

        //public ResultView(string errorMessage)
        //{
        //    Success = false;
        //    ErrorMessage = errorMessage;
        //}
    }
}
