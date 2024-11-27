using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;

namespace MSAuth.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Don't remove! Important to set DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserConfirmation> UserConfirmations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais, como chaves primárias compostas, índices, etc.
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ComplexProperty(u => u.Password);
        }

        // TODO: convert to interceptor (?)
        // TODO: test performance of UpdateDateOfModification!

        public override int SaveChanges()
        {
            UpdateDateOfModification();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateDateOfModification();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateDateOfModification()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && e.State == EntityState.Modified);

            foreach (var entry in modifiedEntities)
            {
                ((BaseEntity)entry.Entity).DateOfModification = DateTime.Now;
            }
        }
    }
}
