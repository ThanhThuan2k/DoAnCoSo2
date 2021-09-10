using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAnCoSo2.Data.Migrations
{
    public partial class updatesysRoletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleCode",
                table: "SysRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_RoleCode",
                table: "SysRole",
                column: "RoleCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SysRole_RoleCode",
                table: "SysRole");

            migrationBuilder.DropColumn(
                name: "RoleCode",
                table: "SysRole");
        }
    }
}
