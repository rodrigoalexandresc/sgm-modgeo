using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModGeo.Models;

namespace ModGeo.Repositories {
    public class MapaMap {
        public MapaMap(EntityTypeBuilder<Mapa> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("mapa");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Pai).HasColumnName("pai");
            entityBuilder.Property(x => x.Titulo).HasColumnName("titulo");
            entityBuilder.HasOne(o => o.MapaArquivo).WithOne().HasForeignKey<MapaArquivo>(o => o.Id);
        }        
    }        

    public class MapaArquivoMap {
        public MapaArquivoMap(EntityTypeBuilder<MapaArquivo> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("mapa");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Pai).HasColumnName("pai");
            entityBuilder.Property(x => x.Titulo).HasColumnName("titulo");
            entityBuilder.Property(x => x.Arquivo).HasColumnName("arquivo");
        }        
    }            
}