using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Commend.Models
{
    public class DeleteDepartmentModelCommend:IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteDepartmentModelCommend(int id)
        {
            Id=id;
        }
    }
}
