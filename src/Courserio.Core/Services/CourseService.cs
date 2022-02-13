using Courserio.Core.Constants;
using Courserio.Core.Models;
using Courserio.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    

    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(IGenericRepository<Course> courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<List<CourseListDto>> GetHomeAsync()
        {
            var courseFilter = new CourseFilter
            {
                IsPagingEnabled = true,
                Page = 1,
                PageSize = 5
            };
            var courses = _courseRepository
                    .ListAllAsQueryable()
                    .Include(x => x.Creator)
                    .ApplyPagination(courseFilter)
                ;

            var result = await courses.ToListAsync();
            return result.Select(_mapper.Map<CourseListDto>).ToList();
        }

        public async Task<List<CourseListDto>> ListAsync(CourseFilter courseFilter)
        {

            var courses = _courseRepository
                    .ListAllAsQueryable()
                    .Include(x => x.Creator)
                    .ApplyPagination(courseFilter)
                ;

            var result = await courses.Select(course => _mapper.Map<CourseListDto>(course)).ToListAsync();
            return result;
        }

        public async Task CreateAsync(CourseCreateDto courseDto)
        {

            var course = _mapper.Map<Course>(courseDto);
            course.CoverImage ??= CourseConstants.DefaultCourseBackground;
            course.MiniatureImage ??= CourseConstants.DefaultCourseBackground;
            await _courseRepository.AddAsync(course);
        }


        //    public List<CourseInfo> GetCourses(string tags, string sort, string search, int page)
        //    {
        //        var sepTags = tags!=null?tags.Split(',').ToList():null;;

        //        var l = _context.Courses
        //            .Include(c => c.Tags)
        //            .Where(c => (sepTags == null || c.Tags.Where(t => sepTags.Contains(t.Name)).Any())
        //            && (search == null || c.Title.Contains(search) || c.Description.Contains(search)));

        //        switch (sort)
        //        {
        //            case "titleA":
        //                l = l.OrderBy(c => c.Title);
        //                break;
        //            case "titleD":
        //                l = l.OrderByDescending(c => c.Title);
        //                break;
        //            case "dateA":
        //                l = l.OrderBy(c => c.CreatedAt);
        //                break;
        //            case "dateB":
        //                l = l.OrderByDescending(c => c.CreatedAt);
        //                break;
        //            default:
        //                break;
        //        }
        //        int nr = RandomConstants.CoursesPerPage;
        //        return l.Skip(page>0?(page - 1)*nr:0)
        //            .Take(nr)
        //            .Select(c => new CourseInfo(c))
        //            .ToList();

        //    }
        //    public CompleteCourseInfo GetCompleteCourse(int courseId)
        //    {
        //        if (_context.Courses.Find(courseId) == null)
        //        {
        //            return null;
        //        }
        //        return _context.Courses
        //            .Include(c => c.Creator)
        //            .Include(c => c.Users)
        //            .Include(c => c.Tags)
        //            .Include(c => c.Sections)
        //            .ThenInclude(s=>s.Chapters)
        //            .Where(c => c.Id == courseId)
        //            .Select(course => new CompleteCourseInfo(course))
        //            .First();
        //    }

        //    public CourseInfo CreateCourse(CoursePost course, string creatorId)
        //    {
        //        Course newCourse = new Course {
        //            Title = course.Title,
        //            Description = course.Description,
        //            CreatedAt = DateTime.Now,
        //            CreatorId = creatorId
        //        };
        //        _context.Courses.Add(newCourse);
        //        _context.SaveChanges();
        //        return new CourseInfo(newCourse);
        //    }

        //    public CourseInfo AssignUserToCourse(int courseId, string userId)
        //    {
        //        Course course = _context.Courses
        //            .Include(c => c.Users)
        //            .Include(c => c.Tags)
        //            .Where(course => course.Id == courseId)
        //            .FirstOrDefault();
        //        User user = _context.Users.Find(userId);
        //        if (course == null || course.Users.Contains(user) || userId == null)
        //        {
        //            return null;
        //        }
        //        course.Users.Add(user);
        //        _context.SaveChanges();
        //        return new CourseInfo(course);
        //    }

        //    public CourseInfo RemoveUserFromCourse(int courseId, string userId)
        //    {
        //        Course course = _context.Courses
        //            .Include(c => c.Users)
        //            .Include(c => c.Tags)
        //            .Where(course => course.Id == courseId)
        //            .FirstOrDefault();
        //        User user = _context.Users.Find(userId);
        //        if(course == null || !course.Users.Contains(user))
        //        {
        //            return null;
        //        }
        //        course.Users.Remove(user);
        //        _context.SaveChanges();
        //        return new CourseInfo(course);
        //    }

        //    public CourseInfo UpdateCourse(int courseId, CoursePost course, string creatorId)
        //    {
        //        Course courseToUpdate = _context.Courses.Find(courseId);
        //        if(courseToUpdate.CreatorId != creatorId)
        //        {
        //            return null;
        //        }
        //        if(course.Title != "")
        //        {
        //            courseToUpdate.Title = course.Title;
        //        }
        //        if(course.Description != "")
        //        {
        //            courseToUpdate.Description = course.Description;
        //        }
        //        _context.SaveChanges();
        //        return new CourseInfo(courseToUpdate);

        //    }

        //    public CourseInfo DeleteCourse(int courseId, string userId)
        //    {
        //        Course courseToDelete = _context.Courses.Find(courseId);
        //        if (courseToDelete == null || (userId != "Moderator" && courseToDelete.CreatorId != userId))
        //        {
        //            return null;
        //        }
        //        _context.Courses.Remove(courseToDelete);
        //        _context.SaveChanges();
        //        return new CourseInfo(courseToDelete);
        //    }



    }
}
