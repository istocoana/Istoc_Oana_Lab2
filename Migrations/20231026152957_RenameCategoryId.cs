using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Istoc_Oana_Lab2.Migrations
{
    public partial class RenameCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Category",
                newName: "CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryID", 
                table: "Book",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_CategoryID", 
                table: "Book",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_CategoryID",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CategoryID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "ID");
        }
    }
}
