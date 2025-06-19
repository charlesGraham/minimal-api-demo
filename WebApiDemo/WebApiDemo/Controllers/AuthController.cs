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
            if (AppRepo.Authenticate(credential.ClientId, credential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok(
                    new
                    {
                        access_token = CreateToken(credential.ClientId, expiresAt),
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

        private string CreateToken(string clientId, DateTime expiresAt)
        {
            var app = AppRepo.GetApplicationByClientId(clientId);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(
                        _configuration["SecurityKey"] ?? string.Empty
                    )
                ),
                SecurityAlgorithms.HmacSha256Signature
            );

            var claimsDict = new Dictionary<string, object>
            {
                { "AppName", app?.ApplicationName ?? string.Empty },
                { "Read", (app?.Scopes ?? string.Empty).Contains("read") ? true : false },
                { "Write", (app?.Scopes ?? string.Empty).Contains("write") ? true : false },
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = signingCredentials,
                Claims = claimsDict,
                Expires = expiresAt,
                NotBefore = DateTime.UtcNow,
            };

            var tokenHandler = new JsonWebTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
