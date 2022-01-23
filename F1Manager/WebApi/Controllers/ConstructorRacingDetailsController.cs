using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConstructorRacingDetailsController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.ConstructorRacingDetails.IConstructorRacingDetailsUnitOfWork constructorsUnitOfWork;
        private readonly IMapper mapper;

        public ConstructorRacingDetailsController(Infrastructure.UnitOfWorks.ConstructorRacingDetails.IConstructorRacingDetailsUnitOfWork constructorsUnitOfWork,
            IMapper mapper)
        {
            this.constructorsUnitOfWork = constructorsUnitOfWork;
            this.mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{constructorId}")]
        public async Task<IActionResult> GetAllConstructors(int constructorId)
        {
            var constructors = await this.constructorsUnitOfWork.Constructors.GetAll().ConfigureAwait(false);
            var constructor = constructors.FirstOrDefault(x => x.ConstructorId == constructorId);

            if (constructor == null)
                return NotFound("No registered constructors racing detials.");

            return Ok(constructor);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("init/")]
        public async Task<IActionResult> CreateInitState
                                    ([FromBody] DTOs.ConstructorsResult.ConstructorInitDto constructorDetails)
        {
            var newConstructor = mapper.Map<Domain.ConstructorRacingDetails.ConstructorsRacingDetail>(constructorDetails);

            if (await this.constructorsUnitOfWork.Constructors.IsCreated(newConstructor.ConstructorId))
            {
                return Conflict("for constructor exist racing details");
            }

            if (!await this.constructorsUnitOfWork.Constructors
                                        .CreateInitState(newConstructor))
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            return Ok(await constructorsUnitOfWork.Constructors.GetById(newConstructor.Id));
        }

        [MapToApiVersion("1.0")]
        [HttpPost("create/")]
        public async Task<IActionResult> CreateExistingState([FromBody] DTOs.ConstructorsResult.ConstructorRacingResultDto constructorDetails)
        {
            var racingdetails = mapper.Map<Domain.ConstructorRacingDetails.ConstructorsRacingDetail>(constructorDetails);
         

            if (await this.constructorsUnitOfWork.Constructors.IsCreated(racingdetails.ConstructorId))
            {
                return Conflict("for constructor exist racing details");
            }

            if (!await this.constructorsUnitOfWork.Constructors.Insert(racingdetails))
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            return Ok(await constructorsUnitOfWork.Constructors.GetById(racingdetails.Id));
        }

        [MapToApiVersion("1.0")]
        [HttpPut("update/{constructorId}")]
        public async Task<IActionResult> UpdateExistingResult([FromBody] DTOs.ConstructorsResult.ConstructorRacingResultDto constructorDetails, int constructorId)
        {
            var racingdetails = mapper.Map<Domain.ConstructorRacingDetails.ConstructorsRacingDetail>(constructorDetails);
            racingdetails.ConstructorId = constructorId;
            if (!await this.constructorsUnitOfWork.Constructors.Update(racingdetails))
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            return Ok(await this.constructorsUnitOfWork.Constructors.GetById(racingdetails.Id));
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("delete/{constructorId}")]
        public async Task<IActionResult> DeleteResult(int constructorId)
        {
            var constructorRacingDetails = new Domain.ConstructorRacingDetails.ConstructorsRacingDetail() { ConstructorId = constructorId };
            if (!await this.constructorsUnitOfWork.Constructors.Delete(constructorRacingDetails))
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration of constructors racing detials.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("constructorsChampionships/{constructorId}")]
        public async Task<IActionResult> IncrementConstructorsChampionships(int constructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.IncrementConstructorChampionships(constructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("driverChampionships/{constructorId}")]
        public async Task<IActionResult> IncrementDriverChampionships(int constructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.IncrementDriverChampionships(constructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("raceVictories/{constructorId}")]
        public async Task<IActionResult> IncrementRaceVictories(int constructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.IncrementRaceVictories(constructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("podiums/{constructorId}")]
        public async Task<IActionResult> IncrementPodiums(int constructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.IncrementPodiums(constructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("polPositions/{constructorId}")]
        public async Task<IActionResult> IncrementPolPositions(int constructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.IncrementPolPositions(constructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("fastesLaps/{constructorId}")]
        public async Task<IActionResult> IncrementFastesLaps(int constructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.IncrementFastesLaps(constructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost("fastesLaps/{constructorId}/{newConstructorId}")]
        public async Task<IActionResult> ChangeToApproprateConstructor(int constructorId, int newConstructorId)
        {
            if (!await this.constructorsUnitOfWork.Constructors.ChangeToApproprateConstructor(constructorId, newConstructorId))
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return NotFound("Problem on registration IncrementConstructorChampionships.");
            }

            return Ok();
        }
    }
}