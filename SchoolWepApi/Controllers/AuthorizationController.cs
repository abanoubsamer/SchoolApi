using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolWep.Core.Features.Authorization.Claims.Commend.Models;
using SchoolWep.Core.Features.Authorization.Claims.Queries.Models;
using SchoolWep.Core.Features.Authorization.Roles.Commend.Models;
using SchoolWep.Core.Features.Authorization.Roles.Queries.Models;
using SchoolWep.Data.AppMetaData;
using SchoolWep.Data.Constans;
using SchoolWep.Services.ServicesResult;
using SchoolWepApi.Basics;

namespace SchoolWepApi.Controllers
{

    [ApiController]
    public class AuthorizationController : AppControllerBasic
    {
        public AuthorizationController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet]
        [Authorize(Permission.Permissions.View)]
        [Route(Router.AuthorizationRouting.RoleRouting.List)]
        public async Task<IActionResult> GetListRole()
        {
            return NewResult(await _Mediator.Send(new GetRolesListModelQueries ()));
        }
        [HttpGet]
        [Route(Router.AuthorizationRouting.RoleRouting.GetByName)]
        [Authorize(Permission.Permissions.View)]
        public async Task<IActionResult> GetByNameRole(string Name)
        {
            return NewResult(await _Mediator.Send(new GetRolesByNameModelQueries(Name)));
        }


        [HttpPost]
        [Route(Router.AuthorizationRouting.RoleRouting.Add)]
        [Authorize(Permission.Permissions.Create)]
        public async Task<IActionResult> AddRole( AddRolesModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }
    
        [HttpPut]
        [Route(Router.AuthorizationRouting.RoleRouting.Update)]
        [Authorize(Permission.Permissions.Edit)]
        public async Task<IActionResult> UpdateRole(UpdateRolesModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

        [HttpDelete]
        [Route(Router.AuthorizationRouting.RoleRouting.Delete)]
        [Authorize(Permission.Permissions.Delete)]
        public async Task<IActionResult> DeleteRole(string Name)
        {
            return NewResult(await _Mediator.Send(new DeleteRoleModelCommend(Name)));
        }

        [HttpPut]
        [Route(Router.AuthorizationRouting.RoleRouting.UpdateCalim)]
        [Authorize(Permission.Permissions.Edit)]
        public async Task<IActionResult> UpdateRoleClaims(UpdateRoleClaimModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }


        [HttpGet]
        [Route(Router.AuthorizationRouting.AuthUserRouting.GetRoles)]
        [Authorize(Permission.Permissions.View)]
        public async Task<IActionResult> GetUserRoles(string Id)
        {
            return NewResult(await _Mediator.Send(new ManageUserRoleModelQueries(Id)));
        }


        [HttpPost]
        [Route(Router.AuthorizationRouting.AuthUserRouting.Add)]
        [Authorize(Permission.Permissions.Create)]
        public async Task<IActionResult> AddUserRoles(AddUserRoleModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }


        [HttpDelete]
        [Route(Router.AuthorizationRouting.AuthUserRouting.Delete)]
        [Authorize(Permission.Permissions.Delete)]
        public async Task<IActionResult> DeleteUserRoles(DeleteUserRoleModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }


        [HttpGet]
        [Route(Router.AuthorizationRouting.AuthUserRouting.GetClaims)]
        [Authorize(Permission.Permissions.View)]
        public async Task<IActionResult> GetUserClaims(string Id)
        {
            return NewResult(await _Mediator.Send(new GetUserCliamsModelQueries(Id)));
        }

        [HttpPut]
        [Route(Router.AuthorizationRouting.AuthUserRouting.UpdateCalim)]
        [Authorize(Permission.Permissions.Edit)]
        public async Task<IActionResult> UpdateUserClaims(UpdateUserClaimModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

    }
}
