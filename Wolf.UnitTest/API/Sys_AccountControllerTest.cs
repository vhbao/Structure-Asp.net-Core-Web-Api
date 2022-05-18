using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.API.Service;
using Wolf.API.Controllers;

using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using Xunit;
using Wolf.API.Infrastructure.Authentication;
using Microsoft.Extensions.Logging;

namespace Wolf.UnitTest.API
{
    public class Sys_AccountControllerTest
    {
        private readonly Sys_AccountController _sys_AccountController;
        private readonly IUserProvider _userProvider;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IServiceWrapper _repository;
        private readonly ILogger<Sys_AccountControllerTest> _logger;
        public Sys_AccountControllerTest()
        {            
            
        }
        [Theory]
        [InlineData("Admin", "1")]
        [InlineData("Admin", "2")]
        public void Login(string userName, string password)
        {
            //Arrange
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.UserName = userName;
            loginRequest.Password = password;
            //Act
            var result =  _sys_AccountController.Login(loginRequest).Result;
            //Assert
            Assert.IsType<ResponseResult>(result);
        }
    }
}
