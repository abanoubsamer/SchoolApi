using SchoolWep.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.AuthorizationServices.CurrentUserServicse
{
    public interface ICurrentUserServicse
    {
        public Task<ApplicationUser> CurrentUser();
        
        public string GetCurrentUserId();

        public Task<List<Claim>>  GetCurrentUserClaims();

        public Task<List<string>> GetCurrentUserRoles();

    }
}
