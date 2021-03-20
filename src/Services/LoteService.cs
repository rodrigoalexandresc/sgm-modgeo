using System;
using System.Text.Json;
using System.Threading.Tasks;
using ModGeo.Models;
using ModGeo.Repositories;

namespace ModGeo.Services {
    public class LoteService {
        private readonly LoteRepository loteRepository;
        private readonly LoteAtualizadoMessageProducer loteAtualizadoMessageProducer;

        public LoteService(LoteRepository loteRepository,
            LoteAtualizadoMessageProducer loteAtualizadoMessageProducer)
        {
            this.loteRepository = loteRepository;
            this.loteAtualizadoMessageProducer = loteAtualizadoMessageProducer;
        }

        public async Task AtualizarLote(LoteHistorico loteHistorico) {
            var lote = await loteRepository.GetByKey(loteHistorico.LoteId);

            loteHistorico.Id = 0;
            loteHistorico.DataAtualizacao = DateTime.Now;
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(loteHistorico));
            
            await loteRepository.AddHistorico(loteHistorico);        
            var loteMessage = new LoteMessage(lote, loteHistorico);
            await loteAtualizadoMessageProducer.SendMessage(loteMessage);
            await loteRepository.SetarDataIntegracao(loteHistorico, DateTime.Now);

        }
    }
}