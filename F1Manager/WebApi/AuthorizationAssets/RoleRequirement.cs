using Microsoft.AspNetCore.Authorization;

namespace WebApi.AuthorizationAssets
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; set; }

        public RoleRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}