namespace RideOnBulgaria.Models
{
    public class ProductImage
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}