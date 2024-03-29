﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiBiblioteca.Migrations
{
    public partial class camposNuevos_Autor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Autores",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "Autores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Autores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Autores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Autores");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Autores",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
