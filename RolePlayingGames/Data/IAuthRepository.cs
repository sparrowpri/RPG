using RolePlayingGames.Models;

namespace RolePlayingGames.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(int Userid, string password);
        Task<ServiceResponse<int>> Login(string username, string password);
    }
}
