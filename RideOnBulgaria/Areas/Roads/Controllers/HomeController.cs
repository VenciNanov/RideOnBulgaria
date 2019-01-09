using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Roads.Models;
using RideOnBulgaria.Web.Areas.Roads.Models.Comments;
using RideOnBulgaria.Web.Areas.Roads.Models.Comments.Replies;
using RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex;
using RideOnBulgaria.Web.Common;
using RoadViewModel = RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex.RoadViewModel;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    [Area(Constants.RoadsArea)]
    public class HomeController : Controller
    {
        private readonly IRoadsService roadsService;
        private readonly IImageService imageService;
        private readonly IRoadsIndexService roadsIndexService;
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IMapper mapper;

        public HomeController(IRoadsService roadsService, IImageService imageService, IRoadsIndexService roadsIndexService, IUsersService usersService, ICommentsService commentsService, IMapper mapper)
        {
            this.roadsService = roadsService;
            this.imageService = imageService;
            this.roadsIndexService = roadsIndexService;
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            RoadsIndexViewModel model = MapCategories();

            return View(model);
        }



        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateRoadViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.roadsService.Create(model.TripName, model.StartingPoint, model.EndPoint, model.TripLength, model.Description,
                   model.Video, userId, model.CoverPhoto, model.Images, model.View, model.Surface, model.Pleasure);

            if (!result)
            {
                return this.RedirectToAction("Error");
            }


            return this.RedirectToAction("All", "Categories");
        }

        [Authorize]
        public IActionResult EditRoad(string id)
        {
            var road = this.roadsService.GetRoadById(id);

            if (road == null)
            {
                //TODO
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (!this.User.IsInRole(Constants.AdminRole))
            {
                if (!this.User.IsInRole(Constants.OwnerRole))
                {

                    if (userId != road.UserId)
                    {
                        return Unauthorized();
                    }

                }
            }


            var model = mapper.Map<EditRoadViewModel>(road);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditRoad(EditRoadViewModel model)
        {
            var road = this.roadsService.GetRoadById(model.Id);

            if (road == null) return NotFound();


            var result = this.roadsService.Edit(model.Id, model.RoadName, model.StartingPoint, model.EndPoint,
                model.RoadLength, model.Description, model.Video, model.CoverPhoto, model.ViewRating, model.SurfaceRating,
                model.PleasureRating);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult EditRoadPictures(string id)
        {
            var road = this.roadsService.GetRoadById(id);

            var model = mapper.Map<EditRoadViewModel>(road);

            //var model = new EditRoadViewModel
            //{
            //    Id = road.Id,
            //    Images = road.Photos
            //};


            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddImagesToRoad(EditRoadViewModel model)
        {
            var road = this.roadsService.GetRoadById(model.Id);

            if (model.NewImages == null)
            {
                return RedirectToAction("Error");
            }

            var images = this.roadsService.AddImagesToRoad(model.NewImages, road.Id);

            return this.Redirect($"/Roads/Home/EditRoadPictures/{road.Id}");

        }

        [Authorize]
        public IActionResult DeleteRoadPictures(string id, string roadId)
        {
            this.imageService.RemoveImage(id);

            return this.Redirect($"/Roads/Home/EditRoadPictures/{roadId}");
        }

        [Authorize]
        public IActionResult DeleteRoad(string id)
        {
            var user = this.usersService.GetUserByUsername(this.User.Identity.Name);
            var road = roadsService.GetRoadById(id);

            if (road == null)
            {
                return NotFound();
            }

            if (!this.User.IsInRole(Constants.AdminRole))
            {
                if (!this.User.IsInRole(Constants.OwnerRole))
                {

                    if (user.Id != road.UserId)
                    {
                        return Unauthorized();
                    }

                }
            }

            bool result = roadsService.DeleteRoad(id);

            if (result == false)
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("DeltedRoadSuccesfully", "Home");
        }

        public IActionResult DeltedRoadSuccesfully()
        {
            return this.View();
        }

        public IActionResult Road(string id)
        {
            var model = this.roadsService.Details<DetailsRoadViewModel>(id);

            if (model == null)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult MyRoads()
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserRoads = this.roadsIndexService.GetCurrentUserRoadsById(currentUser);

            var model = new List<RoadViewModel>();

            foreach (var road in currentUserRoads)
            {
                var coverPhoto = this.imageService.ReturnImage(road.CoverPhoto.Image);
                var token = mapper.Map<RoadViewModel>(road);
                token.CoverPhoto = coverPhoto;
                model.Add(token);
            }
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Comment(string id, DetailsRoadViewModel model)
        {
            var commentViewModel = model.Comment;
            commentViewModel.Commentator = this.usersService.GetUserByUsername(this.User.Identity.Name);
            commentViewModel.RoadId = id;

            var result = this.commentsService.AddCommentToRoad(id, commentViewModel.Commentator, commentViewModel.Rating,
                 commentViewModel.Comment);

            if (result == false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Road", "Home", new { @id = id });
        }


        [HttpPost]
        [Authorize]
        public IActionResult ReplyToComment(string id, string roadId, DetailsRoadViewModel model)
        {
            var replyViewModel = model.Reply;
            var user = this.usersService.GetUserByUsername(this.User.Identity.Name);

            var result = this.commentsService.AddReplyToComment(id, replyViewModel.Content, user);

            return this.RedirectToAction("Road", "Home", new { @id = roadId });
        }

        [Authorize]
        public IActionResult DeleteComment(string id, string roadId)
        {
            var result = this.commentsService.DeleteCommentAndItsReplies(id);

            if (result == false)
            {
                //TODO
                return this.NotFound();

            }

            return this.RedirectToAction("Road", "Home", new { @id = roadId });
        }

        [Authorize]
        public IActionResult DeleteReply(string id, string roadId)
        {
            var result = this.commentsService.DeleteReply(id);

            if (result == false)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Road", "Home", new { @id = roadId });
        }

        private RoadsIndexViewModel MapCategories()
        {
            var model = new RoadsIndexViewModel
            {
                AllRoads = MapAllRoads(this.roadsIndexService.GetAllRoads().ToList()),
                LatestRoads = MapAllRoads(this.roadsIndexService.GetLatestRoads().ToList()),
                LongestRoads = MapAllRoads(this.roadsIndexService.GetLongestRoads().ToList()),
                TopRoads = MapAllRoads(this.roadsIndexService.GetTopRoads().ToList()),
            };

            return model;
        }

        private List<RoadViewModel> MapAllRoads(List<Road> roads)
        {
            var roadsModel = new List<RoadViewModel>();

            foreach (var road in roads)
            {
                var coverPhoto = this.imageService.ReturnImage(road.CoverPhoto.Image);
                var token = mapper.Map<RoadViewModel>(road);
                token.CoverPhoto = coverPhoto;
                roadsModel.Add(token);
            }

            return roadsModel;
        }
    }
}