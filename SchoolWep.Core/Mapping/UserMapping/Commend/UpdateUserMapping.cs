using SchoolWep.Core.Features.User.Commend.Models;
using SchoolWep.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {

        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserModelCommend, ApplicationUser>()
                .ForMember(des=>des.NormalizedUserName,src=>src.MapFrom(src=>src.UserName.ToUpper()));
        }

    }
}
