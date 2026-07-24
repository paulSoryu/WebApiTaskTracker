using System.Security.Claims;

namespace WebApiTaskTracker.Utilities;

// Extension method to get the user ID from the ClaimsPrincipal
public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
        {
            throw new UnauthorizedAccessException("User not authorized.");
        }

        return userId;
    }
}
