using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IImageService
    {

        Image AddPhoto(IFormFile formFile);
        Task<bool> SaveAll();
        void RemovePicture(string publicId);
        string ReturnImage(Image image);
        void RemoveImage(Image image);
        Image FindImageById(string id);
        ProductImage AddImageToProduct(IFormFile photo);
        string ReturnProductImage(ProductImage image);

        //Task<PhotoForReturnViewModel> GetPhoto(string id);
    }
}