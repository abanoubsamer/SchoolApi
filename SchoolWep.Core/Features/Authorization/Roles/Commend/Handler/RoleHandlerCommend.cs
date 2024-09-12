using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;

using SchoolWep.Core.Features.Authorization.Roles.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.AuthorizationServices;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Roles.Commend.Handler
{
    public class RoleHandlerCommend : ResponseHandler
        , IRequestHandler<AddRolesModelCommend, Response<string>>
        , IRequestHandler<UpdateRolesModelCommend, Response<string>>
        , IRequestHandler<DeleteRoleModelCommend, Response<string>>
        , IRequestHandler<AddUserRoleModelCommend, Response<string>>
        , IRequestHandler<DeleteUserRoleModelCommend, Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly IUserServices _UserServices;
        #endregion


        #region Constructor
        public RoleHandlerCommend(IUserServices userServices, IAuthorizationServices authorizationServices, IMapper mapper,RoleManager<IdentityRole> roleManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _UserServices= userServices;
            _authorizationServices = authorizationServices;
            _mapper = mapper;
            _roleManager=roleManager;
            _stringLocalizer= stringLocalizer;
        }
        #endregion


        #region Implemntation
        public async Task<Response<string>> Handle(AddRolesModelCommend request, CancellationToken cancellationToken)
        {
            var RoleMapping = _mapper.Map<IdentityRole>(request);

            if (RoleMapping == null) return BadRequest<string>("Invalid Mapping");

          
            var result = await _roleManager.CreateAsync(RoleMapping);
            if (!result.Succeeded)
            {
                var Errors = string.Empty;
                foreach (var Error in result.Errors)
                {
                    Errors += $"{Error.Description} :";
                }
                return BadRequest<string>(Errors);
            }

            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Added]);

        }

        public async Task<Response<string>> Handle(UpdateRolesModelCommend request, CancellationToken cancellationToken)
        {

            var Role = await _roleManager.FindByNameAsync(request.OldName);
         
            if (Role == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);
            
            var RoleMapping = _mapper.Map(request, Role);

            var result = await _roleManager.UpdateAsync(RoleMapping);
            if (!result.Succeeded)
            {
                var Errors = string.Empty;
                foreach (var Error in result.Errors)
                {
                    Errors += $"{Error.Description} :";
                }
                return BadRequest<string>(Errors);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Updated]);

        }

        public async Task<Response<string>> Handle(DeleteRoleModelCommend request, CancellationToken cancellationToken)
        {
            var Role = await _roleManager.FindByNameAsync(request.Name);
            if (Role == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var result = await _roleManager.DeleteAsync(Role);
            if (!result.Succeeded) {
                var Errors = string.Empty;
                foreach (var Error in result.Errors)
                {
                    Errors += $"{Error.Description} :";
                }
                return BadRequest<string>(Errors);

            }
            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Deleted]);
        }

        public async Task<Response<string>> Handle(AddUserRoleModelCommend request, CancellationToken cancellationToken)
        {
            var user = await _UserServices.FindUserById(request.UserId);
            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var resultRange = await _authorizationServices.AddUserRoleRengeAsync(user, request.Roles);
            if (!resultRange.Successed) return BadRequest<string>(resultRange.Msg);
            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Added]);

        }

        public async Task<Response<string>> Handle(DeleteUserRoleModelCommend request, CancellationToken cancellationToken)
        {
            var user = await _UserServices.FindUserById(request.UserId);
            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var result = await _authorizationServices.RoleIsExsitsInUser(user, request.Roles);
            if (!result) return BadRequest<string>("Roles Is Not Exsits In User");

            var resultdelete= await _authorizationServices.DeleteUserRoleAsync(user, request.Roles);
            if(!resultdelete.Successed) return BadRequest<string>(resultdelete.Msg);

            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Deleted]);

        }
        #endregion


    }
}
