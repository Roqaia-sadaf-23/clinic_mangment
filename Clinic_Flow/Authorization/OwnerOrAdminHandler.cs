using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Clinic_Flow.Authorization
{
   

    // This authorization handler enforces the ownership rule for student resources.
    // It checks whether the current user is either:
    // - An Admin (full access), OR
    // - The owner of the student record being requested
    public class OwnerOrAdminHandler
        : AuthorizationHandler<OwnerOrAdminRequirement, int>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OwnerOrAdminRequirement requirement,
            int Id)
        {
            // Admin override
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Ownership check
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out int authenticatedId) &&
                authenticatedId == Id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
