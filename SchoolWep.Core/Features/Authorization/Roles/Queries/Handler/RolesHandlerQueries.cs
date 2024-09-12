using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authorization.Roles.Commend.Models;
using SchoolWep.Core.Features.Authorization.Roles.Queries.Models;
using SchoolWep.Core.Features.Authorization.Roles.Queries.Response;
using SchoolWep.Core.SharedResources;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.AuthorizationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Roles.Queries.Handler
{
    public class RolesHandlerQueries : ResponseHandler
        , IRequestHandler<GetRolesListModelQueries, Response<List<GetRolesListResponseQueries>>>
        , IRequestHandler<GetRolesByNameModelQueries, Response<GetRolesListResponseQueries>>
        , IRequestHandler<ManageUserRoleModelQueries, Response<ManageUserRoleResponseQueries>>
       
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _mapper;
        #endregion


        #region Constructor
        public RolesHandlerQueries(IAuthorizationServices authorizationServices, UserManager<ApplicationUser> UserManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {

            _authorizationServices=authorizationServices;
            _UserManager=UserManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _stringLocalizer = stringLocalizer;
        }


        #endregion


        #region Implemntation
        public async Task<Response<List<GetRolesListResponseQueries>>> Handle(GetRolesListModelQueries request, CancellationToken cancellationToken)
        {
            var Roles =  _roleManager.Roles.ToList();

            if (Roles == null) return NotFound<List<GetRolesListResponseQueries>>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var RoleMapping = _mapper.Map<List<GetRolesListResponseQueries>>(Roles);

            return Success(RoleMapping);

        }

        public async Task<Response<GetRolesListResponseQueries>> Handle(GetRolesByNameModelQueries request, CancellationToken cancellationToken)
        {
            var Role = await _roleManager.FindByNameAsync(request.Name);
            if (Role == null) return NotFound<GetRolesListResponseQueries>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var RoleMapping = _mapper.Map<GetRolesListResponseQueries>(Role);

            return Success(RoleMapping);
        }

        public async Task<Response<ManageUserRoleResponseQueries>> Handle(ManageUserRoleModelQueries request, CancellationToken cancellationToken)
        {

            var Response = new ManageUserRoleResponseQueries();
         
         
            var user = await _UserManager.FindByIdAsync(request.UserId);
            
            if (user == null) return NotFound<ManageUserRoleResponseQueries>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

           

            Response.UserId = user.Id;

            var Role = await _authorizationServices.GetRolesUserByNameAsync(user);
           
            Response.Roles = _mapper.Map<List<Response.Roles>>(Role);

            return Success(Response);



        }
        #endregion


    }
}

