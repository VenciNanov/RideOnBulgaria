using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RideOnBulgaria.Models;
using RideOnBulgaria.Web.Areas.Roads.Models;

namespace RideOnBulgaria.Web
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Road, DetailsRoadViewModel>()
                .ForMember(x => x.CoverPhoto,
                            c => c.MapFrom(x => x.CoverPhoto.Image))
                .ForMember(x=>x.Images,c=>c.MapFrom(x=>x.Photos))
                .ForMember(x=>x.PostedBy,c=>c.MapFrom(x=>x.User))
                .ForMember(x=>x.ViewRating,c=>c.MapFrom(x=>x.ViewRating))
                .ReverseMap();
        }
    }
}
