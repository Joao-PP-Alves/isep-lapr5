using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDDNetCore.Migrations
{
    public partial class BirthDateMigration : Migration
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
                    decisionStatus = table.Column<int>(type: "int", nullable: false),
                    MessageToIntermediate_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageToTargetUser_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageFromIntermediateToTargetUser_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requester_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    requester = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    BirthDate_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmotionTime_LastEmotionalUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                schema: "LAPR5",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    requester = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    connection_strength_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    relationship_strength_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    friend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    friendshipTag_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => new { x.Id, x.requester });
                    table.ForeignKey(
                        name: "FK_Friendship_Users_requester",
                        column: x => x.requester,
                        principalSchema: "LAPR5",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Friendship_requester",
                schema: "LAPR5",
                table: "Friendship",
                column: "requester");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections",
                schema: "LAPR5");

            migrationBuilder.DropTable(
                name: "Friendship",
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
