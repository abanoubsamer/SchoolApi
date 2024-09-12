using AutoMapper;
using SchoolWep.Core.Features.User.Queries.Responses;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.UserMapping.Resolver
{
    public class RolesResolver : IValueResolver<ApplicationUser, GetUserListResponseQueries, List<string>>
    {
        private readonly IUserServices _userServices;

        public RolesResolver(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public List<string> Resolve(ApplicationUser source, GetUserListResponseQueries destination, List<string> destMember, ResolutionContext context)
        {
            return  _userServices.GetRolesUser(source);
        }

    }
}
