using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolWep.Data.Models;

using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Core.Basics;

namespace SchoolWep.Core.Features.Students.Queries.Models
{
    /////////////// request /////////////////////Respond
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponed>>>
    {


    }
}
