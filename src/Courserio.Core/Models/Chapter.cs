using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Models
{
    public class Chapter
    {
        [Key]
        public int Id { get; set; }
        public int? Number { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Url]
        public string VideoUrl { get; set; }
        public int SectionId { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
