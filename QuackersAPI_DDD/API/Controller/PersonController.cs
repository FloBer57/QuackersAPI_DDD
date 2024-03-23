using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.Application.DTO.Request;
using QuackersAPI_DDD.Application.DTO.Response;
using QuackersAPI_DDD.Application.Interface;
using Microsoft.EntityFrameworkCore; // Ajoutez cette ligne
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using System;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }


        // Create api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreatePersonRequestDTO createUserRequestDTO)
        {
            Person person = new Person
            {
                Person_FirstName = createUserRequestDTO.Firstname,
                Person_LastName = createUserRequestDTO.LastName,
                Person_Email = createUserRequestDTO.Email,
                Person_PhoneNumber = createUserRequestDTO.PhoneNumber,
            };
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            var messageDto = new CreatePersonResponseDTO(person);
            return CreatedAtAction(nameof(GetUserById), new { id = person.Person_Id }, new { person, message = messageDto.Message });
        }


        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<Person> users = await _context.Person.ToListAsync();
            string message = $"Nombre total d'utilisateurs récupérés : {users.Count}.";
            return Ok(new { Users = users, Message = message });
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            Person person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound($"Utilisateur avec l'ID {id} non trouvé.");
            }

            string message = $"L'utilisateur avec l'ID {id} a été récupéré avec succès.";
            return Ok(new { User = person, Message = message });
        }
    }
}
