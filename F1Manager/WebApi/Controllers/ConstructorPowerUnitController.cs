using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConstructorPowerUnitController : ControllerBase
    {

        private readonly Infrastructure.UnitOfWorks.ConstructorsPowerUnit.IConstructorsPowerUnit constructorsPowerUnit;
        private readonly IMapper mapper;

        public ConstructorPowerUnitController(Infrastructure.UnitOfWorks.ConstructorsPowerUnit.IConstructorsPowerUnit constructorsPowerUnit,IMapper mapper)
        {
            this.constructorsPowerUnit = constructorsPowerUnit;
            this.mapper = mapper;
        }

        [HttpGet("{constructorId}")]
        public async Task<IActionResult> GetCurrentPowerUnitSupplier(int constructorId)
        {
            if (!await this.constructorsPowerUnit.ConstructorsPowerUnit.IsInContract(constructorId))
                return NotFound("No registered power unit supplier for constructor.");

            return Ok(await this.constructorsPowerUnit.ConstructorsPowerUnit.GetCurrentConstructorPowerUnit(constructorId));
        }

        [HttpGet("{constructorId}/history")]
        public async Task<IActionResult> GetPowerUnitSupplierHistory(int constructorId)
        {
            var powerUnitSuppliers = await this.constructorsPowerUnit.ConstructorsPowerUnit.GetConstructorsHistoryPowerUnit(constructorId);

            if (powerUnitSuppliers == null)
                return NotFound("No registered power unit supplier for constructor.");

            return Ok(powerUnitSuppliers);
        }


        [HttpPost("startContract")]
        public async Task<IActionResult> StartContract([FromBody] DTOs.ConstructorPowerUnit.ConstructorPowerUnit contract)
        {
            if (await this.constructorsPowerUnit.ConstructorsPowerUnit
                            .IsInContract(contract.ConstructorId))
            {
                return Conflict("Constructor is in contract");
            }

            var mappedObj = mapper.Map<Domain.ConstructorsPowerUnits.ConstructorPowerUnit>(contract);
            if (!await this.constructorsPowerUnit.ConstructorsPowerUnit.CreateNewContract(mappedObj))
            {
                return BadRequest("contract not inserted!!!");
            }

            if (await this.constructorsPowerUnit.Commit() == 0)
            {
                return BadRequest("contract insert not confirmed!!!");
            }


            return Ok(await this.constructorsPowerUnit.ConstructorsPowerUnit.GetById(mappedObj.Id));
        }


        [HttpPost("endContract/{contractId}")]
        public async Task<IActionResult> EndContract(int contractId)
        {
            if (!await this.constructorsPowerUnit.ConstructorsPowerUnit.EndContract(contractId))
                return BadRequest("contract not ended!!!");

            if (await this.constructorsPowerUnit.Commit() == 0)
            {
                return BadRequest("contract end not confirmed!!!");
            }


            return Ok(await this.constructorsPowerUnit.ConstructorsPowerUnit.GetById(contractId));
        }


    }
}