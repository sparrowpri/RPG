using RolePlayingGames.DTO.Characters;
using RolePlayingGames.Models;

namespace RolePlayingGames.Services
{
    public interface ICharacterServices
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> AddChar(AddCharacterDTO newChar);
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllChar();
        Task<ServiceResponse<GetCharacterDTO?>> GetCharacterById(int id);
    }
}