using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TechnicalStaffRoleController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.TechnicalStuffRole.ITechnicalStaffRoleUnitOfWork roleUnitOfWork;

        public TechnicalStaffRoleController(Infrastructure.UnitOfWorks.TechnicalStuffRole.ITechnicalStaffRoleUnitOfWork roleUnitOfWork)
        {
            this.roleUnitOfWork = roleUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTechicalStuffRoles()
        {
            var roles = await this.roleUnitOfWork.TechnicalStaffRole.GetAll();

            if (roles == null)
                return NotFound("No registered techical stuff roles.");

            return Ok(roles);
        }
    }
}