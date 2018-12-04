using Microsoft.AspNetCore.Http;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IRoadsService
    {
        bool Create(string roadName, string startingPoint, string endPoint, double roadLength, string description, string video,string userId,IFormFile photo);
    }
}