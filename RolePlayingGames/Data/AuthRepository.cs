using Microsoft.EntityFrameworkCore;
using RolePlayingGames.Models;

namespace RolePlayingGames.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user= await _context.Users.FirstOrDefaultAsync(x=>x.UserName.ToLower()==username.ToLower());
            if(user==null)
            {
                response.Success = false;
                response.Message = "User not found";
                

            }
            
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Password is Wrong";
                
            }
            else
            {
                response.Data = user.Id.ToString();
            }
            
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if(await UserExists(user.UserName))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User Already Exists";
                return serviceResponse;
            }
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;
           _context.Users.Add(user);
            await _context.SaveChangesAsync();
           
            serviceResponse.Data=user.Id;
            return serviceResponse;

        }

        public async Task<bool> UserExists(string userName)
        {
            if(await _context.Users.AnyAsync(x=>x.UserName.ToLower()==userName.ToLower()))
            {
                return true;
            }
            return false;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt=hmac.Key;
            };

        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var response = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(response);
            };
        }
    }
}
