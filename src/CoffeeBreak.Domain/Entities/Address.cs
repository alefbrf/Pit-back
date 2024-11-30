using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeBreak.Domain.Entities
{
    public partial class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Neighborhood { get; set; }
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }
        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Number { get; set; }
        [MaxLength(50)]
        public string Complement { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
