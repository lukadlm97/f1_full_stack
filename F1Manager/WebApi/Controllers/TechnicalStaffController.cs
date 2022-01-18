using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TechnicalStaffController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.TechnicalStuff.ITechnicalStaffUnitOfWork technicalStuffUnitOfWork;

        public TechnicalStaffController(Infrastructure.UnitOfWorks.TechnicalStuff.ITechnicalStaffUnitOfWork technicalStuffUnitOfWork)
        {
            this.technicalStuffUnitOfWork = technicalStuffUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTechicalStaffs()
        {
            var technicans = await this.technicalStuffUnitOfWork.TechnicalStaffRepository.GetAll();

            if (technicans == null)
                return NotFound("No registered techical staff.");

            return Ok(technicans);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTechicalStaffs([FromBody]Domain.TechnicalStaff.TechnicalStaff technicalStaff)
        {
            if (!await this.technicalStuffUnitOfWork.TechnicalStaffRepository.Insert(technicalStaff))
                return BadRequest("Staff not inserted!!!");

            if (await this.technicalStuffUnitOfWork.Commit() == 0)
            {
                return BadRequest("Staff insert not confirmed!!!");
            }

            return Ok(await this.technicalStuffUnitOfWork.TechnicalStaffRepository.GetById(technicalStaff.Id));
        }

        [HttpDelete("{technicalStaffId}")]
        public async Task<IActionResult> DeleteTechicalStaffs(int technicalStaffId)
        {
            if (!await this.technicalStuffUnitOfWork.TechnicalStaffRepository.Delete(technicalStaffId))
                return BadRequest("Staff not deleted!!!");

            if (await this.technicalStuffUnitOfWork.Commit() == 0)
            {
                return BadRequest("Staff delete not confirmed!!!");
            }

            return Ok();
        }


    }
}
