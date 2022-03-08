using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.DTOs.Question
{
    public class QuestionCreateDto
    {
        public string Content { get; set; }
        public bool Anonymous { get; set; }

        public int ChapterId { get; set; }
        public int UserId { get; set; }
    }
}
