﻿using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXMessageDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using System;
using System.Threading.Tasks;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonXMessageController : ControllerBase
    {
        private readonly IPersonXMessageService _service;

        public PersonXMessageController(IPersonXMessageService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssociations()
        {

                var associations = await _service.GetAllAssociations();
                return Ok(associations);
        }

        [HttpGet("{personId}/{messageId}")]
        public async Task<IActionResult> GetAssociationById(int personId, int messageId)
        {
            try
            {
                var association = await _service.GetAssociationById(personId, messageId);
                if (association == null)
                {
                    return NotFound($"Association not found with person ID {personId} and message ID {messageId}.");
                }
                return Ok(association);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssociation([FromBody] CreatePersonXMessageDTO dto)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetAssociationById), new { personId = dto.PersonId, messageId = dto.MessageId }, createdAssociation);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpPut("{personId}/{messageId}")]
        public async Task<IActionResult> UpdateAssociation(int personId, int messageId, [FromBody] UpdatePersonXMessageDTO dto)
        {
            try
            {
                var updatedAssociation = await _service.UpdateAssociation(personId, messageId, dto);
                return Ok(updatedAssociation);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpDelete("{personId}/{messageId}")]
        public async Task<IActionResult> DeleteAssociation(int personId, int messageId)
        {
            try
            {
                bool success = await _service.DeleteAssociation(personId, messageId);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
