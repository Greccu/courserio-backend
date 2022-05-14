using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courserio.Core.Models
{
    public class Course : BaseEntity
    {
        [Url]
        public string CoverImage { get; set; }
        [Url]
        public string MiniatureImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }

        // Navigation Properties
        [InverseProperty("CreatedCourses")]
        public virtual User Creator { get; set; }
        [InverseProperty("FeaturedCourse")]
        public virtual ICollection<User> FeaturingUsers { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
