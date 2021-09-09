using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAnCoSo2.Data.Migrations
{
    public partial class addcolumnerrorcodeinsyserrortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ErrorCode",
                table: "SysError",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SysError_ErrorCode",
                table: "SysError",
                column: "ErrorCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SysError_ErrorCode",
                table: "SysError");

            migrationBuilder.DropColumn(
                name: "ErrorCode",
                table: "SysError");
        }
    }
}
