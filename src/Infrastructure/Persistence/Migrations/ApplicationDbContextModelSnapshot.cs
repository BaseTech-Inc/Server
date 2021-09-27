﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Domain.Entities.Alerta", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<bool>("Atividade")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("DistritoId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PontoId")
                        .HasColumnType("varchar(767)");

                    b.Property<DateTime>("TempoFinal")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TempoInicio")
                        .HasColumnType("datetime");

                    b.Property<bool>("Transitividade")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("DistritoId");

                    b.HasIndex("PontoId");

                    b.ToTable("Alerta");
                });

            modelBuilder.Entity("Domain.Entities.Cidade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("EstadoId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Cidade");
                });

            modelBuilder.Entity("Domain.Entities.Distrito", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("CidadeId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.ToTable("Distrito");
                });

            modelBuilder.Entity("Domain.Entities.Estado", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<string>("PaisId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Sigla")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("Domain.Entities.HistoricoPrevisao", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("DistritoId")
                        .HasColumnType("varchar(767)");

                    b.Property<double>("SensibilidadeTermica")
                        .HasColumnType("double");

                    b.Property<double>("TemperaturaMaxima")
                        .HasColumnType("double");

                    b.Property<double>("TemperaturaMinima")
                        .HasColumnType("double");

                    b.Property<DateTime>("Tempo")
                        .HasColumnType("datetime");

                    b.Property<double>("Umidade")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("DistritoId");

                    b.ToTable("HistoricoPrevisao");
                });

            modelBuilder.Entity("Domain.Entities.HistoricoUsuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<double>("DistanciaPercurso")
                        .HasColumnType("double");

                    b.Property<string>("Rota")
                        .HasColumnType("text");

                    b.Property<DateTime>("TempoChegada")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TempoPartida")
                        .HasColumnType("datetime");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("HistoricoUsuario");
                });

            modelBuilder.Entity("Domain.Entities.Marcadores", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<string>("PontoId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("PontoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Marcadores");
                });

            modelBuilder.Entity("Domain.Entities.Pais", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<string>("Sigla")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("Domain.Entities.Poligono", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.ToTable("Poligono");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoCidade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("CidadeId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PoligonoId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("PoligonoId");

                    b.ToTable("PoligonoCidade");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoDistrito", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("DistritoId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PoligonoId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("DistritoId");

                    b.HasIndex("PoligonoId");

                    b.ToTable("PoligonoDistrito");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoEstado", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("EstadoId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PoligonoId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("PoligonoId");

                    b.ToTable("PoligonoEstado");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoPais", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PaisId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PoligonoId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.HasIndex("PoligonoId");

                    b.ToTable("PoligonoPais");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoPonto", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PoligonoId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PontoId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("PoligonoId");

                    b.HasIndex("PontoId");

                    b.ToTable("PoligonoPonto");
                });

            modelBuilder.Entity("Domain.Entities.Ponto", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Ponto");
                });

            modelBuilder.Entity("Domain.Entities.PontoRisco", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("PontoId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("PontoId");

                    b.ToTable("PontoRisco");
                });

            modelBuilder.Entity("Domain.Entities.TipoUsuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Descricao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TipoUsuario");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ApplicationUserID")
                        .HasColumnType("varchar(85)");

                    b.Property<string>("ContaBancaria")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("TipoUsuarioId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("TipoUsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Infrastructure.Identity.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(85)");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("ExpiryOn")
                        .HasColumnType("datetime");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("text");

                    b.Property<DateTime>("RevokedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("ProviderKey")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("LoginProvider")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(85)
                        .HasColumnType("varchar(85)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Entities.Alerta", b =>
                {
                    b.HasOne("Domain.Entities.Distrito", "Distrito")
                        .WithMany()
                        .HasForeignKey("DistritoId");

                    b.HasOne("Domain.Entities.Ponto", "Ponto")
                        .WithMany()
                        .HasForeignKey("PontoId");

                    b.Navigation("Distrito");

                    b.Navigation("Ponto");
                });

            modelBuilder.Entity("Domain.Entities.Cidade", b =>
                {
                    b.HasOne("Domain.Entities.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("Domain.Entities.Distrito", b =>
                {
                    b.HasOne("Domain.Entities.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId");

                    b.Navigation("Cidade");
                });

            modelBuilder.Entity("Domain.Entities.Estado", b =>
                {
                    b.HasOne("Domain.Entities.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId");

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("Domain.Entities.HistoricoPrevisao", b =>
                {
                    b.HasOne("Domain.Entities.Distrito", "Distrito")
                        .WithMany()
                        .HasForeignKey("DistritoId");

                    b.Navigation("Distrito");
                });

            modelBuilder.Entity("Domain.Entities.HistoricoUsuario", b =>
                {
                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.Marcadores", b =>
                {
                    b.HasOne("Domain.Entities.Ponto", "Ponto")
                        .WithMany()
                        .HasForeignKey("PontoId");

                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Ponto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoCidade", b =>
                {
                    b.HasOne("Domain.Entities.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId");

                    b.HasOne("Domain.Entities.Poligono", "Poligono")
                        .WithMany()
                        .HasForeignKey("PoligonoId");

                    b.Navigation("Cidade");

                    b.Navigation("Poligono");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoDistrito", b =>
                {
                    b.HasOne("Domain.Entities.Distrito", "Distrito")
                        .WithMany()
                        .HasForeignKey("DistritoId");

                    b.HasOne("Domain.Entities.Poligono", "Poligono")
                        .WithMany()
                        .HasForeignKey("PoligonoId");

                    b.Navigation("Distrito");

                    b.Navigation("Poligono");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoEstado", b =>
                {
                    b.HasOne("Domain.Entities.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");

                    b.HasOne("Domain.Entities.Poligono", "Poligono")
                        .WithMany()
                        .HasForeignKey("PoligonoId");

                    b.Navigation("Estado");

                    b.Navigation("Poligono");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoPais", b =>
                {
                    b.HasOne("Domain.Entities.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId");

                    b.HasOne("Domain.Entities.Poligono", "Poligono")
                        .WithMany()
                        .HasForeignKey("PoligonoId");

                    b.Navigation("Pais");

                    b.Navigation("Poligono");
                });

            modelBuilder.Entity("Domain.Entities.PoligonoPonto", b =>
                {
                    b.HasOne("Domain.Entities.Poligono", "Poligono")
                        .WithMany()
                        .HasForeignKey("PoligonoId");

                    b.HasOne("Domain.Entities.Ponto", "Ponto")
                        .WithMany()
                        .HasForeignKey("PontoId");

                    b.Navigation("Poligono");

                    b.Navigation("Ponto");
                });

            modelBuilder.Entity("Domain.Entities.PontoRisco", b =>
                {
                    b.HasOne("Domain.Entities.Ponto", "Ponto")
                        .WithMany()
                        .HasForeignKey("PontoId");

                    b.Navigation("Ponto");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany("Usuario")
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("Domain.Entities.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("TipoUsuarioId");

                    b.Navigation("TipoUsuario");
                });

            modelBuilder.Entity("Infrastructure.Identity.RefreshToken", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
