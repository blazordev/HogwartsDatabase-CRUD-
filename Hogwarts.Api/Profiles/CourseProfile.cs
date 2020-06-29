using AutoMapper;
using Hogwarts.Data.Models;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Profiles
{
    public class CourseProfile: Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<CourseForEditDto, Course>().ReverseMap();
            
        }
    }
}
