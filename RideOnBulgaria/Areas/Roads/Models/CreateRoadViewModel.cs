using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class CreateRoadViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Road Name")]
        [DataType(DataType.Text)]
        public string TripName { get; set; }

        [Required]
        [Display(Name = "Starting Point")]
        [DataType(DataType.Text)]
        public string StartingPoint { get; set; }

        [Required]
        [Display(Name = "End Point")]
        [DataType(DataType.Text)]
        public string EndPoint { get; set; }

        [Required]
        [Display(Name = "Road Length")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid road length.")]
        public double TripLength { get; set; }

        //public virtual ICollection<Image> Photos { get; set; }

        [Required]
        [Display(Name = "Description about the road")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url]
        [Display(Name = "Video")]
        public string Video { get; set; }

        [Display(Name = "CoverPhoto(Drag and drop your image here...)")]
        public IFormFile CoverPhoto { get; set; }

        [Display(Name = "Photos(Drag and drop your images here...)")]
        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Images { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
