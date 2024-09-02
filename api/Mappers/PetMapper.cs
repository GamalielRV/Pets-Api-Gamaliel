using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Pets;
using api.Models;

namespace api.Mappers
{
     public static class PetMapper
    {
        public static PetDto ToDto(this Pet pet){
            return new PetDto{
                Name = pet.name,
                Animal = pet.animal
            };
        }

        public static Pet ToPetFromCreateDto(this CreatePetRequestDto petDto)
        {
            return new Pet
            {
                name = petDto.Name,
                animal = petDto.Animal,
                userId = petDto.UserId
            };
        }

        
    }
}