using AutoMapper;
using ChatServer.Controllers;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Dto;
using Services.Exceptions;
using Services.Models;
using Services.Services;
using System.Threading.Tasks;
using Xunit;

namespace ControllersTests
{
    public class AccountControllerTest
    {
        private AccessToken Token { get => new AccessToken { Token = "Token" }; }

        [Fact]
        public async Task LoginAsyncReturnsOk()
        {
            var loginModel = new LoginViewModel
            {
                UserName = "UserName",
                Password = "Password"
            };

            var mapperMock = new Mock<IMapper>() { CallBase = true };
            var authenticationServiceMock = new Mock<IAuthenticationService>();

            authenticationServiceMock.Setup(x => x.AuthenticateAsync(loginModel.UserName, loginModel.Password)).Returns(Task.FromResult(Token));

            var controller = new AccountController(authenticationServiceMock.Object, mapperMock.Object);

            var result = await controller.LoginAsync(loginModel);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RegisterAsyncReturnsOk()
        {
            var registerModel = new RegisterViewModel
            {
                UserName = "UserName",
                Password = "Password"
            };

            var registerDto = new RegisterDto
            {
                UserName = "UserName",
                Password = "Password"
            };

            var mapperMock = new Mock<IMapper>() { CallBase = true };
            var authenticationServiceMock = new Mock<IAuthenticationService>();

            authenticationServiceMock.Setup(x => x.RegisterAsync(registerDto)).Returns(Task.FromResult(Token));

            var controller = new AccountController(authenticationServiceMock.Object, mapperMock.Object);

            var result = await controller.RegisterAsync(registerModel);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
