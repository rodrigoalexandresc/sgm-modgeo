using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using ModGeo.Models;

namespace ModGeo.Services {
    public class LoteAtualizadoMessageProducer {
        const string topicName = "modgeo-lote-atualizado";

        private readonly IOptions<KafkaConfig> kafkaConfig;
        public LoteAtualizadoMessageProducer(IOptions<KafkaConfig> kafkaConfig)
        {
            this.kafkaConfig = kafkaConfig;
        }

        public async Task SendMessage(LoteMessage loteMessage) {
            var historicoJson = JsonSerializer.Serialize(loteMessage);
            var config = new ProducerConfig { BootstrapServers =  kafkaConfig.Value.BootstrapServers };
            var producerBuilder = new ProducerBuilder<Null, string>(config);

            using (var producer = producerBuilder.Build()) {
                try
                {
                    await producer.ProduceAsync(topicName, new Message<Null, string> {
                        Value = historicoJson
                    });
                }
                catch (ProduceException<Null, string> e)
                {                    
                    throw e;
                }
            }
        }   
    }
}