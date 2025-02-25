﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("fifty-fifty")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HouseholdUser", b =>
                {
                    b.Property<Guid>("HouseholdsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HouseholdsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("HouseholdUser", "fifty-fifty");
                });

            modelBuilder.Entity("Models.Household", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Households", "fifty-fifty");
                });

            modelBuilder.Entity("Models.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("HouseHoldId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InvitedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InvitingUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("HouseHoldId");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("InvitingUserId");

                    b.ToTable("Invitations", "fifty-fifty");
                });

            modelBuilder.Entity("Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", "fifty-fifty");
                });

            modelBuilder.Entity("Models.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("AddedAt")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("CompletedAt")
                        .HasColumnType("date");

                    b.Property<Guid>("HouseHoldId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("HouseHoldId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks", "fifty-fifty");
                });

            modelBuilder.Entity("Models.TaskTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaskTemplates", "fifty-fifty");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Score = 10,
                            Title = "Do the dishes"
                        },
                        new
                        {
                            Id = 2,
                            Score = 5,
                            Title = "Take out the trash"
                        },
                        new
                        {
                            Id = 3,
                            Score = 15,
                            Title = "Vacuum the floor"
                        },
                        new
                        {
                            Id = 4,
                            Score = 20,
                            Title = "Clean the bathroom"
                        },
                        new
                        {
                            Id = 5,
                            Score = 25,
                            Title = "Mow the lawn"
                        },
                        new
                        {
                            Id = 6,
                            Score = 30,
                            Title = "Wash the car"
                        },
                        new
                        {
                            Id = 7,
                            Score = 10,
                            Title = "Walk the dog"
                        },
                        new
                        {
                            Id = 8,
                            Score = 5,
                            Title = "Feed the cat"
                        },
                        new
                        {
                            Id = 9,
                            Score = 5,
                            Title = "Water the plants"
                        },
                        new
                        {
                            Id = 10,
                            Score = 20,
                            Title = "Cook dinner"
                        },
                        new
                        {
                            Id = 11,
                            Score = 15,
                            Title = "Do the laundry"
                        },
                        new
                        {
                            Id = 12,
                            Score = 10,
                            Title = "Clean the windows"
                        },
                        new
                        {
                            Id = 13,
                            Score = 15,
                            Title = "Organize the closet"
                        },
                        new
                        {
                            Id = 14,
                            Score = 10,
                            Title = "Dust the furniture"
                        },
                        new
                        {
                            Id = 15,
                            Score = 5,
                            Title = "Take out recycling"
                        },
                        new
                        {
                            Id = 16,
                            Score = 5,
                            Title = "Wipe down kitchen counters"
                        },
                        new
                        {
                            Id = 17,
                            Score = 10,
                            Title = "Sweep the porch"
                        },
                        new
                        {
                            Id = 18,
                            Score = 15,
                            Title = "Change bed sheets"
                        },
                        new
                        {
                            Id = 19,
                            Score = 5,
                            Title = "Empty the dishwasher"
                        },
                        new
                        {
                            Id = 20,
                            Score = 10,
                            Title = "Fold and put away laundry"
                        },
                        new
                        {
                            Id = 21,
                            Score = 20,
                            Title = "Clean out the fridge"
                        },
                        new
                        {
                            Id = 22,
                            Score = 5,
                            Title = "Take care of compost"
                        },
                        new
                        {
                            Id = 23,
                            Score = 30,
                            Title = "Shovel snow"
                        },
                        new
                        {
                            Id = 24,
                            Score = 20,
                            Title = "Weed the garden"
                        },
                        new
                        {
                            Id = 25,
                            Score = 10,
                            Title = "Clean up after a meal"
                        },
                        new
                        {
                            Id = 26,
                            Score = 10,
                            Title = "Sweep the floor"
                        },
                        new
                        {
                            Id = 27,
                            Score = 10,
                            Title = "Tidy up the living room"
                        },
                        new
                        {
                            Id = 28,
                            Score = 5,
                            Title = "Wipe down bathroom mirror"
                        },
                        new
                        {
                            Id = 29,
                            Score = 5,
                            Title = "Take out the mail"
                        },
                        new
                        {
                            Id = 30,
                            Score = 15,
                            Title = "Organize pantry"
                        },
                        new
                        {
                            Id = 31,
                            Score = 5,
                            Title = "Replace toilet paper"
                        },
                        new
                        {
                            Id = 32,
                            Score = 5,
                            Title = "Check and refill pet water bowl"
                        },
                        new
                        {
                            Id = 33,
                            Score = 10,
                            Title = "Polish wooden furniture"
                        },
                        new
                        {
                            Id = 34,
                            Score = 5,
                            Title = "Sanitize door handles"
                        },
                        new
                        {
                            Id = 35,
                            Score = 15,
                            Title = "Clean behind the sofa"
                        },
                        new
                        {
                            Id = 36,
                            Score = 15,
                            Title = "Vacuum the stairs"
                        },
                        new
                        {
                            Id = 37,
                            Score = 5,
                            Title = "Take care of houseplants"
                        },
                        new
                        {
                            Id = 38,
                            Score = 5,
                            Title = "Organize shoes at the entrance"
                        },
                        new
                        {
                            Id = 39,
                            Score = 5,
                            Title = "Wipe down light switches"
                        },
                        new
                        {
                            Id = 40,
                            Score = 15,
                            Title = "Declutter a room"
                        },
                        new
                        {
                            Id = 41,
                            Score = 10,
                            Title = "Clean up kids' toys"
                        },
                        new
                        {
                            Id = 42,
                            Score = 10,
                            Title = "Scrub kitchen sink"
                        },
                        new
                        {
                            Id = 43,
                            Score = 10,
                            Title = "Sort out junk drawer"
                        },
                        new
                        {
                            Id = 44,
                            Score = 20,
                            Title = "Defrost the freezer"
                        },
                        new
                        {
                            Id = 45,
                            Score = 5,
                            Title = "Replace burnt-out light bulbs"
                        },
                        new
                        {
                            Id = 46,
                            Score = 10,
                            Title = "Organize office desk"
                        },
                        new
                        {
                            Id = 47,
                            Score = 5,
                            Title = "Disinfect TV remote and electronics"
                        },
                        new
                        {
                            Id = 48,
                            Score = 10,
                            Title = "Sweep the driveway"
                        });
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("nvarchar(48)");

                    b.Property<DateTime?>("LastLoginAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Users", "fifty-fifty");
                });

            modelBuilder.Entity("HouseholdUser", b =>
                {
                    b.HasOne("Models.Household", null)
                        .WithMany()
                        .HasForeignKey("HouseholdsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Invitation", b =>
                {
                    b.HasOne("Models.Household", "Household")
                        .WithMany()
                        .HasForeignKey("HouseHoldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "InvitedUser")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.User", "InvitingUser")
                        .WithMany("SentInvitations")
                        .HasForeignKey("InvitingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Household");

                    b.Navigation("InvitedUser");

                    b.Navigation("InvitingUser");
                });

            modelBuilder.Entity("Models.RefreshToken", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Task", b =>
                {
                    b.HasOne("Models.Household", "Household")
                        .WithMany("Tasks")
                        .HasForeignKey("HouseHoldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("AssignedUser");

                    b.Navigation("Household");
                });

            modelBuilder.Entity("Models.Household", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("ReceivedInvitations");

                    b.Navigation("RefreshTokens");

                    b.Navigation("SentInvitations");
                });
#pragma warning restore 612, 618
        }
    }
}
