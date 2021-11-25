using Microsoft.EntityFrameworkCore.Migrations;

namespace DDDNetCore.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "requester",
                schema: "LAPR5",
                table: "Missions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "missionId",
                schema: "LAPR5",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requester",
                schema: "LAPR5",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "missionId",
                schema: "LAPR5",
                table: "Connections");
        }
    }
}
