using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IRoadsIndexService
    {
        ICollection<Road> GetAllRoads();
    }
}