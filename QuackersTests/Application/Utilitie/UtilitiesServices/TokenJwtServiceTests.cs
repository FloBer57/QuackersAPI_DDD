using Moq;
using QuackersAPI_DDD.Application.Utilitie.UtilitiesServices;
using QuackersAPI_DDD.Domain.Model;
using System;
using NUnit.Framework;

namespace QuackersAPI_DDD.Tests
{
    [TestFixture]
    public class TokenJwtServiceTests
    {
        private readonly string _secretKey = "superSecretKeyThatIsAtLeast32CharactersLong!";
        private readonly string _issuer = "TestIssuer";
        private readonly string _audience = "TestAudience";

        private TokenJwtService CreateService()
        {
            return new TokenJwtService(_secretKey, _issuer, _audience);
        }

        [Test]
        public void GenerateToken_WithValidUser_ReturnsToken()
        {
            // Arrange
            var service = CreateService();
            var user = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                PersonRole = new PersonRole { PersonRole_Name = "Admin", PersonRole_Id = 1 }
            };

            // Act
            var token = service.GenerateToken(user);

            // Assert
            Assert.IsNotNull(token);
        }

        [Test]
        public void GenerateToken_UserIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var service = CreateService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.GenerateToken(null));
        }

        [Test]
        public void GenerateToken_SecretKeyIsEmpty_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = new TokenJwtService("", _issuer, _audience);
            var user = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                PersonRole = new PersonRole { PersonRole_Name = "Admin" }
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.GenerateToken(user));
        }

        [Test]
        public void GenerateToken_UserRoleIsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = CreateService();
            var user = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                PersonRole = null
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.GenerateToken(user));
        }

        [Test]
        public void GenerateToken_IssuerIsEmpty_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = new TokenJwtService(_secretKey, "", _audience);
            var user = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                PersonRole = new PersonRole { PersonRole_Name = "Admin" }
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.GenerateToken(user));
        }

        [Test]
        public void GenerateToken_AudienceIsEmpty_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = new TokenJwtService(_secretKey, _issuer, "");
            var user = new Person
            {
                Person_Id = 1,
                Person_Email = "test@example.com",
                PersonRole = new PersonRole { PersonRole_Name = "Admin" }
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.GenerateToken(user));
        }
    }
}
