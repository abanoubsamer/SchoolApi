using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Commend.Models
{
    public class UpdataStudentCommend : IRequest<Response<string>>
    {
        public  int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public DateTime? BirthDay { get; set; }

        public decimal? Gpa { get; set; }

        public int LevelId { get; set; }

        public int DepartmentId { get; set; }


        public string? Country { get; set; }


        public string? City { get; set; }

        public int? Postal_Code { get; set; }


    }
   
}
