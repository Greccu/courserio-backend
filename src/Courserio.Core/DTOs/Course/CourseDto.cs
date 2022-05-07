using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.DTOs.Course
{
    public class CourseDto
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
    }
}
