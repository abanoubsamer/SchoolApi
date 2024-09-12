using SchoolWep.Core.Features.Departments.Queries.Models;
using SchoolWep.Core.Features.Departments.Queries.Responses;
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
         public void GetDepartmentList()
         {
            CreateMap<Department, GetDepartmentsQueriesResponse>()
                  .ForMember(des => des.Manager, src => 
                  src.MapFrom(src => src.InstructorManger.Localize(src.InstructorManger.FirstNameAr, src.InstructorManger.FirstNameEn) 
                  +  " " + src.InstructorManger.Localize(src.InstructorManger.LastNameAr, src.InstructorManger.LastNameEn)))
                  .ForMember(des=>des.Name,src=>src.MapFrom(src=>src.Localize(src.NameAr,src.NameEn)));            
         }

    }
}
