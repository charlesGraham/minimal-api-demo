using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace WebApiDemo.Authority
{
    public static class Authenticator
    {
        public static bool Authenticate(string clientId, string secret)
        {
            var app = AppRepo.GetApplicationByClientId(clientId);
            if (app == null)
                return false;

            return app.ClientId == clientId && app.ClientSecret == secret;
        }

        public static string CreateToken(string clientId, DateTime expiresAt, string strSecretKey)
        {
            var app = AppRepo.GetApplicationByClientId(clientId);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(strSecretKey)),
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

        public static async Task<bool> VerifyTokenAsync(string tokenString, string securityKey)
        {
            if (string.IsNullOrWhiteSpace(tokenString) ||  string.IsNullOrWhiteSpace(securityKey))
                return false;

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(securityKey);
            var tokenHandler =  new JsonWebTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false, // for internal use only
                ValidateAudience = false, // for internal use only
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero

            };

            try
            {
                var result = await tokenHandler.ValidateTokenAsync(tokenString, validationParameters);
                return result.IsValid;
            }
            catch (SecurityTokenMalformedException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
