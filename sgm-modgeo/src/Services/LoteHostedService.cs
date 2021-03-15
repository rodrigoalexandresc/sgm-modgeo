using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModGeo.Repositories;

namespace ModGeo.Services {
    public class LoteHostedService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public LoteHostedService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Integração inicial de lotes");

                var scope = scopeFactory.CreateScope();   
                var loteRepository = scope.ServiceProvider.GetRequiredService<LoteRepository>();
                var messageProducer = scope.ServiceProvider.GetRequiredService<LoteAtualizadoMessageProducer>();

                var lotesSemIntegracao = await loteRepository.ObterHistoricosSemIntegracao();
                foreach (var historico in lotesSemIntegracao)
                {
                    await messageProducer.SendMessage(new Models.LoteMessage(historico.Lote, historico));
                    await loteRepository.SetarDataIntegracao(historico, DateTime.Now);
                }

                Console.WriteLine("Integração incial concluída com sucesso!");
            }
            catch (System.Exception)
            {
                Console.WriteLine("Falha ao fazer integração inicial dos lotes");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}