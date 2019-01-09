using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class RoadsIndexServiceTests
    {
        [Fact]
        public void GetRoadsShouldReturnAllRoads()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRoads_Roads_IndexDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsIndexService(dbContext);

            var roads = new List<Road> { new Road { RoadName = "Road1" }, new Road { RoadName = "Road2" }, new Road { RoadName = "Road3" } };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var returnCollection = roadsService.GetAllRoads();

            Assert.Equal(roads.Count, returnCollection.Count);
        }

        [Fact]
        void GetLatestRoadShouldReturnRoadsOrderedByDateDescending()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetLatestRoads_Roads_IndexDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsIndexService(dbContext);

            var date = DateTime.UtcNow;
            var dateTwo = "12-12-1999 1:12:00AM";
            var parsedDateTwo = DateTime.Parse(dateTwo);

            var roads = new List<Road> { new Road { RoadName = "Road1", PostedOn = date }, new Road { RoadName = "Road2", PostedOn = date.AddDays(2) }, new Road { RoadName = "Road3", PostedOn = parsedDateTwo } };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var result = roadsService.GetLatestRoads();

            Assert.Equal(roads.OrderByDescending(x => x.PostedOn), result);
        }

        [Fact]
        public void GetLongestRoadsShouldReturnLongestRoads()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetLongestRoads_Roads_IndexDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsIndexService(dbContext);

            var roads = new List<Road> { new Road { RoadName = "Road1", RoadLength = 60 }, new Road { RoadName = "Road2", RoadLength = 80 }, new Road { RoadName = "Road3", RoadLength = 40 } };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var result = roadsService.GetLongestRoads();

            Assert.Equal(roads.OrderByDescending(x => x.RoadLength), result);
        }

        [Fact]
        public void GetTopRoadsShouldReturnRoadsWithMostAverateRating()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetTopRoads_Roads_IndexDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsIndexService(dbContext);

            var commentOne = new Comment { Content = "Loremipsium", Rating = 5 };
            var commentTwo = new Comment { Content = "Loremipsium", Rating = 3 };
            var commentThree = new Comment { Content = "Loremipsium", Rating = 4 };

            dbContext.Comments.AddRange(commentOne, commentTwo, commentThree);
            dbContext.SaveChanges();

            var roads = new List<Road>
            {
                new Road {RoadName = "Road1", Comments = new List<Comment>(){commentOne}},
                new Road {RoadName = "Road2", Comments = new List<Comment>(){commentTwo}},
                new Road {RoadName = "Road3", Comments = new List<Comment>(){commentThree}}
            };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var result = roadsService.GetTopRoads();

            Assert.Equal(roads.OrderByDescending(x => x.AverageRating), result);
        }

        [Fact]
        public void GetCurrentUserRoadsByIdShouldReturnCurrentUsersRoads()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCurrentUserRoads_Roads_IndexDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsIndexService(dbContext);

            var user = new User
            {
                UserName = "user",
                Roads = new List<Road>
                {
                    new Road(),
                    new Road(),
                    new Road(),
                }
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var result = roadsService.GetCurrentUserRoadsById(user.Id);

            Assert.Equal(user.Roads, result);

        }
    }
}
