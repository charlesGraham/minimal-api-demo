using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WebApiDemo.Authority;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost()]
        public IActionResult Authenticate([FromBody] AppCredential credential)
        {
            if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok(
                    new
                    {
                        access_token = Authenticator.CreateToken(credential.ClientId, expiresAt, _configuration["SecurityKey"] ?? string.Empty),
                        expires_at = expiresAt,
                    }
                );
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "Invalid client id or secret.");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized,
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }

    }
}
