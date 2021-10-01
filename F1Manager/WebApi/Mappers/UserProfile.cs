using AutoMapper;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.Mappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ForMember(dest=>dest.RoleId,opt=>opt.MapFrom(src=>src.RoleId==0?4:src.RoleId));
        }
    }
}
