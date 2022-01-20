using Infrastructure.Authentication.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Handlers
{
    public class ShouldBeAAdminAuthorizationHandler : AuthorizationHandler<ShouldBeAAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        ShouldBeAAdminRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            var role = context.User.Claims.FirstOrDefault(
               x => x.Type == ClaimTypes.Role).Value;

            if (role == Domain.Utilities.Constants.JwtClaims.Admin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}