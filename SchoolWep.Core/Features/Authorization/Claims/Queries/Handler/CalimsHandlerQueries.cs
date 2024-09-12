using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authorization.Claims.Queries.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.AuthorizationServices;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Claims.Queries.Handler
{
    public class CalimsHandlerQueries : ResponseHandler,IRequestHandler<GetUserCliamsModelQueries,Response<ResultMangeClaimUser>>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _mapper;

        public CalimsHandlerQueries(IAuthorizationServices authorizationServices, UserManager<ApplicationUser> UserManager, IMapper mapper, RoleManager<IdentityRole> roleManager,IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _authorizationServices = authorizationServices;
            _UserManager = UserManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<ResultMangeClaimUser>> Handle(GetUserCliamsModelQueries request, CancellationToken cancellationToken)
        {
            var user = await _UserManager.FindByIdAsync(request.UserId);
            if (user == null) return NotFound<ResultMangeClaimUser>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var result = await _authorizationServices.GetUserClaims(user);
            if (!result.Successed) return BadRequest<ResultMangeClaimUser>(result.Msg);

            return Success(result);

        }
    }
}
