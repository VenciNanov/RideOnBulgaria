using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IImageService
    {

        Image AddPhoto(IFormFile formFile);
        Task<bool> SaveAll();

        string ReturnImage(Image image);
        //Task<PhotoForReturnViewModel> GetPhoto(string id);
    }
}