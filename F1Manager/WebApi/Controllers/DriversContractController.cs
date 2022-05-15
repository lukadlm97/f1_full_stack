using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DriversContractController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.DriversContract.IDriversContractUnitOfWork driversUnitOfWork;
        private readonly IMapper mapper;

        public DriversContractController(
                Infrastructure.UnitOfWorks.DriversContract.IDriversContractUnitOfWork driversUnitOfWork, IMapper mapper)
        {
            this.driversUnitOfWork = driversUnitOfWork;
            this.mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{constructorId}")]
        public async Task<IActionResult> GetCurrentDrivers(int constructorId)
        {
            var drivers = await this.driversUnitOfWork.DriversContract.GetCurrentContracts(constructorId);

            if (drivers == null)
                return NotFound("No registered drivers.");

            return Ok(drivers.OrderBy(x => x.DriverRolesId));
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{constructorId}/history")]
        public async Task<IActionResult> GetConstructorsHistory(int constructorId)
        {
            var drivers = await this.driversUnitOfWork.DriversContract.GetContractsHistory(constructorId);

            if (drivers == null)
                return NotFound("No registered drivers.");

            return Ok(drivers.OrderBy(x => x.DriverRolesId).OrderBy(x => x.EndOfContactDate));
        }

        [MapToApiVersion("1.0")]
        [HttpGet("driver/{driverId}")]
        public async Task<IActionResult> GetCurrentConstructor(int driverId)
        {
            var contract = await this.driversUnitOfWork.DriversContract.GetCurrentDriverContract(driverId);

            if (contract == null)
                return NotFound("No registered drivers.");

            return Ok(contract);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("driver/{driverId}/history")]
        public async Task<IActionResult> GetDriversConstructorsHistory(int driverId)
        {
            var contracts = await this.driversUnitOfWork.DriversContract.GetDriversContractHistory(driverId);

            if (contracts == null)
                return NotFound("No registered drivers.");

            return Ok(contracts);
        }

        [HttpPost("startContract")]
        public async Task<IActionResult> StartContract([FromBody] DTOs.DriversContracts.DriversContractDto contract)
        {
            var newContract = mapper.Map<Domain.Contracts.Contract>(contract);

            if (!await this.driversUnitOfWork.DriversContract.StartContract(newContract))
            {
                return BadRequest("contract not inserted!!!");
            }

            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("contract insert not confirmed!!!");
            }

            return Ok(await this.driversUnitOfWork.DriversContract.GetById(newContract.Id));
        }

        [HttpPost("endContract/{contractId}")]
        public async Task<IActionResult> EndContract(int contractId)
        {
            if (!await this.driversUnitOfWork.DriversContract.EndContract(contractId))
                return BadRequest("contract not ended!!!");

            if (await this.driversUnitOfWork.Commit() == 0)
            {
                return BadRequest("contract end not confirmed!!!");
            }

            return Ok(await this.driversUnitOfWork.DriversContract.GetById(contractId));
        }
    }
}