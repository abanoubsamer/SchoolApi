using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Core.Features.User.Queries.Modles;
using SchoolWep.Core.Features.User.Queries.Responses;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Queries.Handler
{
    public class UserHandlerQueries : ResponseHandler
        , IRequestHandler<GetUserListModelQueries, Response<List<GetUserListResponseQueries>>>
        , IRequestHandler<GetUserByIdModelQueries, Response<GetUserByIdResponseQueries>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        private readonly IMapper _Mapper;
        private readonly IUserServices _userServices;
        #endregion




        #region Constructor
        public UserHandlerQueries(IUserServices userServices, IStringLocalizer<SharedResource> stringLocalizer,IMapper mapper) : base(stringLocalizer)
        {
            _userServices = userServices;
            _StringLocalizer = stringLocalizer;
            _Mapper = mapper;

        }
        #endregion



        #region Implimntation
        public async Task<Response<List<GetUserListResponseQueries>>> Handle(GetUserListModelQueries request, CancellationToken cancellationToken)
        {
            var Users = await _userServices.GetListUsers();
            if (Users == null) return NotFound<List<GetUserListResponseQueries>>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var UserMapping = _Mapper.Map<List<GetUserListResponseQueries>>(Users);
     
            return Success(UserMapping);
        }

        public async Task<Response<GetUserByIdResponseQueries>> Handle(GetUserByIdModelQueries request, CancellationToken cancellationToken)
        {
            var user = await _userServices.FindUserById(request.Id);
            if (user == null) return NotFound<GetUserByIdResponseQueries>(_StringLocalizer[SharedResourcesKey.Valdiation.NotFound]);
            var UserMapping = _Mapper.Map<GetUserByIdResponseQueries>(user);

            return Success(UserMapping);
        }
        #endregion



    }
}
