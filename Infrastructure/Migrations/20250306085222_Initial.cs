using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
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
                name: "Users",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastLoginAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdUser",
                schema: "fifty-fifty",
                columns: table => new
                {
                    HouseholdsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdUser", x => new { x.HouseholdsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_HouseholdUser_Households_HouseholdsId",
                        column: x => x.HouseholdsId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseholdUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Users",
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
                    InvitedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseHoldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Households_HouseHoldId",
                        column: x => x.HouseHoldId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_Users_InvitingUserId",
                        column: x => x.InvitingUserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "fifty-fifty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Users",
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HouseHoldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Households_HouseHoldId",
                        column: x => x.HouseHoldId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "fifty-fifty",
                        principalTable: "Users",
                        principalColumn: "Id");
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
                name: "IX_RefreshTokens_UserId",
                schema: "fifty-fifty",
                table: "RefreshTokens",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "fifty-fifty",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "fifty-fifty",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseholdUser",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "TaskTemplates",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Households",
                schema: "fifty-fifty");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "fifty-fifty");
        }
    }
}
