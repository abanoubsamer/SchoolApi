using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Queries.Models;
using SchoolWep.Core.Features.Authentication.Queries.Responses;
using SchoolWep.Core.SharedResources;
using SchoolWep.Data.Models;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.AuthenticationServices;
using SchoolWep.Services.SendEmailServices;
using SchoolWep.Services.ServicesResult;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Queries.Handler
{
    public class AuthenticationHandlerQueries : ResponseHandler
        , IRequestHandler<LoginModelQueries, Response<LoginResponseQueries>>
        , IRequestHandler<GetTokenModelQueries, Response<LoginResponseQueries>>
    {


        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthenticationServices _authorizationService;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISendEmailServices _sendEmailServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion


        #region Constructor
        public AuthenticationHandlerQueries(
            SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<SharedResource> stringLocalizer,
           IAuthenticationServices authenticationServices,
           IMapper mapper,
           IUserServices userServices,
           ISendEmailServices sendEmailServices, IHttpContextAccessor httpContextAccessor) : base(stringLocalizer)
        {
            _signInManager = signInManager;
            _authorizationService = authenticationServices;
            _mapper = mapper;
            _userServices = userServices;
            _sendEmailServices = sendEmailServices;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
        }


        #endregion


        #region Function
        public async Task<Response<LoginResponseQueries>> Handle(LoginModelQueries request, CancellationToken cancellationToken)
        {

            var resultCheck = await _userServices.CheckEmail(request.Email, request.Password);

            if (!resultCheck.Successed) return BadRequest<LoginResponseQueries>(resultCheck.Msg);

            if (!resultCheck.User.EmailConfirmed) return Unauthorized<LoginResponseQueries>("Email Not Confirmed");

            var AuthModel = await _authorizationService.GetTokenAsync(resultCheck.User);

            if (!AuthModel.IsAuthenticated) return Unauthorized<LoginResponseQueries>(AuthModel.Messgage);

           await _signInManager.SignInAsync(resultCheck.User, false);

            SetRefreshTokenInCookies(AuthModel.RefreshToken, AuthModel.RefreshTokenExpiration);

            var AuthMapping = _mapper.Map<LoginResponseQueries>(AuthModel);

            return Success(AuthMapping);


        }

        public async Task<Response<LoginResponseQueries>> Handle(GetTokenModelQueries request, CancellationToken cancellationToken)
        {

            var AuthModel = await _authorizationService.RefreshToken(request.RefreshToken,request.AccessToken);

            if (!AuthModel.IsAuthenticated)return Unauthorized<LoginResponseQueries>(AuthModel.Messgage);

            var AuthMapping = _mapper.Map<LoginResponseQueries>(AuthModel);

            return Success(AuthMapping);
        }

        #endregion
        private void SetRefreshTokenInCookies(string RefreshToken, DateTime Expires)
        {
            var CookiesOtion = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = Expires.ToLocalTime(),
                SameSite = SameSiteMode.Strict
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", RefreshToken, CookiesOtion);

        }
    }
}
