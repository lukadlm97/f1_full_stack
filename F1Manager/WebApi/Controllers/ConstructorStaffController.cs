using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConstructorStaffController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.ConstructorsStaffContracts.IConstructorsStaffContractsUnitOfWork constructorsStaffContractsUnitOfWork;

        public ConstructorStaffController(Infrastructure.UnitOfWorks.ConstructorsStaffContracts.IConstructorsStaffContractsUnitOfWork constructorsStaffContractsUnitOfWork)
        {
            this.constructorsStaffContractsUnitOfWork = constructorsStaffContractsUnitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetActiveStaff()
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetAll();

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetStaffHistory()
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetAllHistory();

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }


        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetStaff(int roleId)
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetAllForRoles(roleId);

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpGet("constructor/{constructorId}")]
        public async Task<IActionResult> GetConstructorStaff(int constructorId)
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetCurrentStuff(constructorId);

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpGet("constructor/{constructorId}/history")]
        public async Task<IActionResult> GetConstructorHistoryStaff(int constructorId)
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetHistoryStuff(constructorId);

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetCurrentStaffPostion(int staffId)
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetCurrentPosition(staffId);

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpGet("staff/{staffId}/history")]
        public async Task<IActionResult> GetStaffhistoryPositions(int staffId)
        {
            var technicans = await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetHistoryPosition(staffId);

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartContract([FromBody]Domain.ConstructorsStaffContracts.ConstructorsStaffContracts contract)
        {
            if (!await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.InsertContract(contract))
                return BadRequest("contract not inserted!!!");

            if (await this.constructorsStaffContractsUnitOfWork.Commit() == 0)
            {
                return BadRequest("contract insert not confirmed!!!");
            }


            return Ok(await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetById(contract.Id));
        }


        [HttpPost("end")]
        public async Task<IActionResult> EndContract([FromBody] Domain.ConstructorsStaffContracts.ConstructorsStaffContracts contract)
        {
            if (!await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.EndContract(contract))
                return BadRequest("contract not ended!!!");

            if (await this.constructorsStaffContractsUnitOfWork.Commit() == 0)
            {
                return BadRequest("contract end not confirmed!!!");
            }


            return Ok(await this.constructorsStaffContractsUnitOfWork.ConstructorsStaffContracts.GetById(contract.Id));
        }

    }
}
