using System.Text.Json.Serialization;
namespace RPG.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight =1,
        Maze =2,
        Cleric=3

    }
}