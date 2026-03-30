using Microsoft.EntityFrameworkCore;
using PetitionBackend.Models;

namespace PetitionBackend.DbContexts
{
    // ▶ Контекст бази даних для всього додатку
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
