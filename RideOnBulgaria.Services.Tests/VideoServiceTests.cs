using System.Runtime.InteropServices;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class VideoServiceTests
    {
        [Fact]
        public void ReturnEmbedYoutubeLinkShouldReturnEmbedLink()
        {
            var videoService = new VideoService();

            var normalLink = "https://www.youtube.com/watch?v=ohOtDA3dTAA";
            var embedLink = "https://www.youtube.com/embed/ohOtDA3dTAA";

            var result = videoService.ReturnEmbedYoutubeLink(normalLink);

            Assert.Equal(embedLink, result);
        }
    }
}