﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Task = Domain.Entities.Task;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Household> Households { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskTemplate> TaskTemplates { get; set; }
    public DbSet<Invitation> Invitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureInvitations();
        modelBuilder.ConfigureTasks();
        modelBuilder.ConfigureHouseholds();
        modelBuilder.HasDefaultSchema("fifty-fifty");
        modelBuilder.SeedTaskTemplates();
    }
}