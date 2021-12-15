using AutoMapper;

namespace WebApi.Mappers
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DTOs.Drivers.DriverDto, Domain.Drivers.Driver>();
        }
    }
}