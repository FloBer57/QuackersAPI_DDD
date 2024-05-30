using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.Controller;
using QuackersAPI_DDD.API.DTO.AuthenticationDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Tests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private Mock<IEmailService> _mockEmailService;
        private Mock<IPersonService> _mockPersonService;
        private Mock<ISecurityService> _mockSecurityService;
        private Mock<ITokenJwtService> _mockTokenService;
        private Mock<IRefreshTokenService> _mockRefreshTokenService;
        private AuthenticationController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockEmailService = new Mock<IEmailService>();
            _mockPersonService = new Mock<IPersonService>();
            _mockSecurityService = new Mock<ISecurityService>();
            _mockTokenService = new Mock<ITokenJwtService>();
            _mockRefreshTokenService = new Mock<IRefreshTokenService>();

            _controller = new AuthenticationController(
                _mockPersonService.Object,
                _mockSecurityService.Object,
                _mockTokenService.Object,
                _mockRefreshTokenService.Object,
                _mockEmailService.Object
            );
        }

        [Test]
        public async Task Login_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginDto = new LoginDTO { Email = "test@example.com", Password = "Password123" };
            var person = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                Person_Password = "hashedPassword",
                PersonRole = new PersonRole { PersonRole_Name = "Admin" }
            };

            _mockPersonService.Setup(s => s.GetPersonByEmail(loginDto.Email)).ReturnsAsync(person);
            _mockSecurityService.Setup(s => s.VerifyPassword(loginDto.Password, person.Person_Password)).Returns(true);
            _mockTokenService.Setup(s => s.GenerateToken(person)).Returns("validJwtToken");
            _mockRefreshTokenService.Setup(s => s.GenerateRefreshToken(person)).ReturnsAsync("validRefreshToken");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value as IDictionary<string, object>;
            Assert.IsNotNull(data);
            Assert.AreEqual("validJwtToken", data["Token"]);
            Assert.AreEqual("validRefreshToken", data["RefreshToken"]);
            Assert.AreEqual("Login successful.", data["Message"]);
        }

        [Test]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var loginDto = new LoginDTO { Email = "test@example.com", Password = "Password123" };
            _mockPersonService.Setup(s => s.GetPersonByEmail(loginDto.Email)).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
            Assert.AreEqual("Invalid credentials.", unauthorizedResult.Value);
        }

        [Test]
        public async Task ResetPasswordRequest_WithValidEmail_ReturnsOkResult()
        {
            // Arrange
            var requestDto = new ResetPasswordRequestDTO { Email = "test@example.com" };
            var person = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com"
            };

            _mockPersonService.Setup(s => s.GetPersonByEmail(requestDto.Email)).ReturnsAsync(person);
            _mockSecurityService.Setup(s => s.GeneratePasswordResetToken(person)).ReturnsAsync("resetToken");
            _mockEmailService.Setup(s => s.SendPasswordResetEmail(person.Person_Email, "resetToken")).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ResetPasswordRequest(requestDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value as IDictionary<string, object>;
            Assert.IsNotNull(data);
            Assert.AreEqual("Reset password link has been sent to your email.", data["Message"]);
            Assert.AreEqual("resetToken", data["ResetToken"]);
        }

        [Test]
        public async Task ResetPasswordRequest_WithInvalidEmail_ReturnsNotFoundResult()
        {
            // Arrange
            var requestDto = new ResetPasswordRequestDTO { Email = "test@example.com" };
            _mockPersonService.Setup(s => s.GetPersonByEmail(requestDto.Email)).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.ResetPasswordRequest(requestDto);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("User not found.", notFoundResult.Value);
        }

        [Test]
        public async Task RefreshToken_WithValidToken_ReturnsNewToken()
        {
            // Arrange
            var refreshTokenDto = new RefreshTokenDTO { RefreshToken = "validRefreshToken" };
            var person = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                PersonRole = new PersonRole { PersonRole_Name = "Admin" }
            };

            _mockRefreshTokenService.Setup(s => s.ValidateRefreshToken(refreshTokenDto.RefreshToken)).ReturnsAsync(person.Person_Id);
            _mockPersonService.Setup(s => s.GetPersonById(person.Person_Id)).ReturnsAsync(person);
            _mockTokenService.Setup(s => s.GenerateToken(person)).Returns("newJwtToken");
            _mockRefreshTokenService.Setup(s => s.GenerateRefreshToken(person)).ReturnsAsync("newRefreshToken");

            // Act
            var result = await _controller.RefreshToken(refreshTokenDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value as IDictionary<string, object>;
            Assert.IsNotNull(data);
            Assert.AreEqual("newJwtToken", data["Token"]);
            Assert.AreEqual("newRefreshToken", data["RefreshToken"]);
        }

        [Test]
        public async Task RevokeRefreshToken_WithValidToken_ReturnsSuccess()
        {
            // Arrange
            var revokeTokenDto = new RevokeTokenDTO { RefreshToken = "validRefreshToken" };

            _mockRefreshTokenService.Setup(s => s.RevokeRefreshToken(revokeTokenDto.RefreshToken)).ReturnsAsync(true);

            // Act
            var result = await _controller.RevokeRefreshToken(revokeTokenDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value as IDictionary<string, object>;
            Assert.IsNotNull(data);
            Assert.IsTrue((bool)data["Success"]);
            Assert.AreEqual("Token has been successfully revoked.", data["Message"]);
        }
    }
}
