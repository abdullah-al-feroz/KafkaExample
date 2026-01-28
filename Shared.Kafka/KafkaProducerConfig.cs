using Confluent.Kafka;

namespace Shared.Kafka
{
    public class KafkaProducerConfig<Tk, Tv> : ProducerConfig
    {
        public string Topic { get; set; }
    }
}
