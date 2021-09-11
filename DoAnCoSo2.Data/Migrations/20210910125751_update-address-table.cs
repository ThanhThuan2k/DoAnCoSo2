using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAnCoSo2.Data.Migrations
{
    public partial class updateaddresstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverAddress",
                table: "Address",
                newName: "Receiver");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "Address",
                newName: "ReceiverAddress");
        }
    }
}
