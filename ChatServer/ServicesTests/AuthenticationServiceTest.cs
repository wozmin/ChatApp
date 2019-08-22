using ChatServer.Models;
using ChatServer.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Services;
using Services.Exceptions;
using Services.Factory;
using ServicesTests.Fakes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class AuthenticationServiceTest
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private string UserId { get; set; }
        private string Token { get; set; }

        private ApplicationUser User;
        private List<ApplicationUser> Users;

        public AuthenticationServiceTest()
        {
            UserName = "User Name";
            Password = "Password";
            UserId = "userID";
            Token = "token";
            User = new ApplicationUser { UserName = UserName, Id = UserId };
            Users = new List<ApplicationUser> { User };

        }
        
        [Fact]
        public async Task AuthenticateReturnsToken()
        {
            var singInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var jwtTokenFactoryMock = new Mock<IJwtTokenFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            singInManagerMock.Setup(x => x.PasswordSignInAsync(UserName, Password, false, false)).Returns(Task.FromResult(SignInResult.Success));
            userManagerMock.Setup(x => x.Users).Returns(Users.AsQueryable());
            jwtTokenFactoryMock.Setup(x => x.GenerateJwt(UserName, User)).Returns(Token);

            var authenticationService = new AuthenticationService(singInManagerMock.Object, userManagerMock.Object, unitOfWorkMock.Object, jwtTokenFactoryMock.Object);
            var accessToken = await authenticationService.AuthenticateAsync(UserName, Password);

            Assert.Equal(accessToken.UserId, UserId);
            Assert.Equal(accessToken.UserName, UserName);
            Assert.Equal(accessToken.Token, Token);
        }

        [Fact]
        public void AuthenticateThrowsValidationException()
        { 
            var singInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var jwtTokenFactoryMock = new Mock<IJwtTokenFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            singInManagerMock.Setup(x => x.PasswordSignInAsync(UserName, Password, false, false)).Returns(Task.FromResult(SignInResult.Failed));
            userManagerMock.Setup(x => x.Users).Returns(Users.AsQueryable());
            jwtTokenFactoryMock.Setup(x => x.GenerateJwt(UserName, User)).Returns(Token);

            var authenticationService = new AuthenticationService(singInManagerMock.Object, userManagerMock.Object, unitOfWorkMock.Object, jwtTokenFactoryMock.Object);

            Assert.ThrowsAsync<ValidationException>(async () => await authenticationService.AuthenticateAsync(UserName, Password));
        }
    }
}
