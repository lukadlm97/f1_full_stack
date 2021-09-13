using AutoMapper;
using FormulaManager.DAL.Entities.Origin;
using FormulaManager.DAL.Entities.Users;
using FormulaManager.UsersWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaManager.UsersWebApi.Automappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ForMember(dest=>dest.Origin,opts=>opts.MapFrom(src=>src.Origin));
        }
    }
}
