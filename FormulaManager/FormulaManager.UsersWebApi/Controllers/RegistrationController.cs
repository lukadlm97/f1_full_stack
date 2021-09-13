using AutoMapper;
using FormulaManager.DAL.Entities.Services;
using FormulaManager.DAL.Entities.Users;
using FormulaManager.UsersWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormulaManager.UsersWebApi.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegistrationController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ActionResult<string>> TestCall()
        {
            return Ok("Hello from test!!!");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            var createdUser =  await _userService.RegisterUser(_mapper.Map<User>(userDto));

            if (createdUser!=null)
            {
                return Ok(createdUser);
            }

            return BadRequest();
        }



    }
}