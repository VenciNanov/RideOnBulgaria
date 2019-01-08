using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class VideoService : IVideoService
    {
        private const string RegexPattern =
            "(?:https?:\\/\\/)?(?:www\\.)?(?:(?:(?:youtube.com\\/watch\\?[^?]*v=|youtu.be\\/)([\\w\\-]+))(?:[^\\s?]+)?)";

        private const string Target = "https://www.youtube.com/embed/$1";

        public string ReturnEmbedYoutubeLink(string url)
        {
            var rgx = new Regex(RegexPattern);
            var result = rgx.Replace(url, Target);

            return result;
        }
    }
}
