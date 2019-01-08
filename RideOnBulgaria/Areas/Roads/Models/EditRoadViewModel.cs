using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class EditRoadViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Добави име първо")]
        [Display(Name = "Име на трасето")]
        [DataType(DataType.Text)]
        public string RoadName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Начална точка")]
        [DataType(DataType.Text)]
        public string StartingPoint { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Крайна точка")]
        [DataType(DataType.Text)]
        public string EndPoint { get; set; }

        [Display(Name = "Дължина")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid road length.")]
        public double RoadLength { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Информация за трасето")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url] [Display(Name = "Видео")] public string Video { get; set; }

        [Display(Name = "Основна снимка(Провлечи и пусни снимката тук)")]
        public IFormFile CoverPhoto { get; set; }

        [Display(Name = "Снимки(Провлечи и пусни снимките тук)")]
        [DataType(DataType.Upload)]
        public ICollection<IFormFile> NewImages { get; set; }

        public ICollection<Image> Images { get; set; }

        public DateTime PostedOn { get; set; }

        [Display(Name = "")] public int ViewRating { get; set; }

        [Display(Name = "")] public int SurfaceRating { get; set; }

        [Display(Name = "")] public int PleasureRating { get; set; }
    }
}