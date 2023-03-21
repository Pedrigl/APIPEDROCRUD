using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIPEDROCRUD.Models.Cadastro_1;

public partial class BdcadastroContext : DbContext
{

    public BdcadastroContext(DbContextOptions<BdcadastroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("CLIENTE");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BAIRRO");
            entity.Property(e => e.Cep)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("CEP");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CIDADE");
            entity.Property(e => e.Endereco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ENDERECO");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOME");
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONE");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("UF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
