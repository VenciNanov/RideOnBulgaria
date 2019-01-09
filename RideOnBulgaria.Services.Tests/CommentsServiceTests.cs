using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class CommentsServiceTests
    {
        [Fact]
        public void AddCommentToRoadShouldAddCommentToTheGivenRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddCommentToRoad_Comments_Roads_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);


            var user = new User
            {
                UserName = "user1",
                Roads = new List<Road>
                {
                    new Road
                    {
                        Id = "RoadId1",
                        RoadName = "Lorem",
                    }
                }
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var result = commentsService.AddCommentToRoad("RoadId1", user, 4, "none");

            Assert.Single(dbContext.Comments);
            Assert.True(result);
        }

        [Fact]
        public void GetCommentsByRoadIdShouldReturnAllCommentsOfGivenRoad()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCommentsByRoadId_Comments_Roads_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);

            var road = new Road
            {
                Id = "Road1",
                RoadName = "Lorem",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Content = "comment1"
                    },
                    new Comment
                    {
                        Content = "comment2"
                    }
                }
            };
            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var comments = commentsService.GetCommentsByRoadId(road.Id);

            Assert.Equal(2, comments.Count);
        }

        [Fact]
        public void GetCommentsByRoadIdShouldNotReturnCommentsWhenInvalidRoadIsGiven()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCommentsByRoadId_Comments_Roads_Db")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);

            var road = new Road
            {
                Id = "Road1",
                RoadName = "Lorem",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Content = "comment1"
                    },
                    new Comment
                    {
                        Content = "comment2"
                    }
                }
            };
            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var comments = commentsService.GetCommentsByRoadId("road12");

            Assert.Null(comments);
        }

        [Fact]
        public void GetCommentByIdShouldReturnComment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCommentId_Roads_Comments_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);

            var road = new Road
            {
                Id = "Road1",
                RoadName = "Lorem",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id="commentId1",
                        Content = "comment1"
                    },
                    new Comment
                    {
                        Id = "commentId2",
                        Content = "comment2"
                    }
                }
            };
            dbContext.Roads.Add(road);
            dbContext.SaveChanges();

            var comment = commentsService.GetCommentById("commentId1");

            Assert.Equal("comment1", comment.Content);
            Assert.NotNull(comment);
        }

        [Fact]
        public void AddReplyToCommentShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddReplyToComment_Comments_Roads_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);

            var user = new User
            {
                UserName = "User1"
            };
            var comment = new Comment
            {
                Content = "comment1",
                Id = "commentId1",
                Replies = new List<Reply>(),
                User = user
            };
            dbContext.Users.Add(user);
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
            var result = commentsService.AddReplyToComment("commentId1", "reply1", user);

            Assert.True(result);
            Assert.Equal(1, comment.Replies.Count);
        }

        [Fact]
        public void DeleteCommentAndItsRepliesShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCommentAndItsReplies_Comments_Roads_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);

            var user = new User
            {
                UserName = "User1"
            };
            var comment = new Comment
            {
                Content = "comment1",
                Id = "commentId1",
                Replies = new List<Reply>
                {
                    new Reply
                    {
                        Id = "replyId1",
                    }
                },
                User = user
            };
            dbContext.Users.Add(user);
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();

            var result = commentsService.DeleteCommentAndItsReplies(comment.Id);

            Assert.True(result);
            Assert.True(comment.IsDeleted);
        }

        [Fact]
        public void DeleteReplyByIdShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteReplyById_Comments_Roads_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var roadsService = new RoadsService(dbContext, null, null, null, null, null);

            var commentsService = new CommentsService(roadsService, dbContext);


            var reply = new Reply
            {
                Id = "replyId1",
            };
            dbContext.Replies.Add(reply);
            dbContext.SaveChanges();

            var result = commentsService.DeleteReply(reply.Id);

          //  Assert.Empty(dbContext.Replies);
            Assert.True(result);
        }
    }
}