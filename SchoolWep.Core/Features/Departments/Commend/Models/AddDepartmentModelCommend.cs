using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Commend.Models
{
    public class AddDepartmentModelCommend :IRequest<Response<string>>
    {
        public string NameAr { get; set; }
        
        public string NameEn { get; set; }

        public int? ManagerId { get; set; }

        public int Capsity { get; set; }

    }
}
