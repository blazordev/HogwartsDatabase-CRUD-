using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Profiles
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleForCreationDto,Role>();
            CreateMap<Role,RoleDto>();
            CreateMap<RoleForEditDto,Role>().ReverseMap();            

        }
    }
}
