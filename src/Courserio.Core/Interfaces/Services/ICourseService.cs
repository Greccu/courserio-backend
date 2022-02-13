using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;

namespace Courserio.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<List<CourseListDto>> GetHomeAsync();
        Task<List<CourseListDto>> ListAsync(CourseFilter courseFilter);
        Task CreateAsync(CourseCreateDto courseDto);
    }
}
