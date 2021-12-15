using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/countrys")]
    [ApiController]
    public class CountryControllers : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.Countries.ICountriesUnitOfWork countryUnitOfWork;
        private readonly IMapper mapper;

        public CountryControllers(Infrastructure.UnitOfWorks.Countries.ICountriesUnitOfWork countryUnitOfWork, IMapper mapper)
        {
            this.countryUnitOfWork = countryUnitOfWork;
            this.mapper = mapper;
        }

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
