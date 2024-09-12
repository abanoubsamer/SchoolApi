using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Data.Models;
using SchoolWep.Data.OptionsConfiguration;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Infrustructure.UnitOfWork;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolWep.Services.AuthenticationServices
{
    public class AuthenticationServices : IAuthenticationServices
    {

        #region Fields
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILogger<AuthenticationServices> _logger;
        private readonly IOptions<JwtOptions> _jwtOptions;
        //private readonly RoleManager<IdentityRole> _RoleManager;
        #endregion

        #region Constructor
        public AuthenticationServices(
             RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> UserManager
           , IOptions<JwtOptions> jwtOptions
           , IUnitOfWork unitOfWork
           , ILogger<AuthenticationServices> logger)
        {
            _roleManager= roleManager;
            _UnitOfWork = unitOfWork;
            _logger = logger;
            _UserManager = UserManager;
            _jwtOptions = jwtOptions;
        }
        #endregion


        #region Implmentation

  



        #region AuthFunction
        public async Task<DbServicesResult> Register(ApplicationUser user, string Password)
        {
            if (user == null || Password.IsNullOrEmpty()) return
                    new DbServicesResult
                    {
                        Error = true,
                        Msg = "Email Or Password Invalid"
                    };

            try
            {
                var Transcation = await _UnitOfWork.BeginTransactionAsync();
                var result = await _UserManager.CreateAsync(user, Password);
                if (!result.Succeeded)
                {
                    var Errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        Errors += $"{error.Description} :";
                    }
                    return new DbServicesResult()
                    {
                        Error = true,
                        Msg = Errors
                    };
                }
                var resultRole = await _UserManager.AddToRoleAsync(user, "User");
                if (!resultRole.Succeeded)
                {
                    var Errors = string.Empty;
                    foreach (var Error in resultRole.Errors)
                    {
                        Errors += $"{Error.Description}:";
                    }
                    return new DbServicesResult()
                    {
                        Error = true,
                        Msg = Errors
                    };
                }
                await _UnitOfWork.CommentAsync();
                return new DbServicesResult
                {
                    Successed = true
                };
            }
            catch (Exception ex)
            {
                await _UnitOfWork.RollBackAsync();
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Error Exception {ex.Message}"

                };
            }
        }


        /// <summary>
        /// Login Function
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<AuthModelResult> GetTokenAsync(ApplicationUser user)
        {
           
            var authModel = new AuthModelResult();

            if (user == null) return authModel;

            var Token = await GenerateJWT(user);
            var Roles = await _UserManager.GetRolesAsync(user);


            authModel.UserId = user.Id;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Expiration = Token.ValidTo;
            authModel.Roles = Roles.ToList();
            authModel.IsAuthenticated = true;

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var ActiveRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = ActiveRefreshToken.Token;
                authModel.RefreshTokenExpiration = ActiveRefreshToken.ExpirsOn;
            }
            else
            {
                var NewRefrshToken = await GenerateRefreshToken();
                NewRefrshToken.JwtId = Token.Id;
                NewRefrshToken.AccessToken = authModel.Token;
                authModel.RefreshToken = NewRefrshToken.Token;
                authModel.RefreshTokenExpiration = NewRefrshToken.ExpirsOn;
                user.RefreshTokens.Add(NewRefrshToken);
                try
                {
                    await _UserManager.UpdateAsync(user);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }

            return authModel;
        }

        private async Task<RefreshToken> GenerateRefreshToken()
        {
            var RendemNumber = new byte[32];
            using var Generate = new RNGCryptoServiceProvider();
            Generate.GetBytes(RendemNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RendemNumber),
                CreateOn = DateTime.UtcNow,
                ExpirsOn = DateTime.UtcNow.AddDays(5)

            };
        }

        public async Task<SecurityToken> GenerateJWT(ApplicationUser user)
        {
            // 1. جمع المطالبات الخاصة بالمستخدم
            var claims = await GetUserClaimsAsync(user);

            // 2. إضافة المطالبات المتعلقة بالأدوار
            var roleClaims = await GetRoleClaimsAsync(user);

            claims.AddRange(roleClaims);

            // 3. إنشاء الرمز JWT باستخدام المطالبات
            var token = CreateJwtToken(claims);

            return token;

        }

      
        private async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            var userClaims = await _UserManager.GetClaimsAsync(user);
            var jwtId = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(JwtRegisteredClaimNames.Jti, jwtId)
             };

            claims.AddRange(userClaims);
            return claims;
        }
        private async Task<List<Claim>> GetRoleClaimsAsync(ApplicationUser user)
        {
            var roles = await _UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var roleName in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roleName));

                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var claimsForRole = await _roleManager.GetClaimsAsync(role);
                    roleClaims.AddRange(claimsForRole);
                }
            }

            return roleClaims;
        }
        private SecurityToken CreateJwtToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Value.Audience,
                Issuer = _jwtOptions.Value.Issuer,
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.Value.LiveTimeDay),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }
        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = _jwtOptions.Value.Issuer,
                        ValidAudience = _jwtOptions.Value.Audience
                    }, out SecurityToken validatedToken);

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                // Handle expired token exception
                _logger.LogWarning("Token has expired.");
                return null;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                // Handle invalid signature exception
                _logger.LogWarning("Token has an invalid signature.");
                return null;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                _logger.LogError(ex, "Error validating token.");
                return null;
            }
        }


        public async Task<AuthModelResult> RefreshToken(string RefreshToken,string AccessToken)
        {
            var AuthModel = new AuthModelResult();

            var Clims =  ValidateJwtWithOutExpirationToken(AccessToken);
           
            if(Clims == null)
            {
                AuthModel.Messgage = "Invalid Token";
                return AuthModel;
            }
              
            var user = await _UserManager.Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Token == RefreshToken));
            
            if (user == null)
            {
                AuthModel.Messgage = "Invalid Token";
                return AuthModel;
            }

            // get ResfreshToken
            var StoreRefreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == RefreshToken);
            
            var TokenUserID = Clims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (StoreRefreshToken.ApplicationUserId != TokenUserID.Value)
            {
                AuthModel.Messgage = "Invalid token";
                return AuthModel;
            }

            if (StoreRefreshToken == null || !StoreRefreshToken.IsActive )
            {
                AuthModel.Messgage = "Invalid or expired refresh token";
                return AuthModel;
            }
            
            //Genertate New Jwt
            var Roles = await _UserManager.GetRolesAsync(user);

            var Token = await GenerateJWT(user);
   
            AuthModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            AuthModel.Expiration =Token.ValidTo;
            AuthModel.RefreshToken = StoreRefreshToken.Token;
            AuthModel.RefreshTokenExpiration = StoreRefreshToken.ExpirsOn;
            AuthModel.IsAuthenticated = true;
            AuthModel.UserId = user.Id;
            AuthModel.Email = user.Email;
            AuthModel.UserName = user.UserName;
            AuthModel.Roles = Roles.ToList();
            return AuthModel;

        }


        private JwtSecurityToken ReadAccessToken(string AccessToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(AccessToken); 
        }

        private ClaimsPrincipal ValidateJwtWithOutExpirationToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _jwtOptions.Value.Issuer,
                        ValidAudience = _jwtOptions.Value.Audience
                    }, out SecurityToken validatedToken);

                return principal;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                // Handle invalid signature exception
                _logger.LogWarning("Token has an invalid signature.");
                return null;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                _logger.LogError(ex, "Error validating token.");
                return null;
            }
        }

        public async Task<List<IdentityRole>> GetRolesUserByNameAsync(List<string> Roles)
        {
          
            var UserRole = new List<IdentityRole>();

            foreach (var role in Roles)
            {
                var Tep = await _roleManager.FindByNameAsync(role);
                UserRole.Add(Tep);
            }

            return UserRole;
        }

        #endregion



        #endregion



    }
}
