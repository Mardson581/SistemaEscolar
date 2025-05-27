using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolar.Migrations
{
    /// <inheritdoc />
    public partial class ApresentacaoCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Escolas",
                keyColumn: "IdEscola",
                keyValue: 1L,
                columns: new[] { "Endereco", "Nome" },
                values: new object[] { "R. Miguel Landutti, 314 - Vila Diniz, São José do Rio Preto - SP, 15013-220", "Escola de Ensino Integral" });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 1L,
                columns: new[] { "Email", "EscolaId", "Nome", "Senha" },
                values: new object[] { "carlos@email.com", null, "Carlos", "$4fa2332sFs4" });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 2L,
                columns: new[] { "Email", "Nome", "Senha" },
                values: new object[] { "juliana@email.com", "Juliana", "34234abadc789" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Escolas",
                keyColumn: "IdEscola",
                keyValue: 1L,
                columns: new[] { "Endereco", "Nome" },
                values: new object[] { "Nada", "Escola Teste" });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 1L,
                columns: new[] { "Email", "EscolaId", "Nome", "Senha" },
                values: new object[] { "dezani@email.com", 0L, "Dezani", "12345678" });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 2L,
                columns: new[] { "Email", "Nome", "Senha" },
                values: new object[] { "dezani@email.com", "Dezani", "12345678" });
        }
    }
}
