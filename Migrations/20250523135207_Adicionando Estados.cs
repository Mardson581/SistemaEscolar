using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Municipios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Municipios",
                keyColumn: "IdMunicipio",
                keyValue: 1L,
                column: "Estado",
                value: "SP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Municipios");
        }
    }
}
