using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonolitoBackend.infra.Migrations
{
    /// <inheritdoc />
    public partial class FixTimezoneColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Eventos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Eventos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Eventos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Eventos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Eventos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Eventos",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Eventos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
