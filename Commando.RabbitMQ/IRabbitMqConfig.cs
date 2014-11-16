namespace Commando.RabbitMQ
{
    public interface IRabbitMqConfig
    {
        string Host { get; }
        string SubscriberId { get; }
    }
}