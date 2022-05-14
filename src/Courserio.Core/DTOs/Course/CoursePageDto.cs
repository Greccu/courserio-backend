using Courserio.Core.DTOs.Chapter;
using Courserio.Core.DTOs.Tags;
using Courserio.Core.DTOs.User;

namespace Courserio.Core.DTOs.Course
{
    public class CoursePageDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtRelative { get; set; }
        public string CoverImage { get; set; }
        public string MiniatureImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public int UserRating { get; set; }

        public UserDto Creator { get; set; }

        public ICollection<ChapterListDto> Chapters { get; set; }
        public ICollection<TagDto> Tag { get; set; }
    }
}
