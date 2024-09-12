using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile:Profile
    {

        public AuthorizationProfile()
        {
            AddRolesMapping();
            UpdateRoleMapping();
            GetRolesListMapping();
            ManageUserRoleMapping();
            GetClaimUserMapping();
        }

    }
}
