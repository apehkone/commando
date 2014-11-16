namespace Commando.RabbitMQ
{
    public class RabbitMqMessage<TPayload>
    {
        public TPayload Payload { get; set; }
    }
}