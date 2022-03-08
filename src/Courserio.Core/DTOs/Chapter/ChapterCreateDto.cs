using Courserio.Core.Enums;

namespace Courserio.Core.DTOs.Chapter
{
    public class ChapterCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //
        public ChapterTypeEnum Type { get; set; }
        public string VideoUrl { get; set; }
        public string Content { get; set; }

        //
        public int CourseId { get; set; }
    }
}
