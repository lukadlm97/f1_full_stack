using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConstructorController : ControllerBase
    {
        private readonly Infrastructure.UnitOfWorks.Constructors.ICounstructorUnitOfWork constructorsUnitOfWork;
        private readonly IMapper mapper;

        public ConstructorController(Infrastructure.UnitOfWorks.Constructors.ICounstructorUnitOfWork constructorsUnitOfWork,
            IMapper mapper)
        {
            this.constructorsUnitOfWork = constructorsUnitOfWork;
            this.mapper = mapper;
        }

        // GET: api/drivers/

        [MapToApiVersion("1.0")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllConstructors()
        {
            var constructors = await this.constructorsUnitOfWork.Constructors.GetAll();

            if (constructors == null)
                return NotFound("No registered constructors.");

            return Ok(constructors);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleConstructor(int id)
        {
            var constructor = await this.constructorsUnitOfWork.Constructors.GetById(id);

            if (constructor == null)
                return NotFound("No registered constructor.");

            return Ok(constructor);
        }

        //POST: api/constructors/create

        [MapToApiVersion("1.0")]
        [HttpPost("create")] 
        public async Task<IActionResult> CreateConstructor([FromBody] DTOs.Constructors.ConstructorDto constructor, CancellationToken cancellationToken = default)
        {
            try
            {
                var constructorMapped = this.mapper.Map<Domain.Constructors.Constructor>(constructor);
                if (!await this.constructorsUnitOfWork.Constructors.Insert(constructorMapped))
                    return BadRequest("Constructor not inserted!!!");

                if (await this.constructorsUnitOfWork.Commit() == 0)
                {
                    return BadRequest("Constructor insert not confirmed!!!");
                }

            return Ok(constructorMapped);
            }catch(Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        //PUT: api/constructors/{id}/update

        [MapToApiVersion("1.0")]
        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateConstructor(int id, [FromBody] DTOs.Constructors.ConstructorDto constructorDto, CancellationToken cancellationToken = default)
        {
            var entity = this.mapper.Map<Domain.Constructors.Constructor>(constructorDto);
            entity.Id = id;

            if (!await this.constructorsUnitOfWork.Constructors.Update(entity))
                return BadRequest("Constructor not updated!!!");

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return BadRequest("Constructor update not confirmed!!!");
            }

            return Ok(entity);
        }

        //PUT: api/drivers/{id}/update

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteConstructor(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.constructorsUnitOfWork.Constructors.Delete(new Domain.Constructors.Constructor { Id = id }))
                return BadRequest("Constructor not deleted!!!");

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return BadRequest("Constructor deleted not confirmed!!!");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}/retirement")]
        public async Task<IActionResult> ConstructorRetirement(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.constructorsUnitOfWork.Constructors.RetireConstructor(id))
                return BadRequest("Constructor not retired!!!");

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return BadRequest("Constructor retaire not confirmed!!!");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}/comeback")]
        public async Task<IActionResult> DriverComeback(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.constructorsUnitOfWork.Constructors.MakeComeback(id))
                return BadRequest("Constructor not retired!!!");

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return BadRequest("Constructor retire not confirmed!!!");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}/citizenship/{countryId}")]
        public async Task<IActionResult> ChangeCitizenship(int id, int countryId, CancellationToken cancellationToken = default)
        {
            if (!await this.constructorsUnitOfWork.Constructors.ChangeTeamNationality(id, countryId))
                return BadRequest("Driver not change citizenship!!!");

            if (await this.constructorsUnitOfWork.Commit() == 0)
            {
                return BadRequest("Driver change citizenship not confirmed!!!");
            }

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("nationality/{countryId}")]
        public async Task<IActionResult> GetByNationality(int countryId, CancellationToken cancellationToken = default)
        {
            var constructors = await this.constructorsUnitOfWork.Constructors.GetConstructors(countryId);

                
            if (constructors == null)
            {
                return BadRequest("Bot registred constructors!!!");
            }

            return Ok(constructors);
        }
    }
}