using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Dtos;
using SchoolWep.Core.Features.User.Commend.Models;
using SchoolWep.Core.Features.User.Queries.Modles;
using SchoolWep.Core.Filters;
using SchoolWep.Data.AppMetaData;
using SchoolWep.Data.Constans;
using SchoolWepApi.Basics;
using System.Security.Claims;
using static SchoolWep.Data.Constans.Permission;

namespace SchoolWepApi.Controllers
{

    [ApiController]
    public class UserController : AppControllerBasic
    {
        private readonly IMediator _Mediator;

        public UserController(IMediator mediator) : base(mediator)
        {
            _Mediator=mediator;
        }


        [HttpGet]
        [Authorize(Permission.User.View)]
        [Route(Router.UserRouting.List)]
        public async Task<IActionResult> GetListUser()
        {
            return NewResult(await _Mediator.Send(new GetUserListModelQueries()));
        }

        [HttpGet]
        [Authorize(Permission.User.View)]
        [Route(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetListUser(string Id)
        {
            return NewResult(await _Mediator.Send(new GetUserByIdModelQueries(Id)));
        }


        [HttpPut]
        [Authorize(Permission.User.Edit)]
        [Route(Router.UserRouting.Update)]
        public async Task<IActionResult> UpdateUser(UpdateUserModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }
          
        [HttpPut]
        [Authorize(Permission.User.Edit)]
        [Route(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePasswordUser(ChangePasswordUserModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

        [HttpDelete]
        [Route(Router.UserRouting.Delete)]
        [Authorize(Permission.User.Delete)]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            return NewResult(await _Mediator.Send(new DeleteUserModelCommend(Id)));
        }

        [HttpPost]
        [Route(Router.UserRouting.SendOtpToResetPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordSendEmail(SendOtpResetPasswordUserModelCommend model)
        {
            return NewResult(await _Mediator.Send(model));
        }
        [HttpPost]
        [Route(Router.UserRouting.ResetPassword)]
        [ResetPassword]
        public async Task<IActionResult> ResetPassword(ResetPassworDto model)
        {

            var UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (UserId.IsNullOrEmpty())
            {
                return new UnauthorizedResult(); 
            }

            var ResetPassword = new ResetPasswordUserModelCommend
            {
                 UserID = UserId,
                 NewPasswoed= model.NewPasswoed,
                 ComparePassword = model.ComparePassword,
            };
            
            return NewResult(await _Mediator.Send(ResetPassword));
        }


    }
}
