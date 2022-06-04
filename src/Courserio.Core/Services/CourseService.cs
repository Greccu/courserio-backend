 using AutoMapper;
using Courserio.Core.Constants;
using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
 using Courserio.Core.MachineLearningModel;
 using Courserio.Core.MachineLearningModel.Entities;
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
        private readonly IModelService _modelService;

        public CourseService(IGenericRepository<Course> courseRepository, 
            IMapper mapper, 
            IGenericRepository<User> userRepository, 
            IGenericRepository<Tag> tagRepository, 
            IModelService modelService)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _modelService = modelService;
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
            var user = await _userRepository
                .AsQueryable()
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Username == username);
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
            result.Tags.ToList().ForEach(x => x.IsFollowed = user != null && user.Tags.Any(t => t.Id == x.Id));
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

        public async Task<List<CourseListDto>> GetRecommendedAsync(string username)
        {
            var user = await _userRepository
                .AsQueryable()
                .Where(x => x.Username == username)
                .Include(x => x.Ratings)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new CustomNotFoundException("User not found!");
            }
            var userRatings = user.Ratings.Select(x => x.CourseId).ToList();
            var userTags = user.Tags.Select(x => x.Id).ToList();
            if (userRatings.Count == 0 && userTags.Count == 0)
            {
                throw new CustomBadRequestException("You need to rate at least one course or follow at least one tag to see recommended courses!");
            }
            // base query for recommended courses
            var baseQuery = _courseRepository.AsQueryable()
                .Include(x => x.Creator)
                .Include(course => course.Ratings)
                .Include(course => course.Tags)
                .Where(course => !userRatings.Contains(course.Id)) // exclude courses that user already rated
                .Where(course => course.CreatorId != user.Id); // exclude courses that user created

            // get top rated courses
            var topRatedCourses = await baseQuery
                .OrderByDescending(course => course.AverageRating)
                .Take(5)
                .ToListAsync();

            // get most popular courses
            var popularCourses = await baseQuery
                .OrderByDescending(course => course.RatingsCount)
                .Take(5)
                .ToListAsync();

            // get courses with similar tags
            var similarCourses = await baseQuery
                .OrderByDescending(course => course.Tags.Count(tag => userTags.Contains(tag.Id)))
                .Take(10)
                .ToListAsync();

            var courses = topRatedCourses
                .Concat(popularCourses)
                .Concat(similarCourses)
                .DistinctBy(course => course.Id)
                .OrderByDescending(course => _modelService.Predict(new ModelInput
                {
                    CourseId = course.Id,
                    UserId = user.Id
                }).Score)
                .Take(4)
                .ToList();

            return courses.Select(_mapper.Map<CourseListDto>).ToList();
        }

        



    }
}
