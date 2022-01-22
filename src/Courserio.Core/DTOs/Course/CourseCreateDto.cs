using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.DTOs.Course
{
    public class CourseCreateDto
    {
        [Url, AllowNull]
        public string CoverImage { get; set; }
        [Url, AllowNull]
        public string MiniatureImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CreatorId { get; set; }
    }
}
