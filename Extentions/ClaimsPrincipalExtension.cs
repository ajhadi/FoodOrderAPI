using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FoodOrderAPI.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user, bool allowNull = false)
        {
            var userIds = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub" || c.Type == JwtRegisteredClaimNames.Sub).ToList();
            if (userIds.Count > 1)
            {
                throw new InvalidOperationException($"UserId contains many values: {string.Join(", ", userIds.Select(id => id.Value))}");
            }
            var userId = userIds.SingleOrDefault();
            if (userId == null && !allowNull)
                throw new System.Exception("Missing sub claim");
            return new Guid(userId?.Value);
        }
        public static string GetRole(this ClaimsPrincipal user)
        {
            var userId = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);
            if (userId == null)
                throw new System.Exception("Missing roles");
            return userId.Value;
        }
    }
}