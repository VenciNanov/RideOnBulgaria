using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RideOnBulgaria.Models;
using RideOnBulgaria.Web.Areas.Administration.Models;
using RideOnBulgaria.Web.Areas.Administration.Models.Roads;
using RideOnBulgaria.Web.Areas.Administration.Models.Users;
using RideOnBulgaria.Web.Areas.Roads.Models;
using RideOnBulgaria.Web.Areas.Roads.Models.Comments;
using RideOnBulgaria.Web.Areas.Shop.Models;

namespace RideOnBulgaria.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Comment, AllCommentsViewModel>()
                .ForMember(x => x.Id, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.User, c => c.MapFrom(x => x.User))
                .ForMember(x => x.Content, c => c.MapFrom(x => x.Content))
                .ForMember(x => x.Rating, c => c.MapFrom(x => x.Rating))
                .ReverseMap();

            CreateMap<Road, DetailsRoadViewModel>()
                .ForMember(x => x.CoverPhoto,
                            c => c.MapFrom(x => x.CoverPhoto.Image))
                .ForMember(x => x.Images, c => c.MapFrom(x => x.Photos))
                .ForMember(x => x.PostedBy, c => c.MapFrom(x => x.User))
                .ForMember(x => x.ViewRating, c => c.MapFrom(x => x.ViewRating))
                .ForMember(x=>x.CommentsViewModel,c=>c.MapFrom(x=>x.Comments))
                .ReverseMap();


            CreateMap<Product, CartProductsViewModel>()
                .ForMember(x => x.Id, c => c.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<OrderProduct, OrderDetailsViewModel>()
                .ForMember(x => x.Id, c => c.MapFrom(x => x.ProductId))
                .ForMember(x => x.Name, c => c.MapFrom(x => x.Product.Name))
                .ForMember(x => x.PriceForOne, c => c.MapFrom(x => x.Product.Price))
                .ForMember(x => x.Quantity, c => c.MapFrom(x => x.Quantity))
                .ForMember(x => x.Total, c => c.MapFrom(x => x.Quantity * x.Product.Price))
                .ReverseMap();

            CreateMap<Road, RoadsViewModel>()
                .ForMember(x => x.Name, c => c.MapFrom(x => x.RoadName))
                .ForMember(x => x.PostedBy, c => c.MapFrom(x => x.User))
                .ForMember(x => x.Comments, c => c.MapFrom(x => x.SurfaceRating))
                .ForMember(x => x.PosterRating, c => c.MapFrom(x => x.AveragePosterRating))
                .ForMember(x => x.Rating, c => c.MapFrom(x => x.AveragePosterRating))
                .ReverseMap();
        }
    }
}
