using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Departments.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Data.Models;
using SchoolWep.Services.DepartmentServices.DbDepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Commend.Handlers
{
    public class HandlerDepartmentCommend :ResponseHandler 
        , IRequestHandler<DeleteDepartmentModelCommend, Response<string>>
        , IRequestHandler<AddDepartmentModelCommend, Response<string>>
        , IRequestHandler<UpdateDepartmentModelCommend, Response<string>>
    {

        #region Fields
        private readonly IDbDepartmentServices _dbDepartmentServices;
        private readonly IMapper _Mapper;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        #endregion
        #region Constructor
        public HandlerDepartmentCommend(
              IDbDepartmentServices dbDepartmentServices
            , IMapper Mapper
            , IStringLocalizer<SharedResource> StringLocalizer):base(StringLocalizer)
        {
            _dbDepartmentServices= dbDepartmentServices;
            _Mapper= Mapper;
            _StringLocalizer= StringLocalizer;
        }
        #endregion

        #region Implemntation
        public async Task<Response<string>> Handle(DeleteDepartmentModelCommend request, CancellationToken cancellationToken)
        {
            var dep = await _dbDepartmentServices.FindByIdWihtNotTracking(request.Id);
            if (dep == null) return NotFound<string>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);
            var result= await _dbDepartmentServices.DeleteDepartment(dep);
            if (!result.Successed) return BadRequest<string>(result.Msg);
            return Deleted<string>(_StringLocalizer[SharedResourcesKey.Operations.Deleted]);
        }

        public async Task<Response<string>> Handle(AddDepartmentModelCommend request, CancellationToken cancellationToken)
        {
            var DepMapping = _Mapper.Map<Department>(request);
            var result =  await _dbDepartmentServices.AddDepartment(DepMapping);
            if (!result.Successed) return BadRequest<string>(result.Msg);
            return Created<string>(_StringLocalizer[SharedResourcesKey.Operations.Added]);
        }

        public async Task<Response<string>> Handle(UpdateDepartmentModelCommend request, CancellationToken cancellationToken)
        {
            var Dep = await _dbDepartmentServices.FindByIdWihtNotTracking(request.Id);
            if (Dep == null) return NotFound<string>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);
             _Mapper.Map(request,Dep);
            var result = await _dbDepartmentServices.UpdateDepartment(Dep);
            if (!result.Successed) return BadRequest<string>(result.Msg);

            return Success<string>(_StringLocalizer[SharedResourcesKey.Operations.Updated]);
        }

        #endregion


    }
}
