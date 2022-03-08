using Courserio.Core.DTOs.Chapter;
using Courserio.Core.DTOs.User;

namespace Courserio.Core.DTOs.Course
{
    public class CoursePageDto
    {
        public int Id { get; set; }
        public string CoverImage { get; set; }
        public string MiniatureImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto Creator { get; set; }

        public IEnumerable<ChapterListDto> Chapters { get; set; }
    }
}
