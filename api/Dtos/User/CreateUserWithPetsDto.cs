using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Pets;

namespace api.Dtos.User
{
    public class CreateUserWithPetsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<PetDto> Pets { get; set; } = new List<PetDto>();
    }
}