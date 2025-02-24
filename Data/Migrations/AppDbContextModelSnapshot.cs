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

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("HouseholdsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("HouseholdUser", "fifty-fifty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", "fifty-fifty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", "fifty-fifty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", "fifty-fifty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", "fifty-fifty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", "fifty-fifty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", "fifty-fifty");
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

                    b.Property<string>("InvitedUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InvitingUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HouseHoldId");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("InvitingUserId");

                    b.ToTable("Invitations", "fifty-fifty");
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

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

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
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", "fifty-fifty");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
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

                    b.Navigation("SentInvitations");
                });
#pragma warning restore 612, 618
        }
    }
}
