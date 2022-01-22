using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Url]
        public string CoverImage { get; set; }
        [Url]
        public string MiniatureImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }


        // Navigation Properties
        [InverseProperty("CreatedCourses")]
        public virtual User Creator { get; set; }
        public virtual ICollection<User> Users { get; set; }
        //public virtual ICollection<Tag> Tags { get; set; }
        //public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
