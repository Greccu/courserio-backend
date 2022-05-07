using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;

namespace Courserio.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<List<CourseListDto>> GetHomeAsync();
        Task<List<CourseListDto>> GetRecommendedAsync(string username);
        Task<PagedResult<CourseListDto>> ListAsync(CourseFilter courseFilter);
        Task CreateAsync(CourseCreateDto courseDto);
        Task<CoursePageDto> GetByIdAsync(int id, string username);
        Task UpdateRatingsAsync();
    }
}
