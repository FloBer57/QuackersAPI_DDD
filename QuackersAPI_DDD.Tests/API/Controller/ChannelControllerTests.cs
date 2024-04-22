using Microsoft.AspNetCore.Mvc;
using Moq;
using QuackersAPI_DDD.API.Controller;
using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuackersAPI_DDD.Tests.API.Controller
{
    public class ChannelControllerTests
    {
        private readonly Mock<IChannelService> _mockService;
        private readonly ChannelController _controller;

        public ChannelControllerTests()
        {
            _mockService = new Mock<IChannelService>();
            _controller = new ChannelController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllChannels_ReturnsOkObjectResult_WithListOfChannels()
        {
            // Arrange
            var channels = new List<Channel>
    {
        new Channel { Channel_Id = 1, Channel_Name = "News" },
        new Channel { Channel_Id = 2, Channel_Name = "Updates" }
    };
            _mockService.Setup(service => service.GetAllChannels()).ReturnsAsync(channels);

            // Act
            var result = await _controller.GetAllChannels();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedChannels = Assert.IsType<List<Channel>>(okResult.Value);
            Assert.Equal(2, returnedChannels.Count);
            _mockService.Verify(service => service.GetAllChannels(), Times.Once);
        }

        [Fact]
        public async Task GetChannelById_ReturnsOkObjectResult_WithChannel()
        {
            // Arrange
            var channel = new Channel { Channel_Id = 1, Channel_Name = "News" };
            _mockService.Setup(service => service.GetChannelById(1)).ReturnsAsync(channel);

            // Act
            var result = await _controller.GetChannelById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedChannel = Assert.IsType<Channel>(okResult.Value);
            Assert.Equal(channel, returnedChannel);
            _mockService.Verify(service => service.GetChannelById(1), Times.Once);
        }

        [Fact]
        public async Task GetChannelById_ReturnsNotFound_WhenChannelDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetChannelById(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetChannelById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateChannel_ReturnsCreatedAtActionResult_WithChannelDetails()
        {
            // Arrange
            var channelDto = new CreateChannelDTO { Channel_Name = "New Channel" };
            var createdChannel = new Channel { Channel_Id = 3, Channel_Name = "New Channel" };
            _mockService.Setup(service => service.CreateChannel(channelDto)).ReturnsAsync(createdChannel);

            // Act
            var result = await _controller.CreateChannel(channelDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedChannel = Assert.IsType<Channel>(createdResult.Value);
            Assert.Equal(createdChannel, returnedChannel);
        }

        [Fact]
        public async Task CreateChannel_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Channel_Name", "Required");

            // Act
            var result = await _controller.CreateChannel(new CreateChannelDTO());

            // Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task GetChannelsByChannelType_ReturnsOkObjectResult_WithChannels()
        {
            // Arrange
            var channels = new List<Channel>
    {
        new Channel { Channel_Id = 1, Channel_Name = "Tech" },
        new Channel { Channel_Id = 2, Channel_Name = "General" }
    };
            _mockService.Setup(service => service.GetChannelsByChannelType(1)).ReturnsAsync(channels);

            // Act
            var result = await _controller.GetChannelsByChannelType(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedChannels = Assert.IsType<List<Channel>>(okResult.Value);
            Assert.Equal(2, returnedChannels.Count);
        }

        [Fact]
        public async Task GetChannelsByChannelType_ReturnsNotFound_WhenChannelTypeDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetChannelsByChannelType(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetChannelsByChannelType(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateChannel_ReturnsOkObjectResult_WithUpdatedChannelDetails()
        {
            // Arrange
            var updateDto = new UpdateChannelDTO { Channel_Name = "Updated Channel" };
            var updatedChannel = new Channel { Channel_Id = 1, Channel_Name = "Updated Channel" };
            _mockService.Setup(service => service.UpdateChannel(1, updateDto)).ReturnsAsync(updatedChannel);

            // Act
            var result = await _controller.UpdateChannel(1, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedChannel = Assert.IsType<Channel>(okResult.Value);
            Assert.Equal("Updated Channel", returnedChannel.Channel_Name);
        }

        [Fact]
        public async Task UpdateChannel_ReturnsNotFound_WhenChannelDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateChannelDTO { Channel_Name = "Nonexistent" };
            _mockService.Setup(service => service.UpdateChannel(1, updateDto)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.UpdateChannel(1, updateDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteChannel_ReturnsNoContent_WhenDeletedSuccessfully()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteChannel(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteChannel(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteChannel_ReturnsNotFound_WhenChannelDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteChannel(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.DeleteChannel(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllMessagesFromChannel_ReturnsOkObjectResult_WithMessages()
        {
            // Arrange
            var messages = new List<Message>
    {
        new Message { Message_Id = 1, Message_Text = "Hello" },
        new Message { Message_Id = 2, Message_Text = "World" }
    };
            _mockService.Setup(service => service.GetAllMessagesFromChannel(1)).ReturnsAsync(messages);

            // Act
            var result = await _controller.GetAllMessagesFromChannel(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMessages = Assert.IsType<List<Message>>(okResult.Value);
            Assert.Equal(2, returnedMessages.Count);
        }

        [Fact]
        public async Task GetAllMessagesFromChannel_ReturnsNotFound_WhenChannelDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllMessagesFromChannel(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetAllMessagesFromChannel(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }




    }
}
