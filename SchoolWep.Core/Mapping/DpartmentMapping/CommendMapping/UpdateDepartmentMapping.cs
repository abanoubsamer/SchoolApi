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

        public void UpdateDepartmentMapping()
        {
         


            CreateMap<UpdateDepartmentModelCommend, Department>()
                .ForMember(des => des.InstManger, src => src.MapFrom(src => src.ManagerId))
                        .ForMember(des => des.NameAr, src => src.Condition(src => src.NameAr!=null))
                                .ForMember(des => des.NameEn, src => src.Condition(src => src.NameEn != null))
                                    .ForMember(des => des.Capsity, src => src.Condition(src => src.Capsity > 0));
        }

    }
}
