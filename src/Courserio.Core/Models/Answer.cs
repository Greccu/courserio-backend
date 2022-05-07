using System.ComponentModel.DataAnnotations;

namespace Courserio.Core.Models
{
    public class Answer : BaseEntity 
    {
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
