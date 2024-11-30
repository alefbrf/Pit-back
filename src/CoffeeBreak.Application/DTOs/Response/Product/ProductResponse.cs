namespace CoffeeBreak.Application.DTOs.Response.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool Favorite { get; set; }
        public int Quantity { get; set; }
    }
}
