﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.DTOs.Rating
{
    public class RatingCreateDto
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }
    }
}
