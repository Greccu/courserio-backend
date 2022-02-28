﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Courserio.Core.Enums;

namespace Courserio.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string KeycloakId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        [Url]
        public string ProfilePicture { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public string PhoneNumber { get; set; }
        //
        public int? FeaturedCourseId { get; set; } = null;
        public int RoleId { get; set; }


        // Navigation Properties
        public virtual Role Role { get; set; }
        public virtual Course FeaturedCourse { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<Course> CreatedCourses { get; set; }
        [InverseProperty("Users")]
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<RoleApplication> RoleApplications { get; set; }
    }
}
