using Moq;
using NUnit.Framework;
using QuackersAPI_DDD.API.Controller;
using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.API.DTO.MessageDTO;
using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuackersTests.API.Controller
{
    [TestFixture]
    public class ChannelControllerTests
    {
        private Mock<IChannelService> _mockChannelService;
        private Mock<IChannelPersonRoleXPersonXChannelService> _mockChannelPersonRoleXPersonXChannelService;
        private Mock<IPersonService> _mockPersonService;
        private ChannelController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockChannelService = new Mock<IChannelService>();
            _mockChannelPersonRoleXPersonXChannelService = new Mock<IChannelPersonRoleXPersonXChannelService>();
            _mockPersonService = new Mock<IPersonService>();

            _controller = new ChannelController(
                _mockChannelService.Object,
                _mockChannelPersonRoleXPersonXChannelService.Object,
                _mockPersonService.Object);
        }

        [Test]
        public async Task GetAllChannels_ShouldReturnOkResult_WithListOfChannels()
        {
            // Arrange
            var mockChannels = new List<Channel>
            {
                new Channel { Channel_Name = "Test Channel 1", ChannelType_Id = 1, Channel_ImagePath = "Default/Path" },
                new Channel { Channel_Name = "Test Channel 2", ChannelType_Id = 2, Channel_ImagePath = "Default/Path" }
            };
            _mockChannelService.Setup(service => service.GetAllChannels())
                .ReturnsAsync(mockChannels);

            // Act
            var result = await _controller.GetAllChannels();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Channel>>(okResult.Value);
            var channels = okResult.Value as List<Channel>;
            Assert.AreEqual(2, channels.Count);
        }

        [Test]
        public async Task GetChannelById_ShouldReturnOkResult_WhenChannelExists()
        {
            // Arrange
            int channelId = 1;
            var mockChannel = new Channel { Channel_Name = "Test Channel", ChannelType_Id = 1, Channel_ImagePath = "Default/Path" };
            _mockChannelService.Setup(service => service.GetChannelById(channelId))
                .ReturnsAsync(mockChannel);

            // Act
            var result = await _controller.GetChannelById(channelId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<Channel>(okResult.Value);
            var channel = okResult.Value as Channel;
            Assert.AreEqual("Test Channel", channel.Channel_Name);
        }

        [Test]
        public async Task GetChannelById_ShouldReturnNotFound_WhenChannelDoesNotExist()
        {
            // Arrange
            int channelId = 1;
            _mockChannelService.Setup(service => service.GetChannelById(channelId))
                .ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetChannelById(channelId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetChannelsByChannelType_ShouldReturnOkResult_WithListOfChannels()
        {
            // Arrange
            int channelTypeId = 1;
            var mockChannels = new List<Channel>
            {
                new Channel { Channel_Name = "Test Channel 1", ChannelType_Id = 1, Channel_ImagePath = "Default/Path" }
            };
            _mockChannelService.Setup(service => service.GetChannelsByChannelType(channelTypeId))
                .ReturnsAsync(mockChannels);

            // Act
            var result = await _controller.GetChannelsByChannelType(channelTypeId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Channel>>(okResult.Value);
            var channels = okResult.Value as List<Channel>;
            Assert.AreEqual(1, channels.Count);
        }

        [Test]
        public async Task CreateChannel_ShouldReturnCreatedAtActionResult_WithNewChannel()
        {
            // Arrange
            var createChannelDTO = new CreateChannelDTO { Channel_Name = "New Channel", ChannelType_Id = 1 };
            var createdChannel = new Channel { Channel_Id = 1, Channel_Name = createChannelDTO.Channel_Name, ChannelType_Id = createChannelDTO.ChannelType_Id };
            _mockChannelService.Setup(service => service.CreateChannel(createChannelDTO))
                .ReturnsAsync(createdChannel);

            // Act
            var result = await _controller.CreateChannel(createChannelDTO);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.IsInstanceOf<Channel>(createdAtActionResult.Value);
            var channel = createdAtActionResult.Value as Channel;
            Assert.AreEqual("New Channel", channel.Channel_Name);
        }

        [Test]
        public async Task UpdateChannel_ShouldReturnOkResult_WithUpdatedChannel()
        {
            // Arrange
            int channelId = 1;
            var updateChannelDTO = new UpdateChannelDTO { Channel_Name = "Updated Channel", ChannelType_Id = 1, Channel_ImagePath = "Default/Path" };
            var updatedChannel = new Channel { Channel_Id = channelId, Channel_Name = updateChannelDTO.Channel_Name, ChannelType_Id = (int)updateChannelDTO.ChannelType_Id, Channel_ImagePath = updateChannelDTO.Channel_ImagePath };
            _mockChannelService.Setup(service => service.UpdateChannel(channelId, updateChannelDTO))
                .ReturnsAsync(updatedChannel);

            // Act
            var result = await _controller.UpdateChannel(channelId, updateChannelDTO);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<Channel>(okResult.Value);
            var channel = okResult.Value as Channel;
            Assert.AreEqual("Updated Channel", channel.Channel_Name);
        }

        [Test]
        public async Task DeleteChannel_ShouldReturnNoContentResult_WhenSuccessful()
        {
            // Arrange
            var deleteChannelDTO = new DeleteChannelDTO { Channel_Id = 1, Person_Id = 1 };
            _mockPersonService.Setup(service => service.GetPersonById(deleteChannelDTO.Person_Id))
                .ReturnsAsync(new Person { Person_Id = 1, PersonRole_Id = 2 });
            _mockChannelService.Setup(service => service.DeleteChannel(deleteChannelDTO.Channel_Id))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteChannel(deleteChannelDTO);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task GetAllMessagesFromChannel_ShouldReturnOkResult_WithListOfMessages()
        {
            // Arrange
            int channelId = 1;
            var mockMessages = new List<Message>
            {
                new Message { Message_Id = 1, Message_Text = "Test message" }
            };
            _mockChannelService.Setup(service => service.GetAllMessagesFromChannel(channelId))
                .ReturnsAsync(mockMessages);

            // Act
            var result = await _controller.GetAllMessagesFromChannel(channelId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Message>>(okResult.Value);
            var messages = okResult.Value as List<Message>;
            Assert.AreEqual(mockMessages.Count, messages.Count);
        }
    }
}
