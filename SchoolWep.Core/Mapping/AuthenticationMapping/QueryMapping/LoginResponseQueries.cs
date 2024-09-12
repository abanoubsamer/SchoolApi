using SchoolWep.Core.Features.Authentication.Queries.Responses;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthenticationMapping
{
    public partial class AuthenticationProfile
    {

        public void LoginResponseQueriesMapping()
        {
            CreateMap<AuthModelResult, LoginResponseQueries>()
                .ForMember(des => des.UserID, src => src.MapFrom(src => src.UserId))
                .ForMember(des => des.Username, src => src.MapFrom(src => src.UserName))
                  .ForMember(des => des.Token, src => src.MapFrom(src => src.Token))
                    .ForMember(des => des.Expiration, src => src.MapFrom(src => src.Expiration))
                      .ForMember(des => des.Roles, src => src.MapFrom(src => src.Roles));
        }

    }
}
