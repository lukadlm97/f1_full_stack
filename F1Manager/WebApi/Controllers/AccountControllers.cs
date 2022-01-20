using Application.Services;
using AutoMapper;
using Domain.Users;
using Infrastructure.UnitOfWorks.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    [Authorize(Policy = "ShouldBeAAdmin")]
    [Route("api/account")]
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        private readonly IUsersUoW userUoW;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ClaimsPrincipal caller;
        private readonly IJWTService jWTService;

        public AccountControllers(IUsersUoW userUoW, IConfiguration config, IMapper mapper,Application.Services.IUserService userService,Application.Services.IJWTService jWTService, IHttpContextAccessor httpContextAccessor)
        {
            this.userUoW = userUoW;
            this.userService = userService;
            this.mapper = mapper;
            this.caller = httpContextAccessor.HttpContext.User;
            this.jWTService = jWTService;
        }

        // GET: api/account/users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await this.userUoW.Users.GetAll();

            if (users == null)
                return NotFound("No registered users.");

            return Ok(users.Select(x => this.mapper.Map<SingleAcountView>(x)));
        }

        // POST: api/account/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var identity = await userService.GetIndentity(user.UserName, user.Password);
            if (identity == null)
                return NotFound();

            var jwt = jWTService.GenerateToken(identity);

            return new OkObjectResult(new{
                 token = jwt
            });
        }

       

        // POST: api/account/assigncountry/2
        [AllowAnonymous]
        [HttpPost("assigncountry/{id}")]
        public async Task<IActionResult> AssignCountry(int id, [FromBody] AssignCountryDto countryData)
        {
            var isAssignedToUser = await this.userUoW.Users.AssignCountryToUser(id, countryData.CountryId, countryData.CountryName);
            if (!isAssignedToUser)
            {
                return NotFound();
            }

            var countOfChanges = await this.userUoW.Commit();
            if (countOfChanges != 0)
            {
                return Ok(new VerificationView
                {
                    Message = "success"
                });
            }
            return BadRequest();
        }

        // POST: api/account/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var createdUserId = await this.userService.Register(mapper.Map<User>(user), user.Password.ToLower());
          

            if (createdUserId != 0)
            {
                return Ok(await this.userUoW.Users.GetByID(createdUserId));
            }
            return BadRequest();
        }

        // POST: api/account/verify
        [AllowAnonymous]
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerificationDto user)
        {
            await this.userUoW.Users.VerifyAccount(mapper.Map<User>(user), user.Password);
            var countOfChanges = await this.userUoW.Commit();

            if (countOfChanges != 0)
            {
                return Ok(new VerificationView
                {
                    Message = "success"
                });
            }
            return BadRequest();
        }

        // POST: api/account/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] ContentWriterDto user)
        {
            var adminUsername = caller.Claims.SingleOrDefault(x => x.Type ==
                                                                $"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var newUser = mapper.Map<User>(user);
            newUser.CreatedBy = adminUsername.Value.ToString();

            var createdUser = await this.userUoW.Users.Register(newUser, user.Password.ToLower());
            var countOfChanges = await this.userUoW.Commit();

            if (countOfChanges != 0)
            {
                var createdContentWriter = this.mapper.Map<AdminRegistrationView>(createdUser);
                createdContentWriter.Password = user.Password;

                return Ok(createdContentWriter);
            }

            return BadRequest();
        }

        // PUT: api/account/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] ContentWriterUpdateDto user)
        {
            var adminUsername = caller.Claims.SingleOrDefault(x => x.Type ==
                                                                $"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var newUser = mapper.Map<User>(user);
            newUser.UpdatedBy = adminUsername.Value.ToString();

            var createdUser = await this.userUoW.Users.UpdateDetails(user.Id, newUser, user.Password.ToLower());
            var countOfChanges = await this.userUoW.Commit();

            if (countOfChanges != 0)
            {
                var updatedContentWriter = this.mapper.Map<AdminRegistrationView>(createdUser);
                updatedContentWriter.Password = user.Password;

                return Ok(updatedContentWriter);
            }

            return BadRequest();
        }
    }
}