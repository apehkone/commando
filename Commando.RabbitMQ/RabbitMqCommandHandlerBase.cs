using System.IO;
using System.Text;
using Commando.Core;
using log4net;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Commando.RabbitMQ
{
    public class RabbitMqCommandHandlerBase<TPayload> : ICommandHandler<RabbitMqMessage<TPayload>, RabbitMqCommandResult>
    {
        private readonly ILog log = LogManager.GetLogger(typeof (RabbitMqCommandHandlerBase<TPayload>));
        private readonly ConnectionFactory factory;

        public RabbitMqCommandHandlerBase(ConnectionFactory factory) {
            this.factory = factory;
        }

        public RabbitMqCommandResult Execute(RabbitMqMessage<TPayload> source) {
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(source.RoutingKey, false, false, false, null);
                    channel.BasicPublish(string.Empty, source.RoutingKey, null, SerializeMessage(source));
                    log.DebugFormat("Published message {0} to route {1}", source.Payload, source.RoutingKey);
                }
            }
            return new RabbitMqCommandResult {Success = true};
        }

        private static byte[] SerializeMessage(RabbitMqMessage<TPayload> source) {
            var serializer = JsonSerializer.CreateDefault();
            var writer = new StringWriter();
            serializer.Serialize(writer, source);
            var body = Encoding.UTF8.GetBytes(writer.ToString());
            return body;
        }
    }
}