using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAnCoSo2.Data.Migrations
{
    public partial class addcustomeradminmessagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerAdminMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsSeen = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAdminMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAdminMessage_Admin_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAdminMessage_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdminMessage_AdminID",
                table: "CustomerAdminMessage",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdminMessage_CustomerID",
                table: "CustomerAdminMessage",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAdminMessage");
        }
    }
}
