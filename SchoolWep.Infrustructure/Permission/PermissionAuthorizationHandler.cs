
using Microsoft.AspNetCore.Authorization;
using SchoolWep.Data.Helper;
using SchoolWep.InfrustructurePermission;

namespace SchoolWep.Infrustructure.Permission
{
    public class PermissionAuthorizationHandler:AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {
            
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        PermissionRequirement requirement)
        {
            if (context.User == null) return;
            var permission = context.User.Claims.Where(x => x.Type == Helpers.Permission
            && x.Value == requirement._permission
            && x.Issuer== "https://localhost:7099");
            if (permission.Any()){
                context.Succeed(requirement);
                return;
            }


        }
    }
}
