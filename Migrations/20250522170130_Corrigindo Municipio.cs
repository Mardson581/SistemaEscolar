using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolar.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoMunicipio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diretores",
                columns: table => new
                {
                    UsuarioId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diretores", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    IdMunicipio = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.IdMunicipio);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    UsuarioId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Secretarios",
                columns: table => new
                {
                    UsuarioId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    IdEscola = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CEP = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", nullable: true),
                    SecretarioId = table.Column<long>(type: "INTEGER", nullable: false),
                    DiretorId = table.Column<long>(type: "INTEGER", nullable: false),
                    MunicipioId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.IdEscola);
                    table.ForeignKey(
                        name: "FK_Escolas_Diretores_DiretorId",
                        column: x => x.DiretorId,
                        principalTable: "Diretores",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escolas_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "IdMunicipio");
                    table.ForeignKey(
                        name: "FK_Escolas_Secretarios_SecretarioId",
                        column: x => x.SecretarioId,
                        principalTable: "Secretarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    IdTurma = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Ano = table.Column<short>(type: "INTEGER", nullable: false),
                    EscolaId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.IdTurma);
                    table.ForeignKey(
                        name: "FK_Turmas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "IdEscola",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    IdAluno = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    RA = table.Column<long>(type: "INTEGER", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Matricula = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdTurma = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.IdAluno);
                    table.ForeignKey(
                        name: "FK_Alunos_Turmas_IdTurma",
                        column: x => x.IdTurma,
                        principalTable: "Turmas",
                        principalColumn: "IdTurma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorTurma",
                columns: table => new
                {
                    ProfessoresUsuarioId = table.Column<long>(type: "INTEGER", nullable: false),
                    TurmasIdTurma = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorTurma", x => new { x.ProfessoresUsuarioId, x.TurmasIdTurma });
                    table.ForeignKey(
                        name: "FK_ProfessorTurma_Professores_ProfessoresUsuarioId",
                        column: x => x.ProfessoresUsuarioId,
                        principalTable: "Professores",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorTurma_Turmas_TurmasIdTurma",
                        column: x => x.TurmasIdTurma,
                        principalTable: "Turmas",
                        principalColumn: "IdTurma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diretores",
                columns: new[] { "UsuarioId", "Email", "Nome", "Senha", "Telefone" },
                values: new object[] { 1L, "dezani@email.com", "Dezani", "123456", "1799999999" });

            migrationBuilder.InsertData(
                table: "Municipios",
                columns: new[] { "IdMunicipio", "Nome" },
                values: new object[] { 1L, "São José do Rio Preto" });

            migrationBuilder.InsertData(
                table: "Secretarios",
                columns: new[] { "UsuarioId", "Email", "Nome", "Senha", "Telefone" },
                values: new object[] { 1L, "dezani@email.com", "Dezani", "123456", "1799999999" });

            migrationBuilder.InsertData(
                table: "Escolas",
                columns: new[] { "IdEscola", "CEP", "DiretorId", "Endereco", "MunicipioId", "Nome", "SecretarioId", "Telefone" },
                values: new object[] { 1L, "12315808", 1L, "Nada", 1L, "Escola Teste", 1L, null });

            migrationBuilder.InsertData(
                table: "Turmas",
                columns: new[] { "IdTurma", "Ano", "EscolaId", "Nome" },
                values: new object[] { 1L, (short)2025, 1L, "1º Ano" });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_IdTurma",
                table: "Alunos",
                column: "IdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_DiretorId",
                table: "Escolas",
                column: "DiretorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_MunicipioId",
                table: "Escolas",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_SecretarioId",
                table: "Escolas",
                column: "SecretarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTurma_TurmasIdTurma",
                table: "ProfessorTurma",
                column: "TurmasIdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_EscolaId",
                table: "Turmas",
                column: "EscolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "ProfessorTurma");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Escolas");

            migrationBuilder.DropTable(
                name: "Diretores");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Secretarios");
        }
    }
}
