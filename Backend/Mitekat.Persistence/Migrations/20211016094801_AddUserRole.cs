namespace Mitekat.Persistence.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    
    internal partial class AddUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "users",
                type: "character varying(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "users");
        }
    }
}
