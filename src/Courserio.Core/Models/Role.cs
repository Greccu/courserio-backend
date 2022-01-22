using System.ComponentModel.DataAnnotations;

namespace Courserio.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RoleApplication> RoleApplications { get; set; }
    }
}
