using Data.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Task = Models.Task;

namespace Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Household> Households { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskTemplate> TaskTemplates { get; set; }
    public DbSet<Invitation> Invitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureInvitations();
        modelBuilder.ConfigureTasks();
        modelBuilder.ConfigureHouseholds();
        modelBuilder.HasDefaultSchema("fifty-fifty");
        modelBuilder.SeedTaskTemplates();
    }
}