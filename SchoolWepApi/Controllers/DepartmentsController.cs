using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolWep.Core.Features.Departments.Commend.Models;
using SchoolWep.Core.Features.Departments.Queries.Models;
using SchoolWep.Data.AppMetaData;
using SchoolWep.Data.Constans;
using SchoolWepApi.Basics;


namespace SchoolWepApi.Controllers
{
  
    [ApiController]
    public class DepartmentsController : AppControllerBasic
    {
        public DepartmentsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route(Router.DepartmentRouting.List)]
        [Authorize(Permission.Department.View)]
        public async Task<IActionResult> GetDepartmentList()
        {
            return NewResult(await _Mediator.Send(new GetDepartmentsModelQueries()));
        }

        [HttpGet]
        [Route(Router.DepartmentRouting.GetByIdPagination)]
        [Authorize(Permission.Department.View)]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentsByIdModelQueries Model)
        {
            
            return NewResult(await _Mediator.Send(Model));
        }



        [HttpGet]
        [Route(Router.DepartmentRouting.Pagination)]
        [Authorize(Permission.Department.View)]
        public async Task<IActionResult> GetDepartmentPagination([FromQuery] GetDepartmentsPaginationModelQueries Model)
        {
            return Ok(await _Mediator.Send(Model));
        }



        [HttpDelete]
        [Route(Router.DepartmentRouting.Delete)]
        [Authorize(Permission.Department.Delete)]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            return NewResult(await _Mediator.Send(new DeleteDepartmentModelCommend(Id)));
        }

        [HttpPost]
        [Route(Router.DepartmentRouting.Add)]
        [Authorize(Permission.Department.Create)]
        public async Task<IActionResult> AddDepartment(AddDepartmentModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

        [HttpPut]
        [Route(Router.DepartmentRouting.Update)]
        [Authorize(Permission.Department.Edit)]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentModelCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

    }
}
