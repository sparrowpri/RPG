namespace RolePlayingGames.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public Byte[] PasswordHash{ get; set; }
        public Byte[] PasswordSalt { get; set; }
        public List<Character>? Characters { get; set; }

    }
}
