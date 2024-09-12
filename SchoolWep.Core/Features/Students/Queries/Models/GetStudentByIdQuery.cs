using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery:IRequest<Response<GetSingelStudentResponse>>
    {
        public int Id { get; set; }

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }

    }
}
