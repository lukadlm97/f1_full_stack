using AutoMapper;

namespace WebApi.Mappers
{
    public class ConstructorResultProfile : Profile
    {
        public ConstructorResultProfile()
        {
            CreateMap<DTOs.ConstructorsResult.ConstructorRacingResultDto, Domain.ConstructorRacingDetails.ConstructorsRacingDetail>();
        }
    }
}