using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Departments.Queries.Models;
using SchoolWep.Core.Features.Departments.Queries.Responses;
using SchoolWep.Core.Pagination;
using Microsoft.EntityFrameworkCore;
using SchoolWep.Core.Pagination.Extensions;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.DepartmentServices.DbDepartmentServices;
using SchoolWep.Data.Models;


namespace SchoolWep.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentHandlerQueries :ResponseHandler 
        , IRequestHandler<GetDepartmentsModelQueries, Response<List<GetDepartmentsQueriesResponse>>>
        , IRequestHandler<GetDepartmentsByIdModelQueries, Response<GetDepartmentsByIdQueriesResponse>>
        , IRequestHandler<GetDepartmentsPaginationModelQueries, PaginationResult<GetDepartmentsPaginationResposnesQueries>>
        
    {


        #region Fields
        private readonly IDbDepartmentServices _dbDepartmentServices;
        private readonly IMapper _Mapper;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        #endregion




        #region Counstuctor
        public DepartmentHandlerQueries(IDbDepartmentServices dbDepartmentServices,IMapper mapper
            ,IStringLocalizer<SharedResource> stringLocalizer):base(stringLocalizer)
        {
            _StringLocalizer=stringLocalizer;
            _Mapper=mapper;
            _dbDepartmentServices = dbDepartmentServices;
        }


        #endregion


        #region Implemntation
        public async Task<Response<List<GetDepartmentsQueriesResponse>>> Handle(GetDepartmentsModelQueries request, CancellationToken cancellationToken)
        {
            var Dep = await _dbDepartmentServices.GetDepartmentList();

            if (Dep == null) return NotFound<List<GetDepartmentsQueriesResponse>>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var DepMapping = _Mapper.Map<List<GetDepartmentsQueriesResponse>>(Dep);

            return Success(DepMapping);
        }

        public async Task<Response<GetDepartmentsByIdQueriesResponse>> Handle(GetDepartmentsByIdModelQueries request, CancellationToken cancellationToken)
        {
          
          var dep= await _dbDepartmentServices.FindByIdWihtNotTracking(request.Id);
         
            if (dep == null) return NotFound<GetDepartmentsByIdQueriesResponse>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var DepMapping = _Mapper.Map<GetDepartmentsByIdQueriesResponse>(dep);

            var student = _dbDepartmentServices.AsQueryableConvert(dep.Studnets);
               
            if (student!=null)
                 DepMapping.StudentList = await _Mapper.ProjectTo<StudentResponseDep>(student)
                    .ToPaginationListAsync(request.PageNumber, request.PageSize);

            return Success(DepMapping);
        }

        public async Task<PaginationResult<GetDepartmentsPaginationResposnesQueries>> Handle(GetDepartmentsPaginationModelQueries request, CancellationToken cancellationToken)
        {
           
            var expressin = _dbDepartmentServices.CreateExprestion(
                dep => new GetDepartmentsPaginationResposnesQueries(
                dep.Id,
                dep.Localize(dep.NameAr, dep.NameEn),
                dep.Capsity,
                dep.InstructorManger?.Localize(dep.InstructorManger.FirstNameAr, dep.InstructorManger.FirstNameEn) + 
                " " + dep.InstructorManger?.Localize(dep.InstructorManger.LastNameAr, dep.InstructorManger.LastNameEn) ));

            var Filter = _dbDepartmentServices.FilterDepartmentPagination(request.Oreder, request.OrederBy, request.Search);
            var PangintionList = await Filter.Select(expressin).ToPaginationListAsync(request.NumberPage,request.SizePage);

            return PangintionList;

        }

        #endregion

    }
}
