using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModGeo.Models;
using ModGeo.Repositories;
using ModGeo.Services;

namespace ModGeo.Controllers {

    [ApiController]
    [Route("[controller]")]    
    public class LoteController: ControllerBase 
    {
        private readonly LoteRepository loteRepository;
        private readonly LoteService loteService;
        public LoteController(LoteRepository loteRepository, LoteService loteService)
        {
            this.loteRepository = loteRepository;
            this.loteService = loteService;
        }

        [HttpPost("consulta")]
        public async Task<IActionResult> Consulta(LoteQuery consulta) {

            if (!consulta.IsValid()) {
                return BadRequest(consulta.Errors());
            }
            var retorno = await loteRepository.GetByLoteQuery(consulta);
            return Ok(retorno);
        } 

        [HttpGet("ultimohistorico/{loteId}")]
        public async Task<IActionResult> ObterUltimoHistorico(int loteId) {
            if (loteId <= 0) return BadRequest("Id do lote obrigaório");

            var retorno = await loteRepository.GetUlitmoLoteHistorico(loteId);
            return Ok(retorno);
        }

        [HttpPost("historico")]
        public async Task<IActionResult> SalvarHistorico(LoteHistorico loteHistorico) {
            await loteService.AtualizarLote(loteHistorico);
            return Ok();
        }
    }
}