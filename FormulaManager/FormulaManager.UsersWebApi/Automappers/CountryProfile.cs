using AutoMapper;
using FormulaManager.DAL.Entities.Origin;
using FormulaManager.UsersWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaManager.UsersWebApi.Automappers
{
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDto, Country>().ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.CountryId));
        }
    }
}
