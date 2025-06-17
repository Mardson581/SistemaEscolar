CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Municipios" (
    "IdMunicipio" INTEGER NOT NULL CONSTRAINT "PK_Municipios" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Estado" TEXT NULL
);

CREATE TABLE "Usuario" (
    "UsuarioId" INTEGER NOT NULL CONSTRAINT "PK_Usuario" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NULL,
    "Telefone" TEXT NULL,
    "Email" TEXT NULL,
    "Senha" TEXT NULL,
    "TipoUsuario" TEXT NOT NULL
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
    CONSTRAINT "FK_Escolas_Municipios_MunicipioId" FOREIGN KEY ("MunicipioId") REFERENCES "Municipios" ("IdMunicipio"),
    CONSTRAINT "FK_Escolas_Usuario_DiretorId" FOREIGN KEY ("DiretorId") REFERENCES "Usuario" ("UsuarioId") ON DELETE SET NULL,
    CONSTRAINT "FK_Escolas_Usuario_SecretarioId" FOREIGN KEY ("SecretarioId") REFERENCES "Usuario" ("UsuarioId") ON DELETE SET NULL
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
    CONSTRAINT "FK_ProfessorTurma_Turmas_TurmasIdTurma" FOREIGN KEY ("TurmasIdTurma") REFERENCES "Turmas" ("IdTurma") ON DELETE CASCADE,
    CONSTRAINT "FK_ProfessorTurma_Usuario_ProfessoresUsuarioId" FOREIGN KEY ("ProfessoresUsuarioId") REFERENCES "Usuario" ("UsuarioId") ON DELETE CASCADE
);

INSERT INTO "Municipios" ("IdMunicipio", "Estado", "Nome")
VALUES (1, 'SP', 'São José do Rio Preto');
SELECT changes();


INSERT INTO "Usuario" ("UsuarioId", "Email", "Nome", "Senha", "Telefone", "TipoUsuario")
VALUES (1, 'dezani@email.com', 'Dezani', '123456', '1799999999', 'Diretor');
SELECT changes();

INSERT INTO "Usuario" ("UsuarioId", "Email", "Nome", "Senha", "Telefone", "TipoUsuario")
VALUES (2, 'dezani@email.com', 'Dezani', '123456', '1799999999', 'Secretario');
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
VALUES ('20250524200255_AdicionandoDiscriminator', '8.0.15');

COMMIT;

BEGIN TRANSACTION;

UPDATE "Escolas" SET "Telefone" = '(17)91234-5678'
WHERE "IdEscola" = 1;
SELECT changes();


UPDATE "Usuario" SET "Senha" = '12345678', "Telefone" = '(17)91234-5678'
WHERE "UsuarioId" = 1;
SELECT changes();


UPDATE "Usuario" SET "Senha" = '12345678', "Telefone" = '(17)91234-5678'
WHERE "UsuarioId" = 2;
SELECT changes();


CREATE TABLE "ef_temp_Usuario" (
    "UsuarioId" INTEGER NOT NULL CONSTRAINT "PK_Usuario" PRIMARY KEY AUTOINCREMENT,
    "Email" TEXT NOT NULL,
    "Nome" TEXT NOT NULL,
    "Senha" TEXT NOT NULL,
    "Telefone" TEXT NOT NULL,
    "TipoUsuario" TEXT NOT NULL
);

INSERT INTO "ef_temp_Usuario" ("UsuarioId", "Email", "Nome", "Senha", "Telefone", "TipoUsuario")
SELECT "UsuarioId", IFNULL("Email", ''), IFNULL("Nome", ''), IFNULL("Senha", ''), IFNULL("Telefone", ''), "TipoUsuario"
FROM "Usuario";

