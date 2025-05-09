﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaEscolar.Data;

#nullable disable

namespace SistemaEscolar.Migrations
{
    [DbContext(typeof(DbEscolar))]
    [Migration("20250423164627_AdicionandoProfessorTurmaNparaN")]
    partial class AdicionandoProfessorTurmaNparaN
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.15");

            modelBuilder.Entity("Diretor", b =>
                {
                    b.Property<long>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Diretores");
                });

            modelBuilder.Entity("ProfessorTurma", b =>
                {
                    b.Property<long>("ProfessoresUsuarioId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TurmasIdTurma")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProfessoresUsuarioId", "TurmasIdTurma");

                    b.HasIndex("TurmasIdTurma");

                    b.ToTable("ProfessorTurma");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Aluno", b =>
                {
                    b.Property<long>("IdAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("IdTurma")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Matricula")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<long>("RA")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdAluno");

                    b.HasIndex("IdTurma");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Escola", b =>
                {
                    b.Property<long>("IdEscola")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CEP")
                        .HasColumnType("TEXT");

                    b.Property<long>("DiretorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Endereco")
                        .HasColumnType("TEXT");

                    b.Property<string>("Municipio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<long>("SecretarioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("IdEscola");

                    b.HasIndex("DiretorId")
                        .IsUnique();

                    b.HasIndex("SecretarioId");

                    b.ToTable("Escolas");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Professor", b =>
                {
                    b.Property<long>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Secretario", b =>
                {
                    b.Property<long>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Secretarios");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Turma", b =>
                {
                    b.Property<long>("IdTurma")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Ano")
                        .HasColumnType("INTEGER");

                    b.Property<long>("EscolaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("IdTurma");

                    b.HasIndex("EscolaId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("ProfessorTurma", b =>
                {
                    b.HasOne("SistemaEscolar.Models.Professor", null)
                        .WithMany()
                        .HasForeignKey("ProfessoresUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaEscolar.Models.Turma", null)
                        .WithMany()
                        .HasForeignKey("TurmasIdTurma")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaEscolar.Models.Aluno", b =>
                {
                    b.HasOne("SistemaEscolar.Models.Turma", "Turma")
                        .WithMany("Alunos")
                        .HasForeignKey("IdTurma")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Escola", b =>
                {
                    b.HasOne("Diretor", "Diretor")
                        .WithOne("Escola")
                        .HasForeignKey("SistemaEscolar.Models.Escola", "DiretorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaEscolar.Models.Secretario", "Secretario")
                        .WithMany("Escolas")
                        .HasForeignKey("SecretarioId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Diretor");

                    b.Navigation("Secretario");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Turma", b =>
                {
                    b.HasOne("SistemaEscolar.Models.Escola", "Escola")
                        .WithMany("Turmas")
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Escola");
                });

            modelBuilder.Entity("Diretor", b =>
                {
                    b.Navigation("Escola");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Escola", b =>
                {
                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Secretario", b =>
                {
                    b.Navigation("Escolas");
                });

            modelBuilder.Entity("SistemaEscolar.Models.Turma", b =>
                {
                    b.Navigation("Alunos");
                });
#pragma warning restore 612, 618
        }
    }
}
