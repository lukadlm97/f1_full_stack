using AutoMapper;
using Infrastructure.UnitOfWorks.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(Policy = "CanViewHome")]
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUsersUoW userUoW;
        private readonly IMapper mapper;
        private readonly ClaimsPrincipal caller;

        public HomeController(IUsersUoW userUoW, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userUoW = userUoW;
            this.mapper = mapper;
            this.caller = httpContextAccessor.HttpContext.User;
        }

        // GET: api/account/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var userId = caller.Claims.SingleOrDefault(x => x.Type == "id");
            var user = await this.userUoW.Users.GetByID(Convert.ToInt32(userId.Value));

            return new OkObjectResult(mapper.Map<LoginView>(user));
        }
    }
}