using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Commend.Models;
using SchoolWep.Core.Features.Authentication.Queries.Models;
using SchoolWep.Data.AppMetaData;
using SchoolWepApi.Basics;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SchoolWepApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : AppControllerBasic
    {
        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost]
        [Route(Router.AuthenticationRouting.Register)]
        public async Task<IActionResult> Register(RegisterModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

        [HttpGet]
        [Route(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            return NewResult(await _Mediator.Send(new ConfirmEmailModelCommden(token)));   
        }

        [HttpGet]
        [Route(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken()
        {
            var RefreshToekn = Request.Cookies["RefreshToken"];
          
            var accessToken = Request.Headers["Authorization"].ToString();
            if (accessToken.StartsWith("Bearer "))
            {
                accessToken = accessToken.Substring("Bearer ".Length).Trim();
            }

            if (RefreshToekn == null || accessToken == null) {
                var resp = new Response<string>();
                resp.StatusCode = System.Net.HttpStatusCode.NotFound;
                resp.Message = "Not Found";
                return NewResult(resp);
            } 
            return NewResult(await _Mediator.Send(new GetTokenModelQueries(RefreshToekn,accessToken)));
        }

        [HttpPost]
       
        [Route(Router.AuthenticationRouting.Login)]
        public async Task<IActionResult> ConfirmEmail(LoginModelQueries Model)
        {  
            return NewResult(await _Mediator.Send(Model));
        }



        [HttpPost]
        [Route(Router.AuthenticationRouting.ConfirmOtpResetPass)]
        public async Task<IActionResult> ConfirmOtpResetPass(ConfirmOtoToResetPassModelCommden Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }




    }
}
