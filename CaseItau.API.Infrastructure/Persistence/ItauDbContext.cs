using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.API.Infrastructure.Persistence
{
    public class ItauDbContext : DbContext, IitauDbContext
    {
        public ItauDbContext(DbContextOptions<ItauDbContext> options) : base(options) { }
        public DbSet<Fundo> Fundos { get; set; }
        public DbSet<TipoFundo> TipoFundos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTableFundo(modelBuilder);
            ConfigureTableTipoFundo(modelBuilder);
        }

        private static void ConfigureTableFundo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fundo>(entity =>
            {
                entity.HasKey(e => e.Codigo);
                entity.Property(e => e.Codigo).HasColumnName("CODIGO");
                entity.Property(e => e.Cnpj).HasColumnName("CNPJ");
                entity.Property(e => e.Patrimonio).HasColumnName("PATRIMONIO");
                entity.Property(e => e.Nome).HasColumnName("NOME");
                entity.Property(e => e.TipoFundoCodigo).HasColumnName("CODIGO_TIPO");
                entity.HasOne(p => p.TipoFundo);
                entity.ToTable("FUNDO");
            });
        }

        private static void ConfigureTableTipoFundo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoFundo>(entity =>
            {
                entity.HasKey(e => e.Codigo);
                entity.Property(e => e.Codigo).HasColumnName("CODIGO");
                entity.Property(e => e.Nome).HasColumnName("NOME");
                entity.ToTable("TIPO_FUNDO");
            });
        }
    }
}