CREATE TABLE "ef_temp_Turmas" (
    "IdTurma" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY AUTOINCREMENT,
    "Ano" INTEGER NOT NULL,
    "EscolaId" INTEGER NOT NULL,
    "Nome" TEXT NOT NULL,
    CONSTRAINT "FK_Turmas_Escolas_EscolaId" FOREIGN KEY ("EscolaId") REFERENCES "Escolas" ("IdEscola") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_Turmas" ("IdTurma", "Ano", "EscolaId", "Nome")
SELECT "IdTurma", "Ano", "EscolaId", IFNULL("Nome", '')
FROM "Turmas";

CREATE TABLE "ef_temp_Municipios" (
    "IdMunicipio" INTEGER NOT NULL CONSTRAINT "PK_Municipios" PRIMARY KEY AUTOINCREMENT,
    "Estado" TEXT NOT NULL,
    "Nome" TEXT NOT NULL
);

INSERT INTO "ef_temp_Municipios" ("IdMunicipio", "Estado", "Nome")
SELECT "IdMunicipio", IFNULL("Estado", ''), IFNULL("Nome", '')
FROM "Municipios";

CREATE TABLE "ef_temp_Escolas" (
    "IdEscola" INTEGER NOT NULL CONSTRAINT "PK_Escolas" PRIMARY KEY AUTOINCREMENT,
    "CEP" TEXT NOT NULL,
    "DiretorId" INTEGER NULL,
    "Endereco" TEXT NOT NULL,
    "MunicipioId" INTEGER NOT NULL,
    "Nome" TEXT NOT NULL,
    "SecretarioId" INTEGER NULL,
    "Telefone" TEXT NOT NULL,
    CONSTRAINT "FK_Escolas_Municipios_MunicipioId" FOREIGN KEY ("MunicipioId") REFERENCES "Municipios" ("IdMunicipio"),
    CONSTRAINT "FK_Escolas_Usuario_DiretorId" FOREIGN KEY ("DiretorId") REFERENCES "Usuario" ("UsuarioId") ON DELETE SET NULL,
    CONSTRAINT "FK_Escolas_Usuario_SecretarioId" FOREIGN KEY ("SecretarioId") REFERENCES "Usuario" ("UsuarioId") ON DELETE SET NULL
);

INSERT INTO "ef_temp_Escolas" ("IdEscola", "CEP", "DiretorId", "Endereco", "MunicipioId", "Nome", "SecretarioId", "Telefone")
SELECT "IdEscola", IFNULL("CEP", ''), "DiretorId", IFNULL("Endereco", ''), "MunicipioId", IFNULL("Nome", ''), "SecretarioId", IFNULL("Telefone", '')
FROM "Escolas";

CREATE TABLE "ef_temp_Alunos" (
    "IdAluno" INTEGER NOT NULL CONSTRAINT "PK_Alunos" PRIMARY KEY AUTOINCREMENT,
    "IdTurma" INTEGER NOT NULL,
    "Matricula" TEXT NOT NULL,
    "Nascimento" TEXT NOT NULL,
    "Nome" TEXT NOT NULL,
    "RA" INTEGER NOT NULL,
    CONSTRAINT "FK_Alunos_Turmas_IdTurma" FOREIGN KEY ("IdTurma") REFERENCES "Turmas" ("IdTurma") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Alunos" ("IdAluno", "IdTurma", "Matricula", "Nascimento", "Nome", "RA")
SELECT "IdAluno", "IdTurma", "Matricula", "Nascimento", IFNULL("Nome", ''), "RA"
FROM "Alunos";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Usuario";

ALTER TABLE "ef_temp_Usuario" RENAME TO "Usuario";

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

DROP TABLE "Municipios";

ALTER TABLE "ef_temp_Municipios" RENAME TO "Municipios";

DROP TABLE "Escolas";

ALTER TABLE "ef_temp_Escolas" RENAME TO "Escolas";

DROP TABLE "Alunos";

ALTER TABLE "ef_temp_Alunos" RENAME TO "Alunos";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Turmas_EscolaId" ON "Turmas" ("EscolaId");

CREATE UNIQUE INDEX "IX_Escolas_DiretorId" ON "Escolas" ("DiretorId");

CREATE INDEX "IX_Escolas_MunicipioId" ON "Escolas" ("MunicipioId");

CREATE INDEX "IX_Escolas_SecretarioId" ON "Escolas" ("SecretarioId");

CREATE INDEX "IX_Alunos_IdTurma" ON "Alunos" ("IdTurma");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250525153145_AdicionandoValidacao', '8.0.15');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Usuario" ADD "EscolaId" INTEGER NULL;

ALTER TABLE "Usuario" ADD "Nascimento" TEXT NOT NULL DEFAULT '0001-01-01';

UPDATE "Usuario" SET "EscolaId" = 0, "Nascimento" = '0001-01-01'
WHERE "UsuarioId" = 1;
SELECT changes();


UPDATE "Usuario" SET "Nascimento" = '0001-01-01'
WHERE "UsuarioId" = 2;
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250525193649_AddNascimentoUsuario', '8.0.15');

COMMIT;

BEGIN TRANSACTION;

UPDATE "Escolas" SET "Endereco" = 'R. Miguel Landutti, 314 - Vila Diniz, São José do Rio Preto - SP, 15013-220', "Nome" = 'Escola de Ensino Integral'
WHERE "IdEscola" = 1;
SELECT changes();


UPDATE "Usuario" SET "Email" = 'carlos@email.com', "EscolaId" = NULL, "Nome" = 'Carlos', "Senha" = '$4fa2332sFs4'
WHERE "UsuarioId" = 1;
SELECT changes();


UPDATE "Usuario" SET "Email" = 'juliana@email.com', "Nome" = 'Juliana', "Senha" = '34234abadc789'
WHERE "UsuarioId" = 2;
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250527200055_ApresentacaoCRUD', '8.0.15');

COMMIT;

