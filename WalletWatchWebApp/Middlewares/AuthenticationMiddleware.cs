using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace WalletWatchWebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["JWT"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Items["IsAuthenticated"] = true;
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "name");
                var profilePictureClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "avatar");
                if (usernameClaim != null)
                {
                    context.Items["Username"] = usernameClaim.Value;
                }
                if (profilePictureClaim != null)
                {
                    context.Items["ProfilePictureURL"] = profilePictureClaim.Value;
                }
            }
            else
            {
                context.Items["IsAuthenticated"] = false;
            }

            await _next(context);
        }
    }
}
