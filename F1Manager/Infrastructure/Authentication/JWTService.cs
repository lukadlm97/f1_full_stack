using Domain.Users;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JWTService : Application.Services.IJWTService
    {
        private readonly Domain.Configurations.JWT.JwtSettings jwtOptions;

        public JWTService(IOptions<Domain.Configurations.JWT.JwtSettings> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = new TokenBuilder()
            .AddAudience(jwtOptions.Audience)
            .AddIssuer(jwtOptions.Issuer)
            .AddExpiry(jwtOptions.TokenValidityInDays)
            .AddKey(jwtOptions.Secret)
            .AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.Role,Domain.Utilities.Constants.JwtClaims.Admin),
                    new Claim(ClaimTypes.NameIdentifier,user.UserName),
                })
            .Build();

            return tokenHandler.WriteToken(token);
        }
    }
}
