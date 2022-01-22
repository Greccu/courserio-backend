using Courserio.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Models
{
    public class RoleApplication
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserId { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        [Required, MinLength(2)]
        public string Content { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
