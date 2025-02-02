using CadastroDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CadastroDeClientes.Infrastructure.Persistance.Context
{
    public class CadastroDeClientesDbContext : DbContext
    {
        public CadastroDeClientesDbContext(DbContextOptions<CadastroDeClientesDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Cliente>()
                 .HasKey(x => x.Id);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Logradouros) 
                .WithOne() 
                .HasForeignKey(l => l.ClienteId); 

            modelBuilder.Entity<Logradouro>()
                .HasIndex(l => l.ClienteId)
                .IsUnique(false);
        }
    }
}
