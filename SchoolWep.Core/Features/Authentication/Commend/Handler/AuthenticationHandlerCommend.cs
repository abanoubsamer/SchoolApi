using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Commend.Models;
using SchoolWep.Core.Features.Authentication.Commend.Responses;
using SchoolWep.Core.Features.Authentication.Queries.Responses;
using SchoolWep.Core.SharedResources;
using SchoolWep.Data.AppMetaData;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.AuthenticationServices;
using SchoolWep.Services.SendEmailServices;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Commend.Handler
{
    public class AuthenticationHandlerCommend : ResponseHandler
        , IRequestHandler<RegisterModelCommend, Response<string>>
        , IRequestHandler<ConfirmEmailModelCommden, Response<string>>
        , IRequestHandler<ConfirmOtoToResetPassModelCommden, Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthenticationServices _authorizationService;
        private readonly IMapper _mapper;
        private readonly ISendEmailServices _sendEmailServices;
        private readonly IUserServices _userServices;
        #endregion


        #region Constructor
        public AuthenticationHandlerCommend(
            IUserServices userServices,
            IStringLocalizer<SharedResource> stringLocalizer,
           IAuthenticationServices authenticationServices,
           IMapper mapper,
           ISendEmailServices sendEmailServices) : base(stringLocalizer)
        {
            _userServices = userServices;
            _authorizationService=authenticationServices;
            _mapper = mapper;
            _sendEmailServices = sendEmailServices;
            _stringLocalizer = stringLocalizer;
        }
        #endregion


        #region Implemntation
        public async Task<Response<string>> Handle(RegisterModelCommend request, CancellationToken cancellationToken)
        {
            if(await _userServices.EmailIsExsitAsync(request.Email))
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKey.Valdiation.Email_Is_Already_Exist]);
            }
            if(await _userServices.UserNameIsExsitAsync(request.UserName))
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKey.Valdiation.UserName_Is_Already_Exist]);
            }
            var UserMapping = _mapper.Map<ApplicationUser>(request);

            var result = await _authorizationService.Register(UserMapping, request.Password);
            
            if (!result.Successed) return BadRequest<string>(result.Msg);

            var Token = await _authorizationService.GenerateJWT(UserMapping);
            var AccessToken = new JwtSecurityTokenHandler().WriteToken(Token);


            var baseUrl = "https://localhost:7099"; // عوض عن هذا بالعنوان الأساسي لـ API بتاعك
            

            var confirmationLink = $"{baseUrl}/{Router.AuthenticationRouting.ConfirmEmail}?token={AccessToken}";

            if (!await _sendEmailServices.SendConfirmEmail(UserMapping.Email, confirmationLink))
                return BadRequest<string>("Error In Email Sender");
           
            
            return Success("Succsed Register");



        }

        public async Task<Response<string>> Handle(ConfirmEmailModelCommden request, CancellationToken cancellationToken)
        {
            var claims =  _authorizationService.ValidateJwtToken(request.token);
            if (claims == null)
            {
                return BadRequest<string>("Invalid token");
            }

            var userId = claims.Claims.FirstOrDefault(e=> e.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest<string>("Invalid user");
            }

            var user = await _userServices.FindUserById(userId);
            if (user == null)
            {
                return NotFound<string>("User not found");
            }

            user.EmailConfirmed = true;
            var rsultUpdate= await _userServices.UpdateUserAsync(user);
            if (!rsultUpdate.Successed) return BadRequest<string>(rsultUpdate.Msg);

            return Success("Email confirmed successfully");
        }

        public async Task<Response<string>> Handle(ConfirmOtoToResetPassModelCommden request, CancellationToken cancellationToken)
        {
            var result = await _userServices.ConfirmOtpPassword(request.Email, request.Otp);
            if (!result.Item2) return BadRequest<string>(result.Item1);
            return Success(result.Item1);
        }
        #endregion






    }
}
