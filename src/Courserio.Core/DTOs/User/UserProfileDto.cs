using System.ComponentModel.DataAnnotations.Schema;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Enums;

namespace Courserio.Core.DTOs.User
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public string PhoneNumber { get; set; }
        
        public virtual CoursePageDto FeaturedCourse { get; set; }
        public virtual ICollection<CourseListDto> CreatedCourses { get; set; }
        public virtual ICollection<CourseListDto> Courses { get; set; }
    }
}
