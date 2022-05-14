 using AutoMapper;
using Courserio.Core.Constants;
using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Middlewares.ExceptionMiddleware.CustomExceptions;
using Courserio.Core.Models;
using Courserio.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public CourseService(IGenericRepository<Course> courseRepository, IMapper mapper, IGenericRepository<User> userRepository, IGenericRepository<Tag> tagRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }

        public async Task<List<CourseListDto>> GetHomeAsync()
        {
            return (await _courseRepository
                .AsQueryable()
                .OrderByDescending(x => x.AverageRating)
                .Take(4)
                .Include(x => x.Creator)
                .ToListAsync())
                .Select(_mapper.Map<CourseListDto>).ToList();
        }

        public async Task<List<CourseListDto>> GetRecommendedAsync(string username)
        {
            return (await _courseRepository
                .AsQueryable()
                .OrderByDescending(x => x.AverageRating)
                .Take(4)
                .Include(x => x.Creator)
                .ToListAsync())
                .Select(_mapper.Map<CourseListDto>).ToList();
        }

        public async Task<PagedResult<CourseListDto>> ListAsync(CourseFilter courseFilter)
        {
            var query = _courseRepository.AsQueryable()
                .Include(x => x.Creator).AsQueryable();

            if (!string.IsNullOrEmpty(courseFilter.Title))
            {
                query = query.Where(x => x.Title.Contains(courseFilter.Title));
            }

            if (!string.IsNullOrEmpty(courseFilter.OrderBy))
            {
                switch (courseFilter.OrderBy.ToLower())
                {
                    case "new":
                        query = query.OrderByDescending(x => x.CreatedAt);
                        break;
                    case "rating":
                        query = query.OrderByDescending(x => x.AverageRating);
                        break;
                    case "popularity":
                        query = query.OrderByDescending(x => x.RatingsCount);
                        break;
                }
            }
            
            var result = await query.MapToPagedResultAsync(courseFilter, _mapper.Map<CourseListDto>);
            return result;
        }

        public async Task CreateAsync(CourseCreateDto courseDto)
        {

            var course = _mapper.Map<Course>(courseDto);
            course.CoverImage ??= CourseConstants.DefaultCourseBackground;
            course.MiniatureImage ??= CourseConstants.DefaultCourseBackground;
            course.Tags = await _tagRepository.AsQueryable().Where(x => courseDto.Tags.Contains(x.Id)).ToListAsync();
            await _courseRepository.AddAsync(course);
        }

        public async Task<CoursePageDto> GetByIdAsync(int id, string username)
        {
            var user = await _userRepository.AsQueryable().FirstOrDefaultAsync(x => x.Username == username);
            var course = await _courseRepository
                .AsQueryable()
                .Where(x => x.Id == id)
                .Include(x => x.Creator)
                .Include(x => x.Chapters)
                .Include(x => x.Ratings.Where(y => user != null && y.UserId == user.Id))
                .Include(x => x.Tags)
                .FirstOrDefaultAsync();
            if (course == null)
            {
                throw new CustomNotFoundException("Course not found!");
            }
            var result = _mapper.Map<CoursePageDto>(course);
            if (user != null) result.UserRating = course.Ratings.FirstOrDefault(x => x.UserId == user.Id)?.Value ?? 0;
            return result;
        }
        

        public async Task UpdateRatingsAsync()
        {
            var courses = await _courseRepository
                .AsQueryable()
                .Include(x => x.Ratings)
                .ToListAsync();
            
            foreach (var course in courses)
            {
                var ratings = course.Ratings.Count;
                if (ratings == 0) continue;
                var average = (decimal)course.Ratings.Sum(x => x.Value) / ratings;
                course.AverageRating = (float)Math.Round(average,2);
                course.RatingsCount = ratings;
            }
            await _courseRepository.UpdateRangeAsync(courses);
        }





    }
}
