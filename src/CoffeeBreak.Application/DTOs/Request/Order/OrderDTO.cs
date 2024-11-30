namespace CoffeeBreak.Application.DTOs.Request.Order
{
    public class OrderDTO
    {
        public bool IsDelivery { get; set; }
        public int? AddressId { get; set; }
        public string Observation { get; set; }
        public List<OrderProduct> products { get; set; }
        public class OrderProduct { 
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }
}
