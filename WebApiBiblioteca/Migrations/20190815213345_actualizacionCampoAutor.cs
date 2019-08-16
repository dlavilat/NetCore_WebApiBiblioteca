using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiBiblioteca.Migrations
{
    public partial class actualizacionCampoAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Autores",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Autores",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
