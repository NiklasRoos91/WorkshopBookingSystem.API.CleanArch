using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WorkshopBooking.API.Helpers
{
    public static class UserHelper
    {
        public static ActionResult<int> GetUserIdFromClaims(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return new UnauthorizedObjectResult("UserId saknas i token.");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return new BadRequestObjectResult("Ogiltigt UserId.");
            }

            return userId;
        }
    }
}