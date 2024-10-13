using Ecommerce.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DTOs.shared;
namespace Ecommerce.Application.Services
{
    public interface IPaymentService
    {
        Task<ResultView<Create_DTO>> AddPaymentAsync(Create_DTO payment);
        Task<ResultView<Create_DTO>> UpdatePaymentAsync(Create_DTO payment);
        Task<ResultView<Read_DTO>> DeletePaymentAsync(Read_DTO payment);
        Task<ResultView<Read_DTO>> GetOneAsync(int id);
        Task<IEnumerable<Read_DTO>> GetAllPaymentsAsync();

    }

}
