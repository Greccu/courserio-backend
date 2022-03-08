using System.ComponentModel.DataAnnotations;

namespace Courserio.Core.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(30)]
        public string Name { get; set; }

        // Navigation Properties
        public virtual ICollection<Course> Courses { get; set; }
    }
}
