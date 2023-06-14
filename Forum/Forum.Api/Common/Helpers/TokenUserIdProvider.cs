using Forum.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum.Api.Common.Helpers;

public class TokenUserIdProvider : IUserIdProvider
{
    private readonly JwtSettings _jwtSettings;

    public TokenUserIdProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GetUserId(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        //var userId = userIdClaims;
        return userId;
    }
}
