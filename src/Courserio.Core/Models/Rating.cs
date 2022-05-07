using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Models
{
    public class Rating : BaseEntity
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
