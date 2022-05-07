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
        public string DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public string PhoneNumber { get; set; }

        public virtual CourseFeaturedDto FeaturedCourse { get; set; }
        public virtual ICollection<CourseListDto> CreatedCourses { get; set; }
        public virtual ICollection<CourseListDto> RatedCourses { get; set; }
    }
}
