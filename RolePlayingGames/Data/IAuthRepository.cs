using RolePlayingGames.Models;
using System.Diagnostics.Eventing.Reader;

namespace RolePlayingGames.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string userName);
       
        
    }
}
