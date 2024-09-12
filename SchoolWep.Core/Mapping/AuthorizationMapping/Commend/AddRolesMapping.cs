using Microsoft.AspNetCore.Identity;
using SchoolWep.Core.Features.Authorization.Roles.Commend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void AddRolesMapping()
        {
            CreateMap<AddRolesModelCommend, IdentityRole>();
        }

    }
}
