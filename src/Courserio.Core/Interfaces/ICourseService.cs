using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;

namespace Courserio.Core.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseListDto>> GetHomeAsync();
        Task<List<CourseListDto>> ListAsync(CourseFilter courseFilter);
        Task CreateAsync(CourseCreateDto courseDto);
    }
}
