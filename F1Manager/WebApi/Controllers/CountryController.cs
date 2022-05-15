using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.Countries.ICountriesUnitOfWork countryUnitOfWork;
        private readonly IMapper mapper;

        public CountryController(Infrastructure.UnitOfWorks.Countries.ICountriesUnitOfWork countryUnitOfWork, IMapper mapper)
        {
            this.countryUnitOfWork = countryUnitOfWork;
            this.mapper = mapper;
        }


        [MapToApiVersion("1.0")]
        [HttpGet()]
        public async Task<IActionResult> GetAllDrivers()
        {
            var countries = await this.countryUnitOfWork.Countries.GetAll();

            if (countries == null)
                return NotFound("No registered countires.");

            return Ok(countries);
        }

    }
}
