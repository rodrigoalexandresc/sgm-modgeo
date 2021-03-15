using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModGeo.Models;

namespace ModGeo.Repositories {
    public class MapaRepository {
        private readonly ModGeoDbContext dbContext;
        public MapaRepository(ModGeoDbContext dbContext)
        {            
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Mapa>> GetAll() {
            return await dbContext.Mapas.ToListAsync();
        }

        public async Task<Mapa> GetByKey(int mapaId) {
            return await dbContext.Mapas.FirstOrDefaultAsync(w => w.Id == mapaId);
        }

        public async Task<MapaArquivo> GetMapaArquivo(int mapaId) {
            return await dbContext.MapasComArquivo.FirstOrDefaultAsync(w => w.Id == mapaId);
        }

    }
}