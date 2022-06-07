using Domain.Season;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SeasonController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.Season.ISeasonUnitOfWork seasonUnitOfWork;

        public SeasonController(Infrastructure.UnitOfWorks.Season.ISeasonUnitOfWork seasonUnitOfWork)
        {
            this.seasonUnitOfWork = seasonUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeasons()
        {
            var seasons = await this.seasonUnitOfWork.SeasonRepository.GetAll();

            if (seasons == null || !seasons.Any())
                return NotFound("No registered seasons.");

            return Ok(seasons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeasonById(int id)
        {
            var season = await this.seasonUnitOfWork.SeasonRepository.GetById(id);

            if (season == null)
                return NotFound("No registered seasons.");

            return Ok(season);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Domain.Season.Season season)
        {
            if (!await this.seasonUnitOfWork.SeasonRepository.Insert(season))
                return BadRequest("Season not inserted!!!");

            if (await this.seasonUnitOfWork.Commit() == 0)
            {
                return BadRequest("Season insert not confirmed!!!");
            }

            return Ok(await this.seasonUnitOfWork.SeasonRepository.GetById(season.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.seasonUnitOfWork.SeasonRepository.Delete(new Season() { Id = id }))
                return BadRequest("Season not deleted!!!");

            if (await this.seasonUnitOfWork.Commit() == 0)
            {
                return BadRequest("Season delete not confirmed!!!");
            }

            return Ok();
        }
    }
}