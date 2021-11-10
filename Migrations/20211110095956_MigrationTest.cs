using Microsoft.EntityFrameworkCore.Migrations;

namespace DDDNetCore.Migrations
{
    public partial class MigrationTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requester_Value",
                schema: "LAPR5",
                table: "Introductions",
                newName: "Requester");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requester",
                schema: "LAPR5",
                table: "Introductions",
                newName: "Requester_Value");
        }
    }
}
