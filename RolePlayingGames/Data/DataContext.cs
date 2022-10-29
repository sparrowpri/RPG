using Microsoft.EntityFrameworkCore;
using RolePlayingGames.Models;

namespace RolePlayingGames.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
                
        }
        public DbSet<Character> Characters { get; set; }

    }
}
