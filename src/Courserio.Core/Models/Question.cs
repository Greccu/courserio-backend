using System.ComponentModel.DataAnnotations;

namespace Courserio.Core.Models
{
    public class Question : BaseEntity
    {
        [Required, MinLength(1)]
        public string Content { get; set; }
        public bool Anonymous { get; set; }
        public int UserId { get; set; }
        public int ChapterId { get; set; }

        // Navigation Properties
        public virtual Chapter Chapter { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
