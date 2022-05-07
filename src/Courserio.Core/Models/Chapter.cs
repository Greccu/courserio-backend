using Courserio.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Courserio.Core.Models
{
    public class Chapter : BaseEntity
    {
        public int OrderNumber { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        //
        public ChapterTypeEnum Type { get; set; }
        [Url]
        [AllowNull]
        public string VideoUrl { get; set; }
        public string Content { get; set; }

        //
        public int CourseId { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
