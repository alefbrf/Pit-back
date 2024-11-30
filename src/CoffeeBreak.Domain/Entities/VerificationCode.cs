using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBreak.Domain.Entities
{
    public partial class VerificationCode
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(6)]
        public string Code { get; set; }
        public DateTime Valid { get; set; }
        [Column("User_Id")]
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
