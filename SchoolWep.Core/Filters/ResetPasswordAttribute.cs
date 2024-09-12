using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using static SchoolWep.Data.Constans.Permission;

namespace SchoolWep.Core.Filters
{
    public class ResetPasswordAttribute : ActionFilterAttribute
    {



        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token.IsNullOrEmpty())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jwthandeler = new JwtSecurityTokenHandler();
            var AccessToken = jwthandeler.ReadToken(token) as JwtSecurityToken;
            var claims = AccessToken?.Claims;
            if (claims == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            var match = claims.Any(x => x.Type == "ResetPassword" && x.Value == "true");
            if (!match)
            {
                context.Result = new ForbidResult();
                return;
            }
           
             await next();


        }
    }
}
