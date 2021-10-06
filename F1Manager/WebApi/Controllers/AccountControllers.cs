using AutoMapper;
using Domain.Users;
using Infrastructure.UnitOfWorks.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.AuthorizationAssets;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    [Authorize(Policy = "CanViewUsers")]
    [Route("api/account")]
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        private readonly IUsersUoW userUoW;
        private readonly IMapper mapper;
        private readonly ClaimsPrincipal caller;
        private readonly JwtIssuerOptions jwtOptions;
        private readonly IJwtFactory jwtFactory;

        public AccountControllers(IUsersUoW userUoW, IConfiguration config, IMapper mapper, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            this.userUoW = userUoW;
            this.jwtFactory = jwtFactory;
            this.mapper = mapper;
            this.caller = httpContextAccessor.HttpContext.User;
            this.jwtOptions = jwtOptions.Value;
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

        // GET: api/account/home
        // TODO remove this becouse this is init state for all roles
        [AllowAnonymous]
        [HttpGet("home")]
        public async Task<IActionResult> Home()
        {
            var userId = caller.Claims.SingleOrDefault(x => x.Type == "id");
            var user = await this.userUoW.Users.GetByID(Convert.ToInt32(userId.Value));

            return new OkObjectResult(mapper.Map<LoginView>(user));
        }

        // GET: api/account/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var identity = await GetClaimsIdentity(user.UserName, user.Password);
            if (identity == null)
                return Unauthorized();

            var jwt = await Tokens.GenerateJwt(identity, jwtFactory, user.UserName, jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await this.userUoW.Users.GetObjectByUsername(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            var user = await this.userUoW.Users.Login(userToVerify.UserName.ToLower(), password.ToLower());
            if (user != null)
            {
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString(), user.Role.RoleName));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        // POST: api/account/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var createdUser = await this.userUoW.Users.Register(mapper.Map<User>(user), user.Password.ToLower());
            var countOfChanges = await this.userUoW.Commit();

            if (countOfChanges != 0)
            {
                return Ok(this.mapper.Map<RegistrationView>(createdUser));
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