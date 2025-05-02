using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace MiraeDigital.BffMobile.Application.Extensions
{
    public static class JwtTokenExtensions
    {
        public const string BearerScheme = "Bearer";

        public static CustomClaimsValue GetCustomClaimsValues(this string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomClaimsValue();

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = jwtTokenHandler.ReadJwtToken(jwtToken);

            string userId = securityToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.UserId)?.Value;
            string document = securityToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Document)?.Value;
            string name = securityToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Name)?.Value;
            string username = securityToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.UserName)?.Value;
            string customerId = securityToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.CustomerId)?.Value;
            string investorId = securityToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.InvestorId)?.Value;

            return new CustomClaimsValue(userId, document, name, username, customerId, investorId);
        }

        public static string RemoveBearer(this string jwtToken) 
            => jwtToken.StartsWith(BearerScheme, System.StringComparison.OrdinalIgnoreCase) ? jwtToken[BearerScheme.Length..].Trim() : jwtToken;

        public static string AddBearer(this string jwtToken)
            => !jwtToken.StartsWith(BearerScheme, System.StringComparison.OrdinalIgnoreCase)? $"{BearerScheme} {jwtToken.Trim()}" : jwtToken;
    }
}
