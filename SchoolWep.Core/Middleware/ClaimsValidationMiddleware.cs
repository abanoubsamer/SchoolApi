using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SchoolWep.Services.MiddlewareServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Middleware
{
    public class ClaimsValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserClaimsService userClaimsService)
        {

            var endpoint = context.GetEndpoint();
            if (endpoint != null && endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }


            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // التأكد من أن التوكن ليس فارغًا
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // التحقق من صحة التوكن
                    var isValid = await userClaimsService.ValidateTokenClaimsAsync(token);
                    if (!isValid)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Invalid token claims.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // سجل الاستثناء إذا لزم الأمر
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error.");
                    return;
                }
            }
        

            await _next(context);
        }
    }
}
