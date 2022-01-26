using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PowerUnitSupplierController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.PowerUnitSupplier.IPowerUnitSupplierUnitOfWork powerUnitSupplierUoW;

        public PowerUnitSupplierController(Infrastructure.UnitOfWorks.PowerUnitSupplier.IPowerUnitSupplierUnitOfWork powerUnitSupplierUoW)
        {
            this.powerUnitSupplierUoW = powerUnitSupplierUoW;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var powerUnitSuppliers = await this.powerUnitSupplierUoW.PowerUnitSupplier.GetAll();

            if (powerUnitSuppliers == null)
                return NotFound("No registered drivers.");

            return Ok(powerUnitSuppliers);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] Domain.PoweUnitSupplier.PowerUnitSupplier powerUnitSupplier)
        {
            if (!await this.powerUnitSupplierUoW.PowerUnitSupplier.Insert(powerUnitSupplier))
            {
                return NotFound("No registered drivers.");
            }

            if (await this.powerUnitSupplierUoW.Commit() == 0)
            {
                return BadRequest("Driver insert not confirmed!!!");
            }

            return Ok(await this.powerUnitSupplierUoW.PowerUnitSupplier.GetById(powerUnitSupplier.Id));
        }


        [MapToApiVersion("1.0")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] Domain.PoweUnitSupplier.PowerUnitSupplier powerUnitSupplier)
        {
            if (!await this.powerUnitSupplierUoW.PowerUnitSupplier.Update(powerUnitSupplier))
            {
                return NotFound("No registered drivers.");
            }

            if (await this.powerUnitSupplierUoW.Commit() == 0)
            {
                return BadRequest("Driver insert not confirmed!!!");
            }

            return Ok(await this.powerUnitSupplierUoW.PowerUnitSupplier.GetById(powerUnitSupplier.Id));
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{powerUnitId}")]
        public async Task<IActionResult> Delete(int powerUnitId)
        {
            if (!await this.powerUnitSupplierUoW.PowerUnitSupplier.Delete(new Domain.PoweUnitSupplier.PowerUnitSupplier()
            {
                Id=powerUnitId
            }))
            {
                return NotFound("No registered drivers.");
            }

            if (await this.powerUnitSupplierUoW.Commit() == 0)
            {
                return BadRequest("Driver insert not confirmed!!!");
            }

            return Ok(await this.powerUnitSupplierUoW.PowerUnitSupplier.GetById(powerUnitId));
        }




    }
}
