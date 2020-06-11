﻿using AutoMapper;
using Hogwarts.Api.Models;
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
            CreateMap<Staff, StaffDto>();
            CreateMap<StaffForCreationDto, Staff>();
                
        }
    }
}