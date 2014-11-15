namespace Commando.RabbitMQ
{
    public class RabbitMqMessage<TPayload>
    {
        public TPayload Payload { get; set; }
        public string ContentType { get { return typeof (TPayload).ToString(); } }
        public string RoutingKey { get; set; }
    }
}