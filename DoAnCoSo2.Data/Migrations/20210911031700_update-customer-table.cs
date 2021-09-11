using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAnCoSo2.Data.Migrations
{
    public partial class updatecustomertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Customer");
        }
    }
}
