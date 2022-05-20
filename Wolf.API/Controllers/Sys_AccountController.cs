using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Wolf.API.Infrastructure.Authentication;
using Wolf.Core.Interfaces;
using Wolf.API.Service;
using Wolf.Core.Models;
using Wolf.Core.Constant;
using System.Security.Claims;
using Wolf.Core.ExtensionMethods;
using Wolf.Core.Core;
using Microsoft.Extensions.Configuration;
using Wolf.API.Infrastructure.Authorization;
using Wolf.API.Model;
using Newtonsoft.Json;

namespace Wolf.API.Controllers
{
    [ApiController]
    [AuthorizeFilter]
    [Route("api/[controller]")]
    public class Sys_AccountController : ControllerBase
    {        
        private readonly IUserProvider _userProvider;        
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_AccountController> _logger;
        public Sys_AccountController(IServiceWrapper service, IUserProvider userService, IJwtAuthManager jwtAuthManager, ILogger<Sys_AccountController> logger)
        {            
            _userProvider = userService;            
            _jwtAuthManager = jwtAuthManager;
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Login body: ({0})", JsonConvert.SerializeObject(request)));
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (!UserExtensions.IsValidUserLogin(request.UserName, request.Password, out string message))
                {
                    throw new Exception(message);                    
                }
                var result = await _service.Sys_User.CheckUserLogin(request.UserName, request.Password);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                    new Claim(ClaimTypes.Name, result.UserName),                    
                };

                var jwtResult = _jwtAuthManager.GenerateTokens(result.UserName, claims, DateTime.Now);                
                result.AccessToken = jwtResult.AccessToken;
                Sys_AuthToken authToken = new Sys_AuthToken();                
                authToken.AccessToken = jwtResult.AccessToken;
                authToken.RefeshToken = jwtResult.RefreshToken.TokenString;
                await _service.Sys_AuthToken.SaveByLoginNameAsync(result.UserName, authToken);                
                return ResponseMessage.Success(result, Sys_Const.Message.SERVICE_LOGIN_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Login : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        
        [HttpPost("Signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Signup body: ({0})", JsonConvert.SerializeObject(request)));
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (!UserExtensions.IsValidUserLogin(request.LoginName, request.PassWord, out string message))
                {
                    throw new Exception(message);
                }
                var user = await _service.Sys_User.CheckUserExisted(request.LoginName);
                if (user != null)
                {
                    throw new Exception(Sys_Const.Message.SERVICE_USERNAME_EXISTED);
                }
                Sys_User userNew = new Sys_User();
                ObjectExtensions.Mapping<SignupRequest, Sys_User>(request, userNew);
                userNew.PassWord = Cryption.EncryptByKey(userNew.PassWord, Sys_Const.Security.key);
                if (userNew.LoginName == "admin") { 
                    userNew.IsActive = true; 
                    userNew.IsSystem = true; 
                }        
                userNew = await _service.Sys_User.SaveEntityAsync(userNew);
                return ResponseMessage.Success(userNew, Sys_Const.Message.SERVICE_SIGNUP_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Signup : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] RegisterRequest request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call ChangePassword body: ({0})", JsonConvert.SerializeObject(request)));
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (!UserExtensions.IsValidUserChangePassword(request.UserName, request.PasswordOld, request.PasswordNew, out string message))
                {
                    return ResponseMessage.Error(null, message);
                }
                var user = await _service.Sys_User.CheckUserLogin(request.UserName, request.PasswordOld);
                await _service.Sys_User.UserChangePassword(user.UserId, request.PasswordNew);              
                return ResponseMessage.Success(Sys_Const.Message.SERVICE_CHANGEPASSWORD_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ChangePassword : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("Logout")]
        [AllowAnonymous]
        public IActionResult Logout(string userName)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Logout params: (userName = {0})", userName));
                _jwtAuthManager.RemoveRefreshTokenByUserName(userName);                
                return ResponseMessage.Success(Sys_Const.Message.SERVICE_LOGOUT_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Logout : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("Info")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                _logger.LogInformation("Call GetInfo");
                var userName = _userProvider.LoginName;                
                var userInfo = await _service.Sys_User.GetUserInfo(userName);
                return ResponseMessage.Success(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetInfo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                _logger.LogInformation(string.Format("Call RefreshToken body: ({0})", JsonConvert.SerializeObject(refreshTokenRequest)));
                refreshTokenRequest.RefreshToken = (await _service.Sys_AuthToken.GetByLogiNameAsync(refreshTokenRequest.UserName)).RefeshToken;
                var user = await _service.Sys_User.CheckUserRefreshToken(refreshTokenRequest.UserName);
                if (user == null)
                {
                    throw new Exception(Sys_Const.Message.SERVICE_REFRESH_ERROR);
                }
                var accessToken = refreshTokenRequest.AccessToken;
                var jwtResult = _jwtAuthManager.Refresh(refreshTokenRequest.RefreshToken, accessToken, DateTime.Now);
                Sys_AuthToken authToken = new Sys_AuthToken();
                authToken.AccessToken = jwtResult.AccessToken;
                authToken.RefeshToken = jwtResult.RefreshToken.TokenString;
                await _service.Sys_AuthToken.SaveByLoginNameAsync(refreshTokenRequest.UserName, authToken);
                RefreshTokenResponse refreshTokenResponse = new RefreshTokenResponse();
                refreshTokenResponse.AccessToken = authToken.AccessToken;
                return ResponseMessage.Success(refreshTokenResponse, Sys_Const.Message.SERVICE_REFRESH_SUCCESS);
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError(string.Format("RefreshToken : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message, null, 401);
            }
        }
    }
}
