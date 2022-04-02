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

            CreateMap<DTOs.DriversContracts.DriversContractDto, Domain.Contracts.Contract>()
                .ForMember(dest => dest.ConstructorId, opt => opt.MapFrom(src => src.ConstructorId))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.DriverRolesId, opt => opt.MapFrom(src => src.DriverRolesId));
        }
    }
}