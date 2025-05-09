using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolar.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diretores",
                columns: new[] { "UsuarioId", "Email", "Nome", "Senha", "Telefone" },
                values: new object[] { 1L, "dezani@email.com", "Dezani", "123456", "1799999999" });

            migrationBuilder.InsertData(
                table: "Secretarios",
                columns: new[] { "UsuarioId", "Email", "Nome", "Senha", "Telefone" },
                values: new object[] { 1L, "dezani@email.com", "Dezani", "123456", "1799999999" });

            migrationBuilder.InsertData(
                table: "Escolas",
                columns: new[] { "IdEscola", "CEP", "DiretorId", "Endereco", "Municipio", "Nome", "SecretarioId", "Telefone" },
                values: new object[] { 1L, "12315808", 1L, "Nada", "Teste", "Escola Teste", 1L, null });

            migrationBuilder.InsertData(
                table: "Turmas",
                columns: new[] { "IdTurma", "Ano", "EscolaId", "Nome" },
                values: new object[] { 1L, (short)2025, 1L, "1º Ano" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Turmas",
                keyColumn: "IdTurma",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Escolas",
                keyColumn: "IdEscola",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Diretores",
                keyColumn: "UsuarioId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Secretarios",
                keyColumn: "UsuarioId",
                keyValue: 1L);
        }
    }
}
