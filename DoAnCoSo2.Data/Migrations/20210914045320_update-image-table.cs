using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAnCoSo2.Data.Migrations
{
    public partial class updateimagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Comment_CommentID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Product_ProductID",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CommentID",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Comment_CommentID",
                table: "Image",
                column: "CommentID",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Product_ProductID",
                table: "Image",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Comment_CommentID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Product_ProductID",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommentID",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Comment_CommentID",
                table: "Image",
                column: "CommentID",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Product_ProductID",
                table: "Image",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
