using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeBreak.Domain.Entities
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        [Column("Order_Id")]
        public int? OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public virtual Order? Order { get; set; }
        [Column("Product_Id")]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public virtual Product Product { get; set; }
        public int ProductCount { get; set; }
    }
}
