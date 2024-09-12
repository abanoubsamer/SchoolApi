using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.AuthenticationServices
{
    public interface IAuthenticationServices
    {
        public Task<DbServicesResult> Register(ApplicationUser user,string Password);
     
        public Task<SecurityToken> GenerateJWT(ApplicationUser user);
       
        public ClaimsPrincipal ValidateJwtToken(string token);

        public Task<AuthModelResult> GetTokenAsync(ApplicationUser user);


        public  Task<AuthModelResult> RefreshToken(string RefreshToken, string AccessToken);


    }
}
