﻿using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.product_Facillity;
namespace Ecommerce.Application.Services.Product_Facility
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRebository;
        private readonly IMapper mapper;
        public ImageService(IImageRepository _imageRepository, IMapper _mapper)
        {

            imageRebository = _imageRepository;
            mapper = _mapper;
        }
        public async Task DeleteImageAsync(int id)
        {
            var oldone = (await imageRebository.GetOneAsync(id));
            await imageRebository.DeleteAsync(oldone);
            await imageRebository.SaveChanges();
        }

        public async Task<Images> GetImageByIdAsync(int id)
        {
            return await imageRebository.GetOneAsync(id);
        }

        public async Task<List<Images>> UploadImagesAsync(List<IFormFile> files, int productId)
        {
            var uploadedImages = new List<Images>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/product", uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var imageEntity = new Images
                        {
                            Image = $"/images/product/{uniqueFileName}", 
                            ProductId = productId
                        };

                        await imageRebository.CreateAsync(imageEntity);
                        await imageRebository.SaveChanges();
                        uploadedImages.Add(imageEntity);
                    }
                }
            }
            return uploadedImages;
        }

    }
}