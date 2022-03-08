using Courserio.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Courserio.Core.Models
{
    public class RoleApplication
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int RoleId { get; set; }
        public int UserId { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        [Required, MinLength(2)]
        public string Content { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
