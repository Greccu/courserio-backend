using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Title { get; set; }
        [Required, MinLength(2)]
        public string Content { get; set; }
        public DateTime CretedAt { get; set; }
        public bool Anonymous { get; set; }
        public string UserId { get; set; }
        public int ChapterId { get; set; }

        // Navigation Properties
        public virtual Chapter Chapter { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
