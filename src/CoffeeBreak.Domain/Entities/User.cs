using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoffeeBreak.Domain.Entities
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        public Byte Role { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public DateTime DateUpdated { get; set; }
        [JsonIgnore]
        public bool Verified { get; set; }
        [JsonIgnore]
        public virtual ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        [JsonIgnore]
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        [JsonIgnore]
        public virtual ICollection<ProductCart> ProductsCart { get; set; } = new List<ProductCart>();
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        [JsonIgnore]
        public virtual ICollection<Order> DeliveryOrders { get; set; } = new List<Order>();
    }
}
