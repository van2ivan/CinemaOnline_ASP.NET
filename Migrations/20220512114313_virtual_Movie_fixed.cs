using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaOnline.Migrations
{
    public partial class virtual_Movie_fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_MovieId",
                table: "ShoppingCartItems",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Movies_MovieId",
                table: "ShoppingCartItems",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Movies_MovieId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_MovieId",
                table: "ShoppingCartItems");
        }
    }
}
