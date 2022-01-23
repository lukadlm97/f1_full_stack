using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.Competition.ICompetitionUnitOfWork competiotionUnitOfWork;
        private readonly IMapper mapper;

        public CompetitionController(Infrastructure.UnitOfWorks.Competition.ICompetitionUnitOfWork competiotionUnitOfWork, IMapper mapper)
        {
            this.competiotionUnitOfWork = competiotionUnitOfWork;
            this.mapper = mapper;
        }


        [MapToApiVersion("1.0")]
        [HttpGet()]
        public async Task<IActionResult> GetAllCompetitions()
        {
            var championships = await this.competiotionUnitOfWork.CompetitionRepository.GetAll();

            if (championships == null)
                return NotFound("No registered championships.");

            return Ok(championships);
        }
    }
}
