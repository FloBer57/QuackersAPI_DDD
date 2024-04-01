using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreatePersonRequestDTO createPersonRequestDTO)
        {
            CreatePersonResponseDTO createResponse = await _personService.CreatePerson(createPersonRequestDTO);
            if (createResponse == null)
            {
                return BadRequest("Unable to create user.");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = createResponse.Id }, createResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _personService.GetAllPersons();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _personService.GetPersonById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpPatch("update-password/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordRequestDTO request)
        {
            var updateResponse = await _personService.UpdatePassword(id, request.NewPassword);
            if (!updateResponse.Success)
            {
                return BadRequest(updateResponse.Message);
            }
            return Ok(updateResponse.Message);
        }

        [HttpPatch("update-phonenumber/{id}")]
        public async Task<IActionResult> UpdatePhoneNumber(int id, [FromBody] UpdatePhoneNumberRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResponse = await _personService.UpdatePhoneNumber(id, request.NewPhoneNumber);
            if (!updateResponse.Success)
            {
                return BadRequest(updateResponse.Message);
            }
            return Ok(updateResponse.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var deleteResponse = await _personService.DeletePerson(id);
            if (!deleteResponse.Success)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
    }
}