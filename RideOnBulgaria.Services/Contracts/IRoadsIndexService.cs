using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IRoadsIndexService
    {
        ICollection<Road> GetAllRoads();
        ICollection<Road> GetLatestRoads();
        ICollection<Road> GetLongestRoads();
        ICollection<Road> GetTopRoads();
        ICollection<Road> GetCurrentUserRoadsByName(string id);
    }
}