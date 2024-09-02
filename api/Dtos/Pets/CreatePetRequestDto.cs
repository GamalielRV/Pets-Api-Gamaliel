using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Pets
{
     public class CreatePetRequestDto
    {
        public string Name { get; set; }
        public string Animal { get; set; }
        public int UserId { get; set; }
    }

} 