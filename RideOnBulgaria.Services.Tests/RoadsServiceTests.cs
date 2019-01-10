using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using Xunit;
namespace RideOnBulgaria.Services.Tests
{
    public class RoadsServiceTests
    {
        [Fact]
        public void GetRoadRoadByIdShouldReturnRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRoadsById_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var road = new Road
            {
                RoadName = "Lorem"
            };

            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var result = roadsService.GetRoadById(road.Id);

            Assert.Equal(road, result);
        }

        [Fact]
        public void CreateRoadShouldCreateRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateRoad_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new Mock<IImageService>();
            imageService.Setup(x => x.AddPhoto(It.IsAny<IFormFile>())).Returns(new Image
            {
                PublicId = "PublicId",
                Name = "123",
                DateAdded = DateTime.UtcNow,
                Id = Convert.ToString(Guid.NewGuid()),
                ImageUrl = "http://www.url.com/",
            });

            var roadsService = new RoadsService(dbContext, imageService.Object, null, null, null, null);

            var roadResult = roadsService.Create("Lorem", "Ips", "ium", 1, "Lorem Ipsium", null, "userId", GenerateFile(),
                GenerateFiles(), 1, 1, 1);
            Assert.True(roadResult);

        }

        [Fact]
        public void EditRoadShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditRoad_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new Mock<IImageService>();
            imageService.Setup(x => x.AddPhoto(It.IsAny<IFormFile>())).Returns(new Image
            {
                PublicId = "PublicId",
                Name = "123",
                DateAdded = DateTime.UtcNow,
                Id = Convert.ToString(Guid.NewGuid()),
                ImageUrl = "http://www.ur;.com/",
            });

            var road = new Road
            {
                RoadName = "LoremIpsium1",
            };

            dbContext.Roads.Add(road);
            dbContext.SaveChanges();



            var roadsService = new RoadsService(dbContext, imageService.Object, null, null, null, null);

            var editResult = roadsService.Edit(road.Id, "ipsum", "lorem", "ips", 60, "big description", null, GenerateFile(), 1, 1, 1);

            Assert.True(editResult);
        }

        [Fact]
        public void GetRoadsShouldReturnAllRoads()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRoads_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var roads = new List<Road> { new Road { RoadName = "Road1" }, new Road { RoadName = "Road2" }, new Road { RoadName = "Road3" } };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var returnCollection = roadsService.GetRoads();

            Assert.Equal(returnCollection.Count, roads.Count);
        }

        [Fact]
        void GetLatestRoadShouldReturnRoadsOrderedByDateDescending()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetLatestRoads_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var date = DateTime.UtcNow;
            var dateTwo = "12-12-1999 1:12:00AM";
            var parsedDateTwo = DateTime.Parse(dateTwo);

            var roads = new List<Road> { new Road { RoadName = "Road1", PostedOn = date }, new Road { RoadName = "Road2", PostedOn = date.AddDays(2) }, new Road { RoadName = "Road3", PostedOn = parsedDateTwo } };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var result = roadsService.GetLatestRoads();

            Assert.Equal(result, roads.OrderByDescending(x => x.PostedOn));
        }

        [Fact]
        public void GetLongestRoadsShouldReturnLongestRoads()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetLongestRoads_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var roads = new List<Road> { new Road { RoadName = "Road1", RoadLength = 60 }, new Road { RoadName = "Road2", RoadLength = 80 }, new Road { RoadName = "Road3", RoadLength = 40 } };

            dbContext.Roads.AddRange(roads);
            dbContext.SaveChanges();

            var result = roadsService.GetLongestRoads();

            Assert.Equal(result, roads.OrderByDescending(x => x.RoadLength));
        }

        [Fact]
        public void GetTopRoadsShouldReturnRoadsWithMostAverateRating()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetTopRoads_Roads_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

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

            Assert.Equal(result, roads.OrderByDescending(x => x.AverageRating));
        }

        [Fact]
        public void GetRoadByImageShouldReturnRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRoadByImage_Roads_Image_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);
            var image = new Image();

            dbContext.Images.Add(image);
            dbContext.SaveChanges();

            var road = new Road
            {
                RoadName = "roadOne",
                Photos = new List<Image>() { image }
            };
            image.Road = road;
            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var result = roadsService.GetRoadByImage(image);

            Assert.Equal(road, result);
        }

        [Fact]
        public void AddImagesToRoadShouldAddImagesToTheGivenRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddImageToRoad_Road_Image_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new Mock<IImageService>();
            imageService.Setup(x => x.AddPhoto(It.IsAny<IFormFile>())).Returns(new Image
            {
                PublicId = "PublicId",
                Name = "123",
                DateAdded = DateTime.UtcNow,
                Id = Convert.ToString(Guid.NewGuid()),
                ImageUrl = "http://www.url.com/",
            });


            var roadsService = new RoadsService(dbContext, imageService.Object, null, null, null, null);


            var road = new Road
            {
                RoadName = "RoadOne"
            };
            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var images = GenerateFiles();

            var result = roadsService.AddImagesToRoad(images, road.Id);

            Assert.True(result);
        }


        [Fact]
        public void DeleteRoadShouldHardDeleteRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteRoad_Road_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var road = new Road()
            {
                RoadName = "Road"
            };

            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var result = roadsService.DeleteRoad(road.Id);

            Assert.True(result);
        }

        [Fact]
        public void DeleteRoadShouldNotHardDeleteRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteRoad_Road_Db")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var road = new Road()
            {
                RoadName = "Road"
            };



            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            dbContext.Roads.Remove(road);
            dbContext.SaveChanges();

            var result = roadsService.DeleteRoad(road.Id);

            Assert.False(result);
        }

        private IFormFile GenerateFile()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            return file;
        }


        private ICollection<IFormFile> GenerateFiles()
        {
            var files = new List<IFormFile>();

            for (int i = 0; i < 10; i++)
            {
                var fileMock = new Mock<IFormFile>();
                //Setup mock file using a memory stream
                var content = "content";
                var fileName = "fileName" + i + ".jpg";
                var ms = new MemoryStream();
                var writer = new StreamWriter(ms);
                writer.Write(content);
                writer.Flush();
                ms.Position = 0;
                fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                fileMock.Setup(_ => _.FileName).Returns(fileName);
                fileMock.Setup(_ => _.Length).Returns(ms.Length);
                fileMock.Setup(_ => _.ContentDisposition).Returns($"inline; filename={fileName}");

                var file = fileMock.Object;
                files.Add(file);
            }

            return files;
        }
    }
}