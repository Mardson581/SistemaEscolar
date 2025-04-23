using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoProfessorTurmaNparaN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_TurmaIdTurma",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Professores_ProfessorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_TurmaIdTurma",
                table: "Alunos");

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "IdAluno",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "TurmaIdTurma",
                table: "Alunos");

            migrationBuilder.AddColumn<long>(
                name: "IdTurma",
                table: "Alunos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_IdTurma",
                table: "Alunos",
                column: "IdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTurma_TurmasIdTurma",
                table: "ProfessorTurma",
                column: "TurmasIdTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_IdTurma",
                table: "Alunos",
                column: "IdTurma",
                principalTable: "Turmas",
                principalColumn: "IdTurma",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_IdTurma",
                table: "Alunos");

            migrationBuilder.DropTable(
                name: "ProfessorTurma");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_IdTurma",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "IdTurma",
                table: "Alunos");

            migrationBuilder.AddColumn<long>(
                name: "ProfessorId",
                table: "Turmas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TurmaIdTurma",
                table: "Alunos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "IdAluno", "Matricula", "Nascimento", "Nome", "RA", "TurmaIdTurma" },
                values: new object[] { 1L, new DateTime(2025, 4, 23, 3, 46, 18, 228, DateTimeKind.Local).AddTicks(3269), new DateTime(2025, 4, 23, 3, 46, 18, 228, DateTimeKind.Local).AddTicks(3246), "Mardson", 123123L, null });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaIdTurma",
                table: "Alunos",
                column: "TurmaIdTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_TurmaIdTurma",
                table: "Alunos",
                column: "TurmaIdTurma",
                principalTable: "Turmas",
                principalColumn: "IdTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Professores_ProfessorId",
                table: "Turmas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
