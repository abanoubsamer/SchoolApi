using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Roles.Queries.Response
{
    public class ManageUserRoleResponseQueries
    {
        public string UserId { get; set; }
        
        public List<Roles> Roles { get; set; }

    }
    public class Roles
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }


    }
}
