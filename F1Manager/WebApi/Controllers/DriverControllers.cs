using AutoMapper;
using Domain.Drivers;
using Infrastructure.UnitOfWorks.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverControllers:ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.Drivers.IDriversUnitOfWork driversUnitOfWork;
        private readonly IMapper mapper;

        public DriverControllers(Infrastructure.UnitOfWorks.Drivers.IDriversUnitOfWork driversUnitOfWork, IMapper mapper)
        {
            this.driversUnitOfWork = driversUnitOfWork;
            this.mapper = mapper;
        }
        // GET: api/drivers/
        [HttpGet("")]
        public async Task<IActionResult> GetAllDriverss()
        {
            var drivers = await this.driversUnitOfWork.Drivers.GetAll();

            if (drivers == null)
                return NotFound("No registered drivers.");

            return Ok(drivers);
        }


    }
}
