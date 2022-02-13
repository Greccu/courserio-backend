using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Auth;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Models;
using Courserio.Core.DTOs.User;
using Courserio.Keycloak.Models;

namespace Courserio.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {                                                                                                                                                                                                                                                                       
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserProfileDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginResponseDto>().ReverseMap();

            CreateMap<UserRegisterDto, RegisterDto>().ReverseMap();

            CreateMap<Course, CourseListDto>().ReverseMap();
            CreateMap<Course, CoursePageDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            CreateMap<JsonPatchDocument<User>, JsonPatchDocument<UserProfileDto>>().ReverseMap();
            CreateMap<Operation<User>, Operation<UserProfileDto>>().ReverseMap();



        }
    }
    
}
