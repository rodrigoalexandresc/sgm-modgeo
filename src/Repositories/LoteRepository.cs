using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModGeo.Models;

namespace ModGeo.Repositories {
    public class LoteRepository {
        private readonly ModGeoDbContext dbContext;
        public LoteRepository(ModGeoDbContext dbContext)
        {            
            this.dbContext = dbContext;
        }

        public async Task Add(Lote lote) {
            dbContext.Lotes.Add(lote);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Lote lote) {
            var reg = dbContext.Update(lote);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Lote> GetByKey(int id) {
            return await dbContext.Lotes.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IList<Lote>> GetByLoteQuery(LoteQuery loteQuery) {
            var data = dbContext.Lotes.Where(w => loteQuery.InscricaoImovel == w.InscricaoImovel 
                || w.Endereco.Contains(loteQuery.Endereco) 
                || w.GeoId == loteQuery.GeoId);
            return await data.ToListAsync();
        }

        public async Task<IList<LoteHistorico>> GetHistoricos(int loteId) {
            return await dbContext.Historicos.Where(h => h.LoteId == loteId).ToListAsync();
        }

        public async Task<LoteHistorico> GetUlitmoLoteHistorico(int loteId) {
            //var lote = await dbContext.Lotes.FirstOrDefaultAsync(l => l.Id == loteId);
            var historicos = await dbContext.Historicos.Where(o => o.LoteId == loteId).ToListAsync();
            return historicos.OrderByDescending(o => o.DataAtualizacao).FirstOrDefault();
        }

        public async Task AddHistorico(LoteHistorico loteHistorico) {
            dbContext.Historicos.Add(loteHistorico);
            await dbContext.SaveChangesAsync();
        }

        public async Task SetarDataIntegracao(LoteHistorico loteHistorico, DateTime dataIntegracao) { 
            loteHistorico.DataIntegracao = dataIntegracao;   
            dbContext.Historicos.Update(loteHistorico);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoteHistorico>> ObterHistoricosSemIntegracao() {
            return await dbContext.Historicos
                .Include(i => i.Lote)
                .Where(h => !h.DataIntegracao.HasValue).ToListAsync();
        }
    }
}