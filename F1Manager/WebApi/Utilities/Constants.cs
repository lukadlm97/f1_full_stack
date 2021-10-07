using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Utilities
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Role = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string Admin = "admin_access";
                public const string ContentWriter = "cw_access";
                public const string PremiumUser = "pu_access";
                public const string User = "user_access";
            }
        }
    }
}
