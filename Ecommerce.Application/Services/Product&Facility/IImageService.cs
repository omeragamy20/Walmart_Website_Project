using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Product_Facility
{
    public interface IImageService
    {
        Task DeleteImageAsync(int id);
        Task<Images> GetImageByIdAsync(int id);
        Task<List<Images>> UploadImagesAsync(List<IFormFile> files, int productId);
    }
}
