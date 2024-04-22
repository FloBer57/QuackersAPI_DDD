using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.AuthenticationDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.API.Controller;

namespace QuackersAPI_DDD.Tests.API.Controller
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly Mock<ISecurityService> _mockSecurityService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockPersonService = new Mock<IPersonService>();
            _mockSecurityService = new Mock<ISecurityService>();
            _mockTokenService = new Mock<ITokenService>();
            _controller = new AuthenticationController(_mockPersonService.Object, _mockSecurityService.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task Login_WithEmptyCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "", Password = "" };

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "user@example.com", Password = "wrongpassword" };
            _mockPersonService.Setup(x => x.GetPersonByEmail("user@example.com")).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Login_WithCorrectCredentials_ButWrongPassword_ReturnsUnauthorized()
        {
            // Arrange
            var person = new Person { Person_Id = 1, Person_Email = "user@example.com", Person_Password = "hashedpassword" };
            var loginDTO = new LoginDTO { Email = "user@example.com", Password = "wrongpassword" };
            _mockPersonService.Setup(x => x.GetPersonByEmail("user@example.com")).ReturnsAsync(person);
            _mockSecurityService.Setup(x => x.VerifyPassword("wrongpassword", "hashedpassword")).Returns(false);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Login_WithCorrectCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var person = new Person { Person_Id = 1, Person_Email = "user@example.com", Person_Password = "hashedpassword" };
            var loginDTO = new LoginDTO { Email = "user@example.com", Password = "correctpassword" };
            _mockPersonService.Setup(x => x.GetPersonByEmail("user@example.com")).ReturnsAsync(person);
            _mockSecurityService.Setup(x => x.VerifyPassword("correctpassword", "hashedpassword")).Returns(true);
            _mockTokenService.Setup(x => x.GenerateToken(person)).Returns("GeneratedToken");

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }


    }
}
