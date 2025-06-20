using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Authority;

namespace WebApiDemo.Filters.AuthFilters
{
    public class JwtAuthFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (
                !context.HttpContext.Request.Headers.TryGetValue(
                    "Authorization",
                    out var authorizationHeader
                )
            )
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string tokenString = authorizationHeader.ToString();

            if (tokenString.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                tokenString = tokenString.Substring("Bearer ".Length).Trim();
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var securityKey = configuration?["SecurityKey"] ?? string.Empty;

            // Use Task.Run to execute the async method and wait for it synchronously
            bool isValid = Task.Run(() => Authenticator.VerifyTokenAsync(tokenString, securityKey))
                .GetAwaiter()
                .GetResult();

            if (!isValid)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
