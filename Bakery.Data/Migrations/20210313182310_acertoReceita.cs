using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakery.Data.Migrations
{
    public partial class acertoReceita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingrediente_Produto_ProdutoFinalProduzidoId",
                table: "Ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_Ingrediente_ProdutoFinalProduzidoId",
                table: "Ingrediente");

            migrationBuilder.DropColumn(
                name: "ProdutoFinalProduzidoId",
                table: "Ingrediente");

            migrationBuilder.AddColumn<int>(
                name: "IdProdutoFinalProduzido",
                table: "Ingrediente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_IdProdutoFinalProduzido",
                table: "Ingrediente",
                column: "IdProdutoFinalProduzido");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrediente_Produto_IdProdutoFinalProduzido",
                table: "Ingrediente",
                column: "IdProdutoFinalProduzido",
                principalTable: "Produto",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingrediente_Produto_IdProdutoFinalProduzido",
                table: "Ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_Ingrediente_IdProdutoFinalProduzido",
                table: "Ingrediente");

            migrationBuilder.DropColumn(
                name: "IdProdutoFinalProduzido",
                table: "Ingrediente");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoFinalProduzidoId",
                table: "Ingrediente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_ProdutoFinalProduzidoId",
                table: "Ingrediente",
                column: "ProdutoFinalProduzidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrediente_Produto_ProdutoFinalProduzidoId",
                table: "Ingrediente",
                column: "ProdutoFinalProduzidoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
