using Microsoft.EntityFrameworkCore;
using Models;

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
            entity.HasOne(i => i.InvitingUser)
            .WithMany(u => u.SentInvitations)
            .HasForeignKey(i => i.InvitingUserId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.InvitedUser)
            .WithMany(u => u.ReceivedInvitations)
            .HasForeignKey(i => i.InvitedUserId)
            .OnDelete(DeleteBehavior.NoAction);
        });
    }
}