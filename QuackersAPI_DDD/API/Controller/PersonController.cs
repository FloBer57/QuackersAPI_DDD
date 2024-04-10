using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Domain.Utilitie;

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

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personService.GetAllPersons();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound($"Person with id {id} not found.");
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDTO createPersonDTO)
        {
            var createdPerson = await _personService.CreatePerson(createPersonDTO);
            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Person_Id }, createdPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonDTO updatePersonDTO)
        {
            var updatedPerson = await _personService.UpdatePerson(id, updatePersonDTO);
            if (updatedPerson == null)
            {
                return NotFound($"Person with id {id} not found.");
            }

            return Ok(updatedPerson);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var success = await _personService.DeletePerson(id);
            if (!success)
            {
                return NotFound($"Person with id {id} not found.");
            }
            return Ok($"Person with id {id} deleted successfully.");
        }
    }
}