using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Commend.Models
{
    public class DeleteStudentCommend:IRequest<Response<string>>
    {
     
        public int Id { get; set; }

        public DeleteStudentCommend(int id)
        {
                Id= id;
        }

    }
}
