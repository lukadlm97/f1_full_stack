using AutoMapper;
using System;

namespace WebApi.Mappers
{
    public class ConstructorProfile : Profile
    {
        public ConstructorProfile()
        {
            CreateMap<DTOs.Constructors.ConstructorDto, Domain.Constructors.Constructor>();
            CreateMap<DTOs.ConstructorPowerUnit.ConstructorPowerUnit, Domain.ConstructorsPowerUnits.ConstructorPowerUnit>()
                ;
        }
    }
}