Build started...
Build succeeded.
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


