using System.Threading.Tasks;
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
        //Task<PhotoForReturnViewModel> GetPhoto(string id);
    }
}