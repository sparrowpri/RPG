using System.Text.Json.Serialization;

namespace RolePlayingGames.Models
{
    
    
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum RpgClass
        {
            Knight = 1,
            KingsGuard = 2,
            King = 3,
            Queen=4,
            Sellsword=5,


        }
    }

