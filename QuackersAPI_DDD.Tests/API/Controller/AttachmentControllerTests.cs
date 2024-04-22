using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QuackersAPI_DDD.API.Controller;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
using QuackersAPI_DDD.API.DTO.AttachmentDTO;

namespace QuackersAPI_DDD.Tests.API.Controller
{
    public class AttachmentControllerTests
    {
        private readonly Mock<IAttachmentService> _mockAttachmentService;
        private readonly AttachmentController _controller;

        public AttachmentControllerTests()
        {
            _mockAttachmentService = new Mock<IAttachmentService>();
            _controller = new AttachmentController(_mockAttachmentService.Object);
        }

        [Fact]
        public async Task GetAllAttachment_ReturnsOkWithAttachments()
        {
            // Arrange
            var attachments = new List<Attachment> { new Attachment { Attachment_Id = 1, Attachment_Name = "Attachment1" } };
            _mockAttachmentService.Setup(s => s.GetAllAttachments()).ReturnsAsync(attachments);

            // Act
            var result = await _controller.GetAllAttachment();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAttachments = Assert.IsType<List<Attachment>>(okResult.Value);
            Assert.Equal(attachments, returnedAttachments);
        }

        [Fact]
        public async Task GetAttachmentById_ReturnsOkWithAttachment()
        {
            // Arrange
            var expectedAttachment = new Attachment { Attachment_Id = 1, Attachment_Name = "ExampleAttachment" };
            _mockAttachmentService.Setup(s => s.GetAttachmentById(1)).ReturnsAsync(expectedAttachment);

            // Act
            var result = await _controller.GetAttachmentById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAttachment = Assert.IsType<Attachment>(okResult.Value);
            Assert.Equal(expectedAttachment, returnedAttachment);
        }

        [Fact]
        public async Task GetAttachmentById_ReturnsNotFoundWhenAttachmentDoesNotExist()
        {
            // Arrange
            _mockAttachmentService.Setup(s => s.GetAttachmentById(1)).ThrowsAsync(new KeyNotFoundException("No attachment found with ID 1"));

            // Act
            var result = await _controller.GetAttachmentById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No attachment found with ID 1", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAttachmentById_ReturnsServerErrorOnException()
        {
            // Arrange
            _mockAttachmentService.Setup(s => s.GetAttachmentById(1)).ThrowsAsync(new Exception("Internal server error"));

            // Act
            var result = await _controller.GetAttachmentById(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while get the attachment: Internal server error", statusCodeResult.Value);
        }

        [Fact]
        public async Task CreateAttachments_ReturnsBadRequestWhenNoFilesReceived()
        {
            // Arrange
            var dto = new CreateAttachmentDTO();
            var files = new List<IFormFile>(); // Empty list

            // Act
            var result = await _controller.CreateAttachments(dto, files);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateAttachments_ReturnsCreatedResultWithAttachmentDetails()
        {
            // Arrange
            var dto = new CreateAttachmentDTO();
            var mockFile = new Mock<IFormFile>();
            var files = new List<IFormFile> { mockFile.Object };
            var newAttachments = new List<Attachment> {
                new Attachment { Attachment_Id = 1, Attachment_Name = "Attachment1" }
            };

            _mockAttachmentService.Setup(s => s.CreateAttachments(dto, files)).ReturnsAsync(newAttachments);

            // Act
            var result = await _controller.CreateAttachments(dto, files);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdAtActionResult.Value);
            var returnedAttachments = Assert.IsType<List<Attachment>>(createdAtActionResult.Value);
            Assert.Equal(newAttachments, returnedAttachments);
        }

        [Fact]
        public async Task CreateAttachments_ReturnsNotFoundWhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            var dto = new CreateAttachmentDTO();
            var mockFile = new Mock<IFormFile>();
            var files = new List<IFormFile> { mockFile.Object };

            _mockAttachmentService.Setup(s => s.CreateAttachments(dto, files))
                .ThrowsAsync(new KeyNotFoundException("Specific key not found"));

            // Act
            var result = await _controller.CreateAttachments(dto, files);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Specific key not found", notFoundResult.Value);
        }

        [Fact]
        public async Task CreateAttachments_ReturnsServerErrorWhenExceptionThrown()
        {
            // Arrange
            var dto = new CreateAttachmentDTO();
            var mockFile = new Mock<IFormFile>();
            var files = new List<IFormFile> { mockFile.Object };

            _mockAttachmentService.Setup(s => s.CreateAttachments(dto, files))
                .ThrowsAsync(new Exception("Internal server error"));

            // Act
            var result = await _controller.CreateAttachments(dto, files);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("An error occurred while creating attachments: Internal server error", objectResult.Value);
        }

        [Fact]
        public async Task DeleteAttachmentById_ReturnsOkWhenDeletedSuccessfully()
        {
            // Arrange
            int id = 1;
            _mockAttachmentService.Setup(s => s.DeleteAttachment(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAttachmentById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Attachment with id 1 deleted successfully.", okResult.Value);
        }

        [Fact]
        public async Task DeleteAttachmentById_ReturnsNotFoundWhenAttachmentDoesNotExist()
        {
            // Arrange
            int id = 1;
            _mockAttachmentService.Setup(s => s.DeleteAttachment(id))
                .ThrowsAsync(new KeyNotFoundException($"Attachment with id {id} not found"));

            // Act
            var result = await _controller.DeleteAttachmentById(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Attachment with id {id} not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteAttachmentById_ReturnsServerErrorWhenExceptionThrown()
        {
            // Arrange
            int id = 1;
            _mockAttachmentService.Setup(s => s.DeleteAttachment(id))
                .ThrowsAsync(new Exception("Internal server error"));

            // Act
            var result = await _controller.DeleteAttachmentById(id);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal($"An error occurred while deleting the attachment: Internal server error", objectResult.Value);
        }
    }
}
