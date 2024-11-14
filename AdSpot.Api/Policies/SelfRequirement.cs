using Microsoft.AspNetCore.Authorization;

namespace AdSpot.Api.Policies;

public class SelfAuthorizationHandler : AuthorizationHandler<SelfRequirement, IResolverContext>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SelfRequirement requirement,
        IResolverContext resource
    )
    {
        var graphqlVariableUserId = resource.Variables.GetVariable<int>("userId");
        if (context.User.HasClaim(claim => claim.Type == "sub"))
        {
            var success = int.TryParse(context.User.FindFirstValue("sub"), out var userId);
            if (success && userId == graphqlVariableUserId)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}

public class SelfRequirement : IAuthorizationRequirement { }
