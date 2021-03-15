using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModGeo.Models;
using ModGeo.Services;

namespace ModGeo.Controllers {
    [ApiController]
    [Route("[controller]")]        
    public class MapaController : ControllerBase {
        private readonly MapaService mapaService;        

        public MapaController(MapaService mapaService)
        {
            this.mapaService = mapaService;
        }

        [HttpGet("catalogo")]
        public async Task<IEnumerable<MapaHierarquizado>> ObterCatalogo() {
            return await mapaService.ObterCatalogo();
        }

        [HttpGet("imagem/{mapaId}")]
        public async Task<byte[]> ObterImagem(int mapaId) {
            return await mapaService.ObterImagem(mapaId);
        }
    }
}