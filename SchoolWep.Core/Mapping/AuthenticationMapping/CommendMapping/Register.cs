using SchoolWep.Core.Features.Authentication.Commend.Models;
using SchoolWep.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthenticationMapping
{
     public partial class AuthenticationProfile
    {

        public  void RegisterMapping()
        {
            CreateMap<RegisterModelCommend, ApplicationUser>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(des => des.AccountCreatedDate, opt => opt.MapFrom(src => DateTime.Now));
        }

    }
}
