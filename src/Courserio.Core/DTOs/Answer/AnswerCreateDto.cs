using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.DTOs.Answer
{
    public class AnswerCreateDto
    {
        public string Content { get; set; }
        public bool Anonymous { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
