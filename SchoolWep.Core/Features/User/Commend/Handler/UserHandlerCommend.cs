using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.User.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Handler
{
    public class UserHandlerCommend : ResponseHandler
        , IRequestHandler<UpdateUserModelCommend, Response<string>>
        , IRequestHandler<DeleteUserModelCommend, Response<string>>
        , IRequestHandler<ChangePasswordUserModelCommend, Response<string>>
        , IRequestHandler<SendOtpResetPasswordUserModelCommend, Response<string>>
        , IRequestHandler<ResetPasswordUserModelCommend, Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUserServices _UserServices;
        private readonly IMapper _Mapper;
        #endregion




        #region Constructor
        public UserHandlerCommend(
            IUserServices userServices,
             IMapper mapper,
            IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
             _Mapper= mapper;
            _stringLocalizer= stringLocalizer;
            _UserServices= userServices;

        }

        #endregion


        #region Implemntation
        public async Task<Response<string>> Handle(UpdateUserModelCommend request, CancellationToken cancellationToken)
        {
            //cheack userexist
            var user = await _UserServices.FindUserById(request.Id);
          
            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            //mapping
            _Mapper.Map(request, user);

            var result = await _UserServices.UpdateUserAsync(user);
            
            if (!result.Successed) return BadRequest<string>(result.Msg);

            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserModelCommend request, CancellationToken cancellationToken)
        {

            var user = await _UserServices.FindUserById(request.Id);

            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var result= await _UserServices.DeleteUserAsync(user);

            if (!result.Successed) return BadRequest<string>(result.Msg);

            return Deleted<string>(_stringLocalizer[SharedResourcesKey.Operations.Deleted]);

        }

        public async Task<Response<string>> Handle(ChangePasswordUserModelCommend request, CancellationToken cancellationToken)
        {
            //CheckID
            var user = await _UserServices.FindUserById(request.Id);
            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);
            var result = await _UserServices.ChangePasswordAsync(user, request.NewPassword, request.OldPassword);
            if (!result.Successed) return BadRequest<string>(result.Msg);
            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Updated]);

        }

        public async Task<Response<string>> Handle(SendOtpResetPasswordUserModelCommend request, CancellationToken cancellationToken)
        {
            var result = await _UserServices.SendOtpResetPassword(request.Email);
            if (!result.Successed) return BadRequest<string>(result.Msg);

            return Success("Success Send Otp To Email");

        }

        public async Task<Response<string>> Handle(ResetPasswordUserModelCommend request, CancellationToken cancellationToken)
        {
            var user = await _UserServices.FindUserById(request.UserID);

            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKey.Valdiation.NotFound]);

            var Result = await _UserServices.ChangePasswordAsync(user, request.NewPasswoed);
            if (!Result.Successed) return BadRequest<string>(Result.Msg);

            return Success<string>(_stringLocalizer[SharedResourcesKey.Operations.Updated]);

        }
        #endregion



    }
}
