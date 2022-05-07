using Courserio.Core.DTOs.Answer;
using Courserio.Core.DTOs.User;

namespace Courserio.Core.DTOs.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtRelative { get; set; }
        public string Content { get; set; }
        public bool Anonymous { get; set; }

        public int ChapterId { get; set; }

        public UserDto User { get; set; }
        public virtual ICollection<AnswerDto> Answers { get; set; }

    }
}
