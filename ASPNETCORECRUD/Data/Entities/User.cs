namespace ASPNETCORECRUD.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}
