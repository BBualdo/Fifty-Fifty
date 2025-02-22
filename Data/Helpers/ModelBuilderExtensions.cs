using Microsoft.EntityFrameworkCore;
using Models;
using Task = Models.Task;

namespace Data.Helpers;

internal static class ModelBuilderExtensions
{
    internal static void SeedTaskTemplates(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskTemplate>().HasData(SeedGenerator.GenerateTemplateTasks());
    }

    internal static void ConfigureInvitations(this ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<Invitation>(entity =>
        {
            // Configure relationship
            entity.HasOne(i => i.InvitingUser)
            .WithMany(u => u.SentInvitations)
            .HasForeignKey(i => i.InvitingUserId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.InvitedUser)
            .WithMany(u => u.ReceivedInvitations)
            .HasForeignKey(i => i.InvitedUserId)
            .OnDelete(DeleteBehavior.NoAction);

            // Configure properties
            entity.Property(i => i.ExpirationDate).IsRequired();
            entity.Property(i => i.InvitedUserId).IsRequired();
            entity.Property(i => i.HouseHoldId).IsRequired();
            entity.Property(i => i.InvitingUserId).IsRequired();
        });
    }

    internal static void ConfigureTasks(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>(entity =>
        {
            entity.Property(t => t.Title).IsRequired().HasMaxLength(64);
            entity.Property(t => t.AddedAt).IsRequired();
            entity.Property(t => t.Score).IsRequired();
        });
    }

    internal static void ConfigureHouseholds(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Household>(entity =>
        {
            entity.Property(h => h.Name).IsRequired().HasMaxLength(64);
        });
    }
}