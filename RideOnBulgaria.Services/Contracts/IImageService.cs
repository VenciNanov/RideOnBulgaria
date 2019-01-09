using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IImageService
    {

        Image AddPhoto(IFormFile formFile);
        //Task<bool> SaveAll();
        string ReturnImageWithGiverDimensions(Image image, int width, int height, string crop);
        string ReturnImage(Image image);
        void RemoveImage(string imageId);
        Image FindImageById(string id);
        ProductImage AddImageToProduct(IFormFile photo);
        string ReturnProductImage(ProductImage image);

    }
}