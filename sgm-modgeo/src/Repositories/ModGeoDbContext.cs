using Microsoft.EntityFrameworkCore;
using ModGeo.Models;

namespace ModGeo.Repositories {
    public class ModGeoDbContext: DbContext {
        public ModGeoDbContext(DbContextOptions<ModGeoDbContext> options) : base(options)
        {
            
        }

        public DbSet<Lote> Lotes { get; set; }
        public DbSet<LoteHistorico> Historicos { get; set; }
        public DbSet<Mapa> Mapas { get; set; }
        public DbSet<MapaArquivo> MapasComArquivo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            
            new LoteMap(modelBuilder.Entity<Lote>());
            new LoteHistoricoMap(modelBuilder.Entity<LoteHistorico>());
            new MapaMap(modelBuilder.Entity<Mapa>());
            new MapaArquivoMap(modelBuilder.Entity<MapaArquivo>());
        }
    }
}