using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Models
{
    public class Answer
    {
        //
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required, MinLength(2), MaxLength(100)]
        public string Title { get; set; }
        [Required, MinLength(2)]
        public string Content { get; set; }
        public bool Anonymous { get; set; }
        
        //
        public int UserId { get; set; }
        public int QuestionId { get; set; }

        // Navigation Properties
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
