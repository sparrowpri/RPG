using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RolePlayingGames.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RolePlayingGames.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                response.Data = CreateToken(user);
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
        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt=hmac.Key;
            };

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var response = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(response);
            };
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };
            JwtSecurityTokenHandler handler=new JwtSecurityTokenHandler();
            SecurityToken token =handler.CreateToken(descriptor);
            return handler.WriteToken(token);

        }
    }
}
