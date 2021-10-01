using AutoMapper;
using Domain.Users;
using Infrastructure.UnitOfWorks.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        private readonly IUsersUoW userUoW;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public AccountControllers(IUsersUoW userUoW, IConfiguration config,IMapper mapper)
        {
            this.userUoW = userUoW;
            this.config = config;
            this.mapper = mapper;
        }

        // GET: api/account/users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await this.userUoW.Users.GetAll();

            if (users == null)
                return NotFound("No register users.");

            return Ok(users);
        }

        // GET: api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto user)
        {
            var userDb = await this.userUoW.Users.Login(user.UserName.ToLower(), user.Password.ToLower());
            if (userDb == null)
                return Unauthorized();

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
               new Claim(ClaimTypes.Name, userDb.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(this.config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                employee = userDb
            });
        }

        // GET: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var createdUser = await this.userUoW.Users.Register(mapper.Map<User>(user),user.Password.ToLower());
            var countOfChanges = await this.userUoW.Commit();

            if (countOfChanges != 0)
            {
                return Ok(createdUser);
            }
            return BadRequest();
        }
    }
}