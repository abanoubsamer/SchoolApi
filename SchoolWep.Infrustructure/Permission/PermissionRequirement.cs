using Microsoft.AspNetCore.Authorization;

namespace SchoolWep.InfrustructurePermission
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string _permission { get;private set; }
        public PermissionRequirement(string permission)
        {
            _permission = permission;
        }
    }
}
