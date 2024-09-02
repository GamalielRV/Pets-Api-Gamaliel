using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
  public static class UserMapper
    {
        // de user a User.Dto
        public static UserDto ToDto(this User user){
            return new UserDto {
                Id = user.id,
                FirstName = user.firstName,
                LastName = user.lastName,
                Age = user.age,
                PetList = user.pets.Select(PetMapper.ToDto).ToList()
            };
        }

        // de createUser a User model
        public static User ToUserFromCreateDto(this CreateUserRequestDto createUserRequest){
            return new User{
                firstName = createUserRequest.FirstName,
                lastName = createUserRequest.LastName,
                age = createUserRequest.Age
            };
        }


          public static User ToUserWithPetsFromCreateDto(this CreateUserWithPetsDto createUserWithPetsDto)
        {
            return new User
            {
                firstName = createUserWithPetsDto.FirstName,
                lastName = createUserWithPetsDto.LastName,
                age = createUserWithPetsDto.Age,
                pets = createUserWithPetsDto.Pets.Select(petDto => new Pet
                {
                    name = petDto.Name,
                    animal = petDto.Animal
                }).ToList()
            };
        }
    }
}