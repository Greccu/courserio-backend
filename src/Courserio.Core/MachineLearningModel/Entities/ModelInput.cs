using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Courserio.Core.MachineLearningModel.Entities
{
    public class ModelInput
    {
        [ColumnName(@"userId")]
        public int UserId { get; set; }

        [ColumnName(@"courseId")]
        public int CourseId { get; set; }

        [ColumnName(@"user-rating")]
        public float UserRating { get; set; }

        [ColumnName(@"rating")]
        public float Rating { get; set; }

        [ColumnName(@"rating-count")]
        public float RatingCount { get; set; }
    }
}
