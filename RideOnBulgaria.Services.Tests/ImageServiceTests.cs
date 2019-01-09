using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class ImageServiceTests
    {
        [Fact]
        public void FindImageByIdShouldReturnImage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("FindImageById_Image_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new ImageService(dbContext, null);

            var image = new Image
            {
                PublicId = "PublicId",
                Name = "123",
                DateAdded = DateTime.UtcNow,
                Id = "imageId1",
            };
            dbContext.Images.Add(image);
            dbContext.SaveChanges();

            var result = imageService.FindImageById("imageId1");
            var images = dbContext.Images.ToList();
            
            Assert.Equal("imageId1",result.Id);
        }

        [Fact]
        public void RemovePictureShouldRemoveTheGivenPicture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("FindImageById_Image_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new ImageService(dbContext, null);

            var image = new Image
            {
                PublicId = "PublicId",
                Name = "123",
                DateAdded = DateTime.UtcNow,
                Id = "imageId1",
            };
            dbContext.Images.Add(image);
            dbContext.SaveChanges();

            imageService.RemoveImage("imageId1");

            Assert.Empty(dbContext.Images);
        }

    }
}