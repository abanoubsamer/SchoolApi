using SchoolWep.Core.Features.User.Queries.Responses;
using SchoolWep.Core.Mapping.UserMapping.Resolver;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        

        public void GetListUserMapping()
        {
            CreateMap<ApplicationUser, GetUserListResponseQueries>()
              
                .ForMember(dest => dest.Roles, opt => opt.MapFrom<RolesResolver>())
                .ForMember(des=>des.FullName , src=>src.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(des=>des.DateCreated , src=>src.MapFrom(src => src.AccountCreatedDate));
        }
    }
}
