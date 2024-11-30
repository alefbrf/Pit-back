using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeBreak.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }
        public bool IsDelivery { get; set; }
        [MaxLength(400)]
        public string Address { get; set; }
        public decimal Price { get; set; }
        [MaxLength(200)]
        public string Observation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Disapproved { get; set; }
        public DateTime? Preparing { get; set; }
        public DateTime? Ready { get; set; }
        public DateTime? InDelivery { get; set; }
        public DateTime? Delivered { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        [Column("Delivery_Man_Id")]
        public int? DeliveryManId { get; set; }
        [ForeignKey(nameof(DeliveryManId))]
        public virtual User? DeliveryMan { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
