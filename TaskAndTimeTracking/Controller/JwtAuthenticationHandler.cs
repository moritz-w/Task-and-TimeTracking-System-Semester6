using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskAndTimeTracking.Controller.interfaces;

namespace TaskAndTimeTracking.Controller
{
    public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IAuthControllerConfiguration Configuration;

        public JwtAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthControllerConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            Configuration = configuration;
        }
        
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authHeader = Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer"))
            {
                return AuthenticateResult.Fail("No token provided");
            }

            string jwtoken = authHeader.Substring("Bearer ".Length).Trim();
            ClaimsPrincipal principal = GetPrincipalFromJwt(jwtoken);
            var principalIdentity = principal.Identity as ClaimsIdentity;
            
            if (!principalIdentity.IsAuthenticated)
                return AuthenticateResult.Fail("Token not valid");

            
            return AuthenticateResult.Success(new AuthenticationTicket(principal, "Bearer"));
        }


        
        private ClaimsPrincipal GetPrincipalFromJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (securityToken == null)
            {
                return null;
            }
            
            var secret = Convert.FromBase64String(Configuration.Secret);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(secret)
            };
            
            SecurityToken sToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out sToken);

            return principal;
        }
    }
}