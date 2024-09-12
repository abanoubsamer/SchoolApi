using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Students.Queries.Models;
using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Core.Pagination;
using SchoolWep.Core.Pagination.Extensions;
using SchoolWep.Data.Models;
using SchoolWep.Services.StudentServices.DbStudentServices;
using SchoolWep.Core.SharedResources;

namespace SchoolWep.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler, 
        IRequestHandler<GetStudentListQuery,Response<List<GetStudentListResponed>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetSingelStudentResponse>>,
        IRequestHandler<GetStudentPaginationQuery,PaginationResult<GetStudentPaginationListResponse>>

    {


        #region Fields
        private readonly IDbStudentServices _StudentServices;
        private readonly IMapper _Mapper;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        #endregion


        #region Constrauctor
        public StudentQueryHandler(IDbStudentServices studentServices,
            IMapper Mapper,
            IStringLocalizer<SharedResource> StringLocalizer):base(StringLocalizer)
        {
            _StudentServices = studentServices;
            _Mapper=Mapper;
            _StringLocalizer = StringLocalizer;
        }
        #endregion


        #region HandleFunctions
        public async Task<Response<List<GetStudentListResponed>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var Std = await _StudentServices.GetAllStudentsAsync();

            var Dto = _Mapper.Map<List<GetStudentListResponed>>(Std);

             var result= Success(Dto);
            result.Meta = new
            {
                Count = Dto.Count(),
                //GreaterStduent=Dto.OrderByDescending(s=>s.Age).FirstOrDefault(),
                //LessStudent= Dto.OrderBy(s => s.Age).FirstOrDefault()

            };
            return result;
        }

        public async Task<Response<GetSingelStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var Std = await _StudentServices.FindByIdAsync(request.Id);

            if (Std == null) return NotFound<GetSingelStudentResponse>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);


            var StdDto= _Mapper.Map<GetSingelStudentResponse>(Std);

            return Success(StdDto);
        }

        public async Task<PaginationResult<GetStudentPaginationListResponse>>Handle(GetStudentPaginationQuery request, CancellationToken cancellationToken)
        {
            var expression = _StudentServices.CreateExpression(
                e => new GetStudentPaginationListResponse(
                    e.Id,
                    e.Localize(e.FirstNameAr + " " + e.LastNameAr, e.FirstNameEn + " " + e.LastNameEn)
                    , e.BirthDay
                    , e.Gpa
                    , e.Department.Localize(e.Department.NameAr,e.Department.NameEn)
                    , e.level.Localize(e.level.NameAr, e.level.NameEn)));
            // var Queryable = _StudentServices.GetQueryable();
            var FilterQuery = _StudentServices.FilterStudentPagination(request.Oreder,request.OrederBy,request.Search);
            var PaginationList = await FilterQuery.Select(expression).ToPaginationListAsync(request.PageNumber, request.PageSize);
            PaginationList.Meta = new 
            {
                Count = PaginationList.Data.Count(),
                Date = DateTime.Now.ToShortDateString() 
                };
            return PaginationList;
        
        }




        #endregion

    }
}
