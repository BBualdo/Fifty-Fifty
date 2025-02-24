using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fifty-fifty");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Households",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Households", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTemplates",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "fifty-fifty",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "fifty-fifty",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "fifty-fifty",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdUser",
                schema: "fifty-fifty",
                columns: table => new
                {
                    HouseholdsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdUser", x => new { x.HouseholdsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_HouseholdUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseholdUser_Households_HouseholdsId",
                        column: x => x.HouseholdsId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvitedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HouseHoldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitingUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_InvitingUserId",
                        column: x => x.InvitingUserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Households_HouseHoldId",
                        column: x => x.HouseHoldId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AddedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HouseHoldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Households_HouseHoldId",
                        column: x => x.HouseHoldId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "fifty-fifty",
                table: "TaskTemplates",
                columns: new[] { "Id", "Score", "Title" },
                values: new object[,]
                {
                    { 1, 10, "Do the dishes" },
                    { 2, 5, "Take out the trash" },
                    { 3, 15, "Vacuum the floor" },
                    { 4, 20, "Clean the bathroom" },
                    { 5, 25, "Mow the lawn" },
                    { 6, 30, "Wash the car" },
                    { 7, 10, "Walk the dog" },
                    { 8, 5, "Feed the cat" },
                    { 9, 5, "Water the plants" },
                    { 10, 20, "Cook dinner" },
                    { 11, 15, "Do the laundry" },
                    { 12, 10, "Clean the windows" },
                    { 13, 15, "Organize the closet" },
                    { 14, 10, "Dust the furniture" },
                    { 15, 5, "Take out recycling" },
                    { 16, 5, "Wipe down kitchen counters" },
                    { 17, 10, "Sweep the porch" },
                    { 18, 15, "Change bed sheets" },
                    { 19, 5, "Empty the dishwasher" },
                    { 20, 10, "Fold and put away laundry" },
                    { 21, 20, "Clean out the fridge" },
                    { 22, 5, "Take care of compost" },
                    { 23, 30, "Shovel snow" },
                    { 24, 20, "Weed the garden" },
                    { 25, 10, "Clean up after a meal" },
                    { 26, 10, "Sweep the floor" },
                    { 27, 10, "Tidy up the living room" },
                    { 28, 5, "Wipe down bathroom mirror" },
                    { 29, 5, "Take out the mail" },
                    { 30, 15, "Organize pantry" },
                    { 31, 5, "Replace toilet paper" },
                    { 32, 5, "Check and refill pet water bowl" },
                    { 33, 10, "Polish wooden furniture" },
                    { 34, 5, "Sanitize door handles" },
                    { 35, 15, "Clean behind the sofa" },
                    { 36, 15, "Vacuum the stairs" },
                    { 37, 5, "Take care of houseplants" },
                    { 38, 5, "Organize shoes at the entrance" },
                    { 39, 5, "Wipe down light switches" },
                    { 40, 15, "Declutter a room" },
                    { 41, 10, "Clean up kids' toys" },
                    { 42, 10, "Scrub kitchen sink" },
                    { 43, 10, "Sort out junk drawer" },
                    { 44, 20, "Defrost the freezer" },
                    { 45, 5, "Replace burnt-out light bulbs" },
                    { 46, 10, "Organize office desk" },
                    { 47, 5, "Disinfect TV remote and electronics" },
                    { 48, 10, "Sweep the driveway" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "fifty-fifty",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "fifty-fifty",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "fifty-fifty",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "fifty-fifty",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "fifty-fifty",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "fifty-fifty",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "fifty-fifty",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdUser_UsersId",
                schema: "fifty-fifty",
                table: "HouseholdUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_HouseHoldId",
                schema: "fifty-fifty",
                table: "Invitations",
                column: "HouseHoldId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitedUserId",
                schema: "fifty-fifty",
                table: "Invitations",
                column: "InvitedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitingUserId",
                schema: "fifty-fifty",
                table: "Invitations",
                column: "InvitingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_HouseHoldId",
                schema: "fifty-fifty",
                table: "Tasks",
                column: "HouseHoldId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                schema: "fifty-fifty",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "HouseholdUser",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "TaskTemplates",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Households",
                schema: "fifty-fifty");
        }
    }
}
