using AutoMapper;

namespace WebApi.Mappers
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DTOs.Drivers.DriverDto, Domain.Drivers.Driver>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.IsRetired))
                .ForMember(dest => dest.IsRetired, opt => opt.MapFrom(src => src.IsRetired));
        }
    }
}