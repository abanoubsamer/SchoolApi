using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Students.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Data.Models;
using SchoolWep.Services.StudentServices.DbStudentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Commend.Handlers
{
    public class StudentCommendHandler : ResponseHandler, 
        IRequestHandler<AddStudentCommend,Response<string>>,
        IRequestHandler<UpdataStudentCommend, Response<string>>,
        IRequestHandler<DeleteStudentCommend, Response<string>>
    {

        #region Fields
        private readonly IDbStudentServices _StudentServices;
        private readonly IMapper _Mapper;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        #endregion


        #region Constrauctor
        public StudentCommendHandler(IDbStudentServices studentServices
            , IMapper Mapper
            , IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _StudentServices = studentServices;
            _Mapper = Mapper;
            _StringLocalizer= stringLocalizer;
        }
        #endregion


        #region HandleFunctions 
        public async Task<Response<string>> Handle(AddStudentCommend request, CancellationToken cancellationToken)
        {
            var StdMaping = _Mapper.Map<Student>(request);
            var result = await _StudentServices.AddStudentAsync(StdMaping);
            if (!result.Successed) return UnprocessableEntity<string>(result.Msg);
            return Created<string>((string)_StringLocalizer[SharedResourcesKey.Operations.Added]);
        }

        public async Task<Response<string>> Handle(UpdataStudentCommend request, CancellationToken cancellationToken)
        {
            var std = await _StudentServices.FindByIdAsync(request.Id);
            
            if (std == null) return NotFound<string>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

              _Mapper.Map(request,std);

            var result = await _StudentServices.UpdateStudentAsync(std);
            
            if (!result.Successed) return UnprocessableEntity<string>(result.Msg);
            
            return Success<string>(_StringLocalizer[SharedResourcesKey.Operations.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteStudentCommend request, CancellationToken cancellationToken)
        {
            var res = await _StudentServices.RemoveStudentAsync(request.Id);
            if (res.Msg== "Not Found") return NotFound<string>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);
            if (!res.Successed) return BadRequest<string>(res.Msg);
            return Deleted<string>(_StringLocalizer[SharedResourcesKey.Operations.Deleted]);
        }
        #endregion

    }
}
