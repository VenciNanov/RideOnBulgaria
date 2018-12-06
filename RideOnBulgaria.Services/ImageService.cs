﻿using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class ImageService : IImageService
    {
        private readonly IUsersService _usersService;
        private readonly Cloudinary _cloudinary;
        private readonly ApplicationDbContext context;

        public ImageService(ApplicationDbContext context, IUsersService usersService)
        {
            this.context = context;
            _usersService = usersService;

            Account account = new Account
            {
                Cloud = "rideonbg",
                ApiSecret = "AXj6a668SCJ9PchRZbFYGpmaMmo",
                ApiKey = "695619639461885",
            };

            _cloudinary = new Cloudinary(account);
        }

        public Image AddPhoto(IFormFile photo)
        {

            var file = photo;

            var uploadResult = new ImageUploadResult();

            string imageName;

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)

                    };
                    imageName = file.Name;
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();
            var publicId = uploadResult.PublicId;


            var image = new Image
            {
                ImageUrl = url,
                DateAdded = DateTime.UtcNow,
                PublicId = publicId,
              };

            return image;

        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public string ReturnImage(Image image)
        {
            string url = _cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(2000).Height(300).Crop("fill").Gravity("center"))
                .BuildUrl(image.PublicId);

            return url;
        }

        //public async Task<PhotoForReturnViewModel> GetPhoto(string id)
        //{
        //    var photo = await context.Images.FirstOrDefaultAsync(x => x.Id == id);

        //    var photoForReturn = new PhotoForReturnViewModel
        //    {
        //        Id = photo.Id,
        //        Url = photo.ImageUrl,
        //        DateAdded = photo.DateAdded,
        //        PublicId = photo.PublicId
        //    };
        //    return photoForReturn;
        //}
    }
}