namespace Commando.RabbitMQ
{
    public class RabbitMqConfig : IRabbitMqConfig
    {
        public string Host { get { return "localhost"; } }
        public string SubscriberId { get { return "test"; } }
    }
}