using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.DpartmentMapping
{
    public partial class DepartmentProfile : Profile
    {

        public DepartmentProfile()
        {
            GetDepartmentList(); 
            GetDepartmentById();
           AddDepartmnetMapping();
            UpdateDepartmentMapping();
        }
    }
}
