using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;

namespace MSAuth.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Don't remove! Important to set DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<UserConfirmation> UserConfirmations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais, como chaves primárias compostas, índices, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
