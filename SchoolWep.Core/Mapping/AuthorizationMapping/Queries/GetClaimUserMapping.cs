using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {

        public void GetClaimUserMapping()
        {

            CreateMap<Claim, Calims>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
        }
    }
}
