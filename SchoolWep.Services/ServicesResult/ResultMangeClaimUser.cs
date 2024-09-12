using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.ServicesResult
{
    public class ResultMangeClaimUser: DbServicesResult
    {
        public string UserId { get; set; }

        public UserClaims UserClaims { get; set; } = new();
        public List<RoleClaims> RoleClaims { get; set; } = new();
    }

    public class UserClaims
    {
        public List<Calims> Calims { get; set; }
    }
    public class RoleClaims
    {
        public string Name { get; set; }
        public List<Calims> Calims { get; set; }
    }
    public class Calims
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
