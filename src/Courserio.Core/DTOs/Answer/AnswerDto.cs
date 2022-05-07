using Courserio.Core.DTOs.User;

namespace Courserio.Core.DTOs.Answer
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtRelative { get; set; }
        public string Content { get; set; }
        public bool Anonymous { get; set; }

        public int QuestionId { get; set; }

        public UserDto User { get; set; }
    }
}
