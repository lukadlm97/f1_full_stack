using AutoMapper;
using Infrastructure.UnitOfWorks.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class DriverControllers:ControllerBase
    {
        private readonly IUsersUoW userUoW;
        private readonly IMapper mapper;

        public DriverControllers()
        {

        }
    }
}
