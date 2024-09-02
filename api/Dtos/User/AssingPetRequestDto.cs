using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class AssingPetRequestDto
    {
        public int IdUser { get; set; }
        public int IdPet { get; set; }
    }
}