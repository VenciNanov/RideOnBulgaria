using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RideOnBulgaria.Models
{
    public class Road
    {
        private const int AverageRatingCountDivider = 3;

        private double _averagePosterRating;
        private double _averageRating;


        public Road()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Photos = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
        }

        public string Id { get; set; }

        public string RoadName { get; set; }

        public string StartingPoint { get; set; }

        public string EndPoint { get; set; }

        public double RoadLength { get; set; }

        //public string CoverId { get; set; }
        //public virtual Image Cover { get; set; }

        public string CoverPhotoId { get; set; }
        public virtual CoverPhotoRoad CoverPhoto { get; set; }

        public virtual ICollection<Image> Photos { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public DateTime PostedOn { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double AveragePosterRating
        {
            get
            {
                int sum = ViewRating + SurfaceRating + PleasureRating;
                double average = (double)sum / AverageRatingCountDivider;
                return _averagePosterRating = average;
            }
            private set => _averagePosterRating = value;
        }


        public int ViewRating { get; set; }

        public int SurfaceRating { get; set; }

        public int PleasureRating { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        //public virtual ICollection<Reply> Replies { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double AverageRating
        {
            get
            {
                var sum = this.Comments.Sum(x => x.Rating);
                if (sum==0||Comments.Count==0)
                {
                    return _averageRating;
                }
                var rating = sum / Comments.Count;

                return _averageRating = rating;
            }
            private set => _averageRating = value;
        }


    }
}
