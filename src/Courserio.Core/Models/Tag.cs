using System.ComponentModel.DataAnnotations;

namespace Courserio.Core.Models
{
    public class Tag : BaseEntity
    {
        [Required, MinLength(2), MaxLength(30)]
        public string Name { get; set; }

        // Navigation Properties
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
