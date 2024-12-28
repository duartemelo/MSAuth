using Microsoft.EntityFrameworkCore;
using MSGym.Domain.Entities;

namespace MSGym.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Don't remove! Important to set DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymContact> GymContacts { get; set; }
        public DbSet<GymSchedule> GymSchedules { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<TrainingPlanExercise> TrainingPlanExercises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais, como chaves primárias compostas, índices, etc.
            base.OnModelCreating(modelBuilder);

            // UserRole -> Role
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .OnDelete(DeleteBehavior.Cascade);

            // GymContact -> Gym
            modelBuilder.Entity<GymContact>()
                .HasOne(gc => gc.Gym)
                .WithMany(g => g.Contacts)
                .OnDelete(DeleteBehavior.Cascade);

            // GymSchedule -> Gym
            modelBuilder.Entity<GymSchedule>()
                .HasOne(gs => gs.Gym)
                .WithMany(g => g.Schedules)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
