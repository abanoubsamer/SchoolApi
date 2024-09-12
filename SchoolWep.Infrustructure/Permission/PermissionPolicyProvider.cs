
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using SchoolWep.Data.Helper;

namespace SchoolWep.InfrustructurePermission
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider  FallBackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallBackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public async Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
           return await FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public async Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return await FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(Helpers.Permission, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }
            return  FallBackPolicyProvider.GetPolicyAsync(policyName);
            
        }
    }
}
