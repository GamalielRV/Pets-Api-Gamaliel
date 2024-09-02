using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var users = await _context.Users.Include(user => user.pets).ToListAsync();
            var usersDto = users.Select(users => users.ToDto());
            return Ok(usersDto);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] int id){
            var user =  await _context.Users.Include(user => user.pets).FirstOrDefaultAsync(u => u.id == id);
            if(user == null){
                return NotFound();
            }
            return Ok(user.ToDto());
        }

        [HttpPost]
          public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto){
            var userModel = userDto.ToUserFromCreateDto();
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getById), new { id = userModel.id}, userModel.ToDto());
        }

        [HttpPut]
        [Route("{id}")]
         public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto){
            var userModel = await _context.Users.FirstOrDefaultAsync(user => user.id == id);
            if (userModel == null){
                return NotFound();
            }
            userModel.age = userDto.Age;
            userModel.firstName = userDto.FirstName;
            userModel.lastName = userDto.LastName;

            _context.SaveChanges();
            await _context.SaveChangesAsync();

            return Ok(userModel.ToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var userModel = await _context.Users.FirstOrDefaultAsync(user => user.id == id);
            if (userModel == null){
                return NotFound();
            }
            _context.Users.Remove(userModel);

            _context.SaveChanges();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{userId}/assign-pet/{petId}")]
        public async Task<IActionResult> AssignPetToUser([FromBody] AssingPetRequestDto assingPet)
        {
            // Buscar el usuario por ID
            var user = await _context.Users.FirstOrDefaultAsync(u => u.id == assingPet.IdUser);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Buscar la mascota por ID
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.id == assingPet.IdPet);
            if (pet == null)
            {
                return NotFound("Pet not found.");
            }

            // Asigna el usuario a la mascota
            pet.userId = user.id;

            // Guarda los cambios en la base de datos
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();

            return Ok($"Pet {pet.name} has been assigned to user {user.firstName} {user.lastName}.");
        }    

       
    }
 }
