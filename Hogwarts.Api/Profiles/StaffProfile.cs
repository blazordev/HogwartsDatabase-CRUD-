using AutoMapper;
using Hogwarts.Data.Models;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Profiles
{
    public class StaffProfile: Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, StaffDto>()
                .ForMember(
                    dest => dest.Roles,
                    opt => opt.MapFrom(src => src.StaffRoles.Select(sr => sr.Role)));
            
            CreateMap<StaffDto, Staff>();            
        }
    }
}
