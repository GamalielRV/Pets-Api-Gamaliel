namespace api.Models
{
    public class Pet
    {
        public int id {get ; set;}
        public string name {get ; set;}
        public string animal {get ; set;}
        public int userId {get ; set;}
        public User user { get; set;}

    }
}