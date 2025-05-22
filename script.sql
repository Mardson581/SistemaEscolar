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

CREATE TABLE "Municipios" (
    "IdMunicipio" INTEGER NOT NULL CONSTRAINT "PK_Municipios" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL
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
    "Telefone" TEXT NULL,
    "Endereco" TEXT NULL,
    "SecretarioId" INTEGER NOT NULL,
    "DiretorId" INTEGER NOT NULL,
    "MunicipioId" INTEGER NOT NULL,
    CONSTRAINT "FK_Escolas_Diretores_DiretorId" FOREIGN KEY ("DiretorId") REFERENCES "Diretores" ("UsuarioId") ON DELETE CASCADE,
    CONSTRAINT "FK_Escolas_Municipios_MunicipioId" FOREIGN KEY ("MunicipioId") REFERENCES "Municipios" ("IdMunicipio"),
    CONSTRAINT "FK_Escolas_Secretarios_SecretarioId" FOREIGN KEY ("SecretarioId") REFERENCES "Secretarios" ("UsuarioId") ON DELETE SET NULL
);

CREATE TABLE "Turmas" (
    "IdTurma" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Ano" INTEGER NOT NULL,
    "EscolaId" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Escolas_EscolaId" FOREIGN KEY ("EscolaId") REFERENCES "Escolas" ("IdEscola") ON DELETE RESTRICT
);

CREATE TABLE "Alunos" (
    "IdAluno" INTEGER NOT NULL CONSTRAINT "PK_Alunos" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "RA" INTEGER NOT NULL,
    "Nascimento" TEXT NOT NULL,
    "Matricula" TEXT NOT NULL,
    "IdTurma" INTEGER NOT NULL,
    CONSTRAINT "FK_Alunos_Turmas_IdTurma" FOREIGN KEY ("IdTurma") REFERENCES "Turmas" ("IdTurma") ON DELETE CASCADE
);

CREATE TABLE "ProfessorTurma" (
    "ProfessoresUsuarioId" INTEGER NOT NULL,
    "TurmasIdTurma" INTEGER NOT NULL,
    CONSTRAINT "PK_ProfessorTurma" PRIMARY KEY ("ProfessoresUsuarioId", "TurmasIdTurma"),
    CONSTRAINT "FK_ProfessorTurma_Professores_ProfessoresUsuarioId" FOREIGN KEY ("ProfessoresUsuarioId") REFERENCES "Professores" ("UsuarioId") ON DELETE CASCADE,
    CONSTRAINT "FK_ProfessorTurma_Turmas_TurmasIdTurma" FOREIGN KEY ("TurmasIdTurma") REFERENCES "Turmas" ("IdTurma") ON DELETE CASCADE
);

INSERT INTO "Diretores" ("UsuarioId", "Email", "Nome", "Senha", "Telefone")
VALUES (1, 'dezani@email.com', 'Dezani', '123456', '1799999999');
SELECT changes();


INSERT INTO "Municipios" ("IdMunicipio", "Nome")
VALUES (1, 'São José do Rio Preto');
SELECT changes();


INSERT INTO "Secretarios" ("UsuarioId", "Email", "Nome", "Senha", "Telefone")
VALUES (1, 'dezani@email.com', 'Dezani', '123456', '1799999999');
SELECT changes();


INSERT INTO "Escolas" ("IdEscola", "CEP", "DiretorId", "Endereco", "MunicipioId", "Nome", "SecretarioId", "Telefone")
VALUES (1, '12315808', 1, 'Nada', 1, 'Escola Teste', 1, NULL);
SELECT changes();


INSERT INTO "Turmas" ("IdTurma", "Ano", "EscolaId", "Nome")
VALUES (1, 2025, 1, '1º Ano');
SELECT changes();


CREATE INDEX "IX_Alunos_IdTurma" ON "Alunos" ("IdTurma");

CREATE UNIQUE INDEX "IX_Escolas_DiretorId" ON "Escolas" ("DiretorId");

CREATE INDEX "IX_Escolas_MunicipioId" ON "Escolas" ("MunicipioId");

CREATE INDEX "IX_Escolas_SecretarioId" ON "Escolas" ("SecretarioId");

CREATE INDEX "IX_ProfessorTurma_TurmasIdTurma" ON "ProfessorTurma" ("TurmasIdTurma");

CREATE INDEX "IX_Turmas_EscolaId" ON "Turmas" ("EscolaId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250522170130_Corrigindo Municipio', '8.0.15');

COMMIT;

