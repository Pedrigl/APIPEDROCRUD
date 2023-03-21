using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIPEDROCRUD.Models;

public partial class LoginContext : DbContext
{
    
    
    public LoginContext(IConfiguration config, DbContextOptions<LoginContext> options)
        : base(options) 
    {
        
    }
    

    public virtual DbSet<Login> Logins { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("Usuarios");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name");
            entity.Property(e => e.User)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("User");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
