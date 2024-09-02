using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Pets
{
    public class UpdatePetRequestDto
    {
        public string name {get ; set;}
        public string animal {get ; set;}
    }
}