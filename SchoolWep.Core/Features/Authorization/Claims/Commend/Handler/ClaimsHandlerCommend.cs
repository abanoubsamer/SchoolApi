using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authorization.Claims.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.AuthorizationServices;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Claims.Commend.Handler
{
    public class ClaimsHandlerCommend : ResponseHandler
        , IRequestHandler<UpdateUserClaimModelCommend, Response<string>>
        , IRequestHandler<UpdateRoleClaimModelCommend, Response<string>>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _mapper;

        public ClaimsHandlerCommend(IAuthorizationServices authorizationServices, UserManager<ApplicationUser> UserManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _authorizationServices = authorizationServices;
            _UserManager = UserManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimModelCommend request, CancellationToken cancellationToken)
        {
         
            var result = await _authorizationServices.UpdateUserClaims(request.UserId,request.UserClaims);
         
            if (!result.Successed) return BadRequest<string>(result.Msg);

            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Updated]);

         }

        public async Task<Response<string>> Handle(UpdateRoleClaimModelCommend request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.UpdateRoleClaims(request.RoleClaims);
            if (!result.Successed) return BadRequest<string>(result.Msg);

            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Updated]);
        }
    }
}
