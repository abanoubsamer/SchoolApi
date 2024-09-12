using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using SchoolWep.Core.Features.Authorization.Roles.Queries.Response;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void ManageUserRoleMapping()
        {
            CreateMap<IdentityRole, Roles>()
                .ForMember(des => des.RoleId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.RoleName, src => src.MapFrom(src => src.Name));
        }

    }
}
