﻿using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelPersonRoleDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelPersonRoleController : ControllerBase
    {
        private readonly IChannelPersonRoleService _channelPersonRoleService;

        public ChannelPersonRoleController(IChannelPersonRoleService channelPersonRoleService)
        {
            _channelPersonRoleService = channelPersonRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChannelPersonRoles()
        {
                var roles = await _channelPersonRoleService.GetAllChannelPersonRoles();
                return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelPersonRoleById(int id)
        {
            try
            {
                var role = await _channelPersonRoleService.GetChannelPersonRoleById(id);
                return Ok(role);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"ChannelPersonRole with id {id} not found.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannelPersonRole([FromBody] CreateChannelPersonRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRole = await _channelPersonRoleService.CreateChannelPersonRole(dto);
                return CreatedAtAction(nameof(GetChannelPersonRoleById), new { id = createdRole.ChannelPersonRole_Id }, createdRole);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the channel person role." + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChannelPersonRole(int id, [FromBody] UpdateChannelPersonRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedRole = await _channelPersonRoleService.UpdateChannelPersonRole(id, dto);
                return Ok(updatedRole);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the channel person role: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannelPersonRole(int id)
        {
            try
            {
                bool deleted = await _channelPersonRoleService.DeleteChannelPersonRole(id);
                return Ok($"ChannelPersonRole with id {id} has been deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the channel person role: " + ex.Message);
            }

        }
    }
}
