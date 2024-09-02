using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Dtos.Pets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
   
    [Route("api/pet")]
    [ApiController]
    public class PetController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        
        public PetController(ApplicationDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var pets = await _context.Pets.ToListAsync();
            var petsDto = pets.Select(pets => pets.ToDto());
            return Ok(petsDto);
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id){

            var pet = await _context.Pets.FirstOrDefaultAsync(u => u.id == id);
             if(pet == null){
                return NotFound();
            }else{
            var usersDto =PetMapper.ToDto(pet);
            return Ok(usersDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetRequestDto petDto)
        {
            if (petDto == null)
            {
                return BadRequest("Invalid pet data.");
            }

            // Mapeo de DTO a la entidad Pet
            var pet = PetMapper.ToPetFromCreateDto(petDto);

            // Agrega la nueva mascota al contexto
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();

            // Mapeo de la entidad a DTO para devolverlo en la respuesta
            var petCreatedDto = PetMapper.ToDto(pet);

            return CreatedAtAction(nameof(GetPetById), new { id = pet.id }, petCreatedDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet([FromRoute] int id, [FromBody] UpdatePetRequestDto petDto)
        {
            if (petDto == null)
            {
                return BadRequest("Invalid pet data.");
            }

            // Busca la mascota en la base de datos por su ID
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound("Pet not found.");
            }
            // por si los datos llegan vacios
            if (!string.IsNullOrEmpty(petDto.name) && petDto.name != "string"){
                pet.name = petDto.name;
            }

            if (!string.IsNullOrEmpty(petDto.animal) && petDto.animal != "string"){
                pet.animal = petDto.animal;
            }

            // Marca la entidad como modificada
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();

            // Mapeo de la entidad a DTO para devolverlo en la respuesta
            var updatedPetDto = PetMapper.ToDto(pet);

            return Ok(updatedPetDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            // Busca la mascota en la base de datos por su ID
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound("Pet not found.");
            }

            // Elimina la mascota del contexto
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 No Content
        }



         
    }
} 