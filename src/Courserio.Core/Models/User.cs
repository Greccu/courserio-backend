using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courserio.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Url]
        public string ProfilePicture { get; set; }


        // Navigation Properties
        [InverseProperty("Creator")]
        public virtual ICollection<Course> CreatedCourses { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
        //public virtual ICollection<Question> Questions { get; set; }
        //public virtual ICollection<Answer> Answers { get; set; }
        //public virtual ICollection<RoleApplication> RoleApplications { get; set; }
    }
}
