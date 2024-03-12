using ArduinoWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace ArduinoWeb.Data
{
    public class ArduinoDbContext : DbContext
    {
        public ArduinoDbContext(DbContextOptions<ArduinoDbContext> options) : base(options)
        {
        }

        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<TipoMedicao> TipoMedicoes { get; set; }
        public DbSet<RelatorioDispositivo> RelatorioDispositivos { get; set; }
        public DbSet<Medicao> Medicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispositivo>(d => { d.Property(e => e.DispositivoId).ValueGeneratedNever(); });
            modelBuilder.Entity<Localizacao>(d => { d.Property(e => e.LocalizacaoId).ValueGeneratedNever(); });
            modelBuilder.Entity<TipoMedicao>(d =>
            { d.Property(e => e.TipoMedicaoId).ValueGeneratedNever(); });
            modelBuilder.Entity<RelatorioDispositivo>(d =>
            { d.Property(e => e.RelatorioDispositivoId).ValueGeneratedNever(); });

            modelBuilder.Entity<Medicao>()
                .HasOne(d => d.RelatorioDispositivo)
                .WithMany(m => m.Medicoes)
                .OnDelete(DeleteBehavior.Restrict);


          
        }

     
    }
}
