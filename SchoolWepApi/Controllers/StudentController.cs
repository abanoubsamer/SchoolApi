using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolWep.Core.Features.Students.Commend.Models;
using SchoolWep.Core.Features.Students.Queries.Models;
using SchoolWep.Data.AppMetaData;
using SchoolWep.Data.Constans;
using SchoolWepApi.Basics;

namespace SchoolWepApi.Controllers
{

    [ApiController]

    public class StudentController : AppControllerBasic
    {
        public StudentController(IMediator mediator) : base(mediator)
        {

        }
        
        [HttpGet]
        [Route(Router.StudentRouting.List)]
        [Authorize(Permission.Studnet.View)]
        public async Task<IActionResult> GetStudentsList()
        { 
            return NewResult(await _Mediator.Send(new GetStudentListQuery()));
        }

        [HttpGet]
        [Route(Router.StudentRouting.Pagination)]
        [Authorize(Permission.Studnet.View)]
        public async Task<IActionResult> GetStudentsPaginationList([FromQuery] GetStudentPaginationQuery query)
        {
            return Ok(await _Mediator.Send(query));
        }

        [HttpGet]
        [Route(Router.StudentRouting.GetById)]
        [Authorize(Permission.Studnet.View)]
        public async Task<IActionResult> GetStudentById([FromRoute] int Id)
        {
            return NewResult(await _Mediator.Send(new GetStudentByIdQuery(Id)));
        }

        [HttpPost]
        [Route(Router.StudentRouting.Add)]
        [Authorize(Permission.Studnet.Create)]
        public async Task<IActionResult> AddStudent(AddStudentCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }

        [HttpPut]
        [Route(Router.StudentRouting.Update)]
        [Authorize(Permission.Studnet.Edit)]
        public async Task<IActionResult> UpdateStudent(UpdataStudentCommend Model)
        {
            return NewResult(await _Mediator.Send(Model));
        }


        [HttpDelete]
        [Route(Router.StudentRouting.Delete)]
        [Authorize(Permission.Studnet.Delete)]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            return NewResult(await _Mediator.Send(new DeleteStudentCommend(Id)));
        }

    }
}
