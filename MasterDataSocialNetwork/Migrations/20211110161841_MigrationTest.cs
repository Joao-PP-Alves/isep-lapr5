using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDDNetCore.Migrations
{
    public partial class MigrationTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LAPR5");

            migrationBuilder.CreateTable(
                name: "Connections",
                schema: "LAPR5",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    requester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    targetUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Introductions",
                schema: "LAPR5",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    decision_decision = table.Column<int>(type: "int", nullable: true),
                    MissionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requester_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Introductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                schema: "LAPR5",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    dificultyDegree_level = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "LAPR5",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_Strength = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emotionalState_emotion = table.Column<int>(type: "int", nullable: true),
                    emotionalState_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    emotionalState_TimeElapsed = table.Column<TimeSpan>(type: "time", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                schema: "LAPR5",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    connection_strenght = table.Column<float>(type: "real", nullable: false),
                    relationship_strenght = table.Column<float>(type: "real", nullable: false),
                    user1Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    user2Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    friendshipTag_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_user1Id",
                        column: x => x.user1Id,
                        principalSchema: "LAPR5",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_user2Id",
                        column: x => x.user2Id,
                        principalSchema: "LAPR5",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_tags",
                schema: "LAPR5",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_tags", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_Users_tags_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "LAPR5",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_user1Id",
                schema: "LAPR5",
                table: "Friendships",
                column: "user1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_user2Id",
                schema: "LAPR5",
                table: "Friendships",
                column: "user2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections",
                schema: "LAPR5");

            migrationBuilder.DropTable(
                name: "Friendships",
                schema: "LAPR5");

            migrationBuilder.DropTable(
                name: "Introductions",
                schema: "LAPR5");

            migrationBuilder.DropTable(
                name: "Missions",
                schema: "LAPR5");

            migrationBuilder.DropTable(
                name: "Users_tags",
                schema: "LAPR5");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "LAPR5");
        }
    }
}
