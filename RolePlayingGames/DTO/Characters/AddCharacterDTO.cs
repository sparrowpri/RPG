using RolePlayingGames.Models;

namespace RolePlayingGames.DTO.Characters
{
    public class AddCharacterDTO
    {
       
        public string Name { get; set; } = "frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}
