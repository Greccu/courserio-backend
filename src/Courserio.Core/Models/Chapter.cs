﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.Enums;

namespace Courserio.Core.Models
{
    public class Chapter
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int OrderNumber { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        //
        public ChapterTypeEnum Type { get; set; }
        [Url]
        [AllowNull]
        public string VideoUrl { get; set; }
        public string Content { get; set; }

        //
        public int CourseId { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
