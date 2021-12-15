﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverControllers : ControllerBase
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
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await this.driversUnitOfWork.Drivers.GetAllRaw();

            if (drivers == null)
                return NotFound("No registered drivers.");

            return Ok(drivers);
        }

        // GET: api/drivers/full
        [HttpGet("full")]
        public async Task<IActionResult> GetFullDrivers()
        {
            var drivers = await this.driversUnitOfWork.Drivers.GetAll();

            if (drivers == null)
                return NotFound("No registered drivers.");

            return Ok(drivers);
        }

        //POST: api/drivers/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateDriver([FromBody] Domain.Drivers.Driver driver, CancellationToken cancellationToken = default)
        {
            if (!await this.driversUnitOfWork.Drivers.Insert(this.mapper.Map<Domain.Drivers.Driver>(driver)))
                return BadRequest("Driver not inserted!!!");

            if(await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver insert not confirmed!!!");
            }

            return Ok();
        }

        //PUT: api/drivers/{id}/update
        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateDriver(int id, [FromBody] Domain.Drivers.Driver driver, CancellationToken cancellationToken = default)
        {
            var entity = this.mapper.Map<Domain.Drivers.Driver>(driver);
            entity.Id = id;

            if (!await this.driversUnitOfWork.Drivers.Update(entity))
                return BadRequest("Driver not updated!!!");

            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver update not confirmed!!!");
            }

            return Ok();
        }

        //PUT: api/drivers/{id}/update
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteDriver(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.driversUnitOfWork.Drivers.Delete(new Domain.Drivers.Driver { Id = id }))
                return BadRequest("Driver not deleted!!!");

            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver deleted not confirmed!!!");
            }

            return Ok();
        }

        [HttpGet("{id}/retirement")]
        public async Task<IActionResult> DriverRetirement(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.driversUnitOfWork.Drivers.DriverRetirement(id))
                return BadRequest("Driver not retired!!!");
            
            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver retaire not confirmed!!!");
            }

            return Ok();
        }

        [HttpGet("{id}/comeback")]
        public async Task<IActionResult> DriverComeback(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.driversUnitOfWork.Drivers.DriverReactivation(id))
                return BadRequest("Driver not retired!!!");
            
            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver retire not confirmed!!!");
            }

            return Ok();
        }

        [HttpGet("{id}/citizenship/{countryId}")]
        public async Task<IActionResult> ChangeCitizenship(int id, int countryId, CancellationToken cancellationToken = default)
        {
            if (!await this.driversUnitOfWork.Drivers.ChangeCitizenship(id, countryId, cancellationToken))
                return BadRequest("Driver not change citizenship!!!");
            
            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver change citizenship not confirmed!!!");
            }

            return Ok();
        }
    }
}