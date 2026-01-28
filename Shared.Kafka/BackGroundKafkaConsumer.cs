using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace Shared.Kafka
{
    public class BackGroundKafkaConsumer<Tk, Tv> : BackgroundService
    {
        private readonly KafkaConsumerConfig<Tk, Tv> _config;
        private IKafkaHandler<Tk, Tv> _handler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackGroundKafkaConsumer(IOptions<KafkaConsumerConfig<Tk, Tv>> config,
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _config = config.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _handler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<Tk, Tv>>();

                var builder = new ConsumerBuilder<Tk, Tv>(_config)
                    .SetValueDeserializer(new KafkaDeserializer<Tv>());

                using (IConsumer<Tk, Tv> consumer = builder.Build())
                {
                    consumer.Subscribe(_config.Topic);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var result = consumer.Consume(TimeSpan.FromMilliseconds(1000));

                        if (result != null)
                        {
                            await _handler.HandleAsync(result.Message.Key, result.Message.Value);
                            consumer.Commit(result);
                            consumer.StoreOffset(result);
                        }
                    }
                }
            }
        }
    }
}
