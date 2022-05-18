using Microsoft.AspNetCore.Http;
using Wolf.Core.Constant;
using Wolf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Core
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserProvider(IHttpContextAccessor context)
        {
            _context = context;
        }

        public bool IsAuthenticated
        {
            get
            {
                bool isAuthenticated = false;
                try
                {
                    isAuthenticated = _context.HttpContext.User.Identity.IsAuthenticated;
                }
                catch { }
                return isAuthenticated;
            }
        }

        public Guid Id
        {
            get
            {
                Guid userId = Guid.Empty;
                try
                {
                    userId = Guid.Parse(_context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                }
                catch { }
                return userId;
            }
        }
        public string LoginName
        {
            get
            {
                string userName = string.Empty;
                try
                {
                    userName = _context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                }
                catch { }
                return userName;
            }
        }

        public bool IsValidUserLogin(string userName, string password, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(userName))
            {
                message = Sys_Const.Message.SERVICE_LOGIN_USERNAME_EMPTY;
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                message = Sys_Const.Message.SERVICE_LOGIN_PASSWORD_EMPTY;
                return false;
            }
            return true;
        }
        public bool IsValidUserChangePassword(string userName, string passwordOld, string passwordNew, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(userName))
            {
                message = Sys_Const.Message.SERVICE_LOGIN_USERNAME_EMPTY;
                return false;
            }
            if (string.IsNullOrEmpty(passwordOld))
            {
                message = Sys_Const.Message.SERVICE_LOGIN_PASSWORDOld_EMPTY;
                return false;
            }

            if (string.IsNullOrWhiteSpace(passwordNew))
            {
                message = Sys_Const.Message.SERVICE_LOGIN_PASSWORDNEW_EMPTY;
                return false;
            }

            if (passwordOld != passwordNew)
            {
                message = Sys_Const.Message.SERVICE_LOGIN_PASSNEW_PASSOld_DIFFERENT;
                return false;
            }
            return true;
        }
    }
}
