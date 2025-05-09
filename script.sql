CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Diretores" (
    "UsuarioId" INTEGER NOT NULL CONSTRAINT "PK_Diretores" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Telefone" TEXT NULL,
    "Email" TEXT NULL,
    "Senha" TEXT NULL
);

CREATE TABLE "Professores" (
    "UsuarioId" INTEGER NOT NULL CONSTRAINT "PK_Professores" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Telefone" TEXT NULL,
    "Email" TEXT NULL,
    "Senha" TEXT NULL
);

CREATE TABLE "Secretarios" (
    "UsuarioId" INTEGER NOT NULL CONSTRAINT "PK_Secretarios" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Telefone" TEXT NULL,
    "Email" TEXT NULL,
    "Senha" TEXT NULL
);

CREATE TABLE "Escolas" (
    "IdEscola" INTEGER NOT NULL CONSTRAINT "PK_Escolas" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "CEP" TEXT NULL,
    "Municipio" TEXT NULL,
    "Telefone" TEXT NULL,
    "Endereco" TEXT NULL,
    "SecretarioId" INTEGER NOT NULL,
    "DiretorId" INTEGER NOT NULL,
    CONSTRAINT "FK_Escolas_Diretores_DiretorId" FOREIGN KEY ("DiretorId") REFERENCES "Diretores" ("UsuarioId") ON DELETE CASCADE,
    CONSTRAINT "FK_Escolas_Secretarios_SecretarioId" FOREIGN KEY ("SecretarioId") REFERENCES "Secretarios" ("UsuarioId") ON DELETE SET NULL
);

CREATE TABLE "Turmas" (
    "IdTurma" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Ano" INTEGER NOT NULL,
    "ProfessorId" INTEGER NOT NULL,
    "EscolaId" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Escolas_EscolaId" FOREIGN KEY ("EscolaId") REFERENCES "Escolas" ("IdEscola") ON DELETE RESTRICT,
    CONSTRAINT "FK_Turmas_Professores_ProfessorId" FOREIGN KEY ("ProfessorId") REFERENCES "Professores" ("UsuarioId") ON DELETE SET NULL
);

CREATE TABLE "Alunos" (
    "IdAluno" INTEGER NOT NULL CONSTRAINT "PK_Alunos" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "RA" INTEGER NOT NULL,
    "Nascimento" TEXT NOT NULL,
    "Matricula" TEXT NOT NULL,
    "TurmaIdTurma" INTEGER NULL,
    CONSTRAINT "FK_Alunos_Turmas_TurmaIdTurma" FOREIGN KEY ("TurmaIdTurma") REFERENCES "Turmas" ("IdTurma")
);

INSERT INTO "Alunos" ("IdAluno", "Matricula", "Nascimento", "Nome", "RA", "TurmaIdTurma")
VALUES (1, '2025-04-23 03:46:18.2283269', '2025-04-23 03:46:18.2283246', 'Mardson', 123123, NULL);
SELECT changes();


CREATE INDEX "IX_Alunos_TurmaIdTurma" ON "Alunos" ("TurmaIdTurma");

CREATE UNIQUE INDEX "IX_Escolas_DiretorId" ON "Escolas" ("DiretorId");

CREATE INDEX "IX_Escolas_SecretarioId" ON "Escolas" ("SecretarioId");

CREATE INDEX "IX_Turmas_EscolaId" ON "Turmas" ("EscolaId");

CREATE INDEX "IX_Turmas_ProfessorId" ON "Turmas" ("ProfessorId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250423034618_InitialCreate', '8.0.15');

COMMIT;

BEGIN TRANSACTION;

DROP INDEX "IX_Turmas_ProfessorId";

DROP INDEX "IX_Alunos_TurmaIdTurma";

DELETE FROM "Alunos"
WHERE "IdAluno" = 1;
SELECT changes();


ALTER TABLE "Alunos" ADD "IdTurma" INTEGER NOT NULL DEFAULT 0;

CREATE TABLE "ProfessorTurma" (
    "ProfessoresUsuarioId" INTEGER NOT NULL,
    "TurmasIdTurma" INTEGER NOT NULL,
    CONSTRAINT "PK_ProfessorTurma" PRIMARY KEY ("ProfessoresUsuarioId", "TurmasIdTurma"),
    CONSTRAINT "FK_ProfessorTurma_Professores_ProfessoresUsuarioId" FOREIGN KEY ("ProfessoresUsuarioId") REFERENCES "Professores" ("UsuarioId") ON DELETE CASCADE,
    CONSTRAINT "FK_ProfessorTurma_Turmas_TurmasIdTurma" FOREIGN KEY ("TurmasIdTurma") REFERENCES "Turmas" ("IdTurma") ON DELETE CASCADE
);

CREATE INDEX "IX_Alunos_IdTurma" ON "Alunos" ("IdTurma");

CREATE INDEX "IX_ProfessorTurma_TurmasIdTurma" ON "ProfessorTurma" ("TurmasIdTurma");

CREATE TABLE "ef_temp_Alunos" (
    "IdAluno" INTEGER NOT NULL CONSTRAINT "PK_Alunos" PRIMARY KEY AUTOINCREMENT,
    "IdTurma" INTEGER NOT NULL,
    "Matricula" TEXT NOT NULL,
    "Nascimento" TEXT NOT NULL,
    "Nome" TEXT NULL,
    "RA" INTEGER NOT NULL,
    CONSTRAINT "FK_Alunos_Turmas_IdTurma" FOREIGN KEY ("IdTurma") REFERENCES "Turmas" ("IdTurma") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Alunos" ("IdAluno", "IdTurma", "Matricula", "Nascimento", "Nome", "RA")
SELECT "IdAluno", "IdTurma", "Matricula", "Nascimento", "Nome", "RA"
FROM "Alunos";

CREATE TABLE "ef_temp_Turmas" (
    "IdTurma" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY AUTOINCREMENT,
    "Ano" INTEGER NOT NULL,
    "EscolaId" INTEGER NOT NULL,
    "Nome" TEXT NULL,
    CONSTRAINT "FK_Turmas_Escolas_EscolaId" FOREIGN KEY ("EscolaId") REFERENCES "Escolas" ("IdEscola") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_Turmas" ("IdTurma", "Ano", "EscolaId", "Nome")
SELECT "IdTurma", "Ano", "EscolaId", "Nome"
FROM "Turmas";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Alunos";

ALTER TABLE "ef_temp_Alunos" RENAME TO "Alunos";

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Alunos_IdTurma" ON "Alunos" ("IdTurma");

CREATE INDEX "IX_Turmas_EscolaId" ON "Turmas" ("EscolaId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250423164627_AdicionandoProfessorTurmaNparaN', '8.0.15');

COMMIT;

