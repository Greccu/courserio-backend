using AutoMapper;
using Courserio.Core.Constants;
using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Answer;
using Courserio.Core.DTOs.Auth;
using Courserio.Core.DTOs.Chapter;
using Courserio.Core.DTOs.Course;
using Courserio.Core.DTOs.Question;
using Courserio.Core.DTOs.Rating;
using Courserio.Core.DTOs.Tags;
using Courserio.Core.DTOs.User;
using Courserio.Core.Helpers;
using Courserio.Core.Models;
using Courserio.Keycloak.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Courserio.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<DateTime, DateOnly>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();
            CreateMap<User, UserProfileDto>().ReverseMap();

            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginResponseDto>().ReverseMap();

            CreateMap<UserRegisterDto, RegisterDto>().ReverseMap();

            CreateMap<Course, CourseListDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .ReverseMap();
            CreateMap<Course, CoursePageDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))

                 
                .ReverseMap();
            CreateMap<Course, CourseCreateDto>()
                .ReverseMap()
                .ForMember(dest => dest.Tags, opt => opt.Ignore());
            CreateMap<Course, CourseFeaturedDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .ReverseMap();
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .ReverseMap();
            
            CreateMap<Chapter, ChapterListDto>().ReverseMap();
            CreateMap<Chapter, ChapterPageDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .ReverseMap();
            CreateMap<Chapter, ChapterCreateDto>()
                .ReverseMap();


            CreateMap<Answer, AnswerDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .AfterMap((src, dest) => { if (src.Anonymous) dest.User = UserConstants.AnonymousUser; });
            CreateMap<Answer, AnswerCreateDto>().ReverseMap();

            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .AfterMap((src, dest) => { if (src.Anonymous) dest.User = UserConstants.AnonymousUser; });
            CreateMap<Question, QuestionCreateDto>().ReverseMap();

            CreateMap<Rating, RatingCourseDto>()
                .ForMember(dest => dest.CreatedAtRelative, opt => opt.MapFrom(src => src.CreatedAt.ToRelativeTime()))
                .ReverseMap();
            CreateMap<Rating, RatingCreateDto>().ReverseMap();

            CreateMap<Tag, TagDto>();
            CreateMap<Tag, TagCourseDto>();
            CreateMap<string, Tag>().ConvertUsing(s => new Tag { Name = s });
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            CreateMap<JsonPatchDocument<User>, JsonPatchDocument<UserProfileDto>>().ReverseMap();
            CreateMap<Operation<User>, Operation<UserProfileDto>>().ReverseMap();



        }
    }

}
