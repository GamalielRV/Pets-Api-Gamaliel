
namespace api.Models
{
    public class User
    {
        public int id {get; set;}
        public string firstName {get; set;}
        public string lastName {get; set;}
        public int age {get; set;}
        public DateTime  contexedAt {get; set;} = DateTime.Now;
        public List<Pet> pets {get; set;} = new List<Pet>();
        
    }
}