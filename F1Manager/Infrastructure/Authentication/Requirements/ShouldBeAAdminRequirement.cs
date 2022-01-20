using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Authentication.Requirements
{
    public class ShouldBeAAdminRequirement: IAuthorizationRequirement
    {
        public ShouldBeAAdminRequirement()
        {

        }
    }
}
