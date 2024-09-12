using SchoolWep.Core.Features.Departments.Commend.Models;
using SchoolWep.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.DpartmentMapping
{
    public partial class DepartmentProfile
    {

        public void AddDepartmnetMapping()
        {
            CreateMap<AddDepartmentModelCommend, Department>()
                .ForMember(des => des.InstManger, src => src.MapFrom(src => src.ManagerId));
               
        }

    }
}
