using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.DTOs.Course;

namespace Courserio.Core.DTOs.Rating
{
    public class RatingCourseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtRelative { get; set; }
        public int Value { get; set; }

        public CourseDto Course { get; set; }
    }
}
