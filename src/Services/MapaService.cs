using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ModGeo.Models;
using ModGeo.Repositories;

namespace ModGeo.Services {
    public class MapaService {
        private readonly MapaRepository mapaRepository;
        public MapaService(MapaRepository mapaRepository)
        {            
            this.mapaRepository = mapaRepository;
        }

        public async Task<IEnumerable<MapaHierarquizado>> ObterCatalogo() {
            var mapas = await mapaRepository.GetAll();
            var categorias = mapas.Select(o => o.Pai).Distinct().OrderBy(o => o).ToList();
            return categorias.Select(cat => {
                return new MapaHierarquizado {
                    Titulo = cat,
                    Filhos = mapas.Where(m => m.Pai == cat).Select(m => new MapaHierarquizado { MapaId = m.Id, Titulo = m.Titulo })
                };    
            });            
        }

        public async Task<byte[]> ObterImagem(int mapaId) {
            var mapa = await mapaRepository.GetMapaArquivo(mapaId);
            return mapa.Arquivo;
        }
    }
}