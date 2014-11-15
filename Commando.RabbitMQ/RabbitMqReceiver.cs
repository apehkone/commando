using System.Text;
using log4net;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Commando.RabbitMQ
{
    public class RabbitMqReceiver
    {
        readonly ILog log = LogManager.GetLogger(typeof (RabbitMqReceiver));
        readonly ConnectionFactory factory;
        readonly ReceiverConfig config;

        public RabbitMqReceiver(ConnectionFactory factory, ReceiverConfig config) {
            this.factory = factory;
            this.config = config;
        }

        public TResult Receive<TResult>() {
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(config.RoutingKey, false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(config.RoutingKey, true, consumer);
                    var ea = consumer.Queue.Dequeue();
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    log.InfoFormat("Message received: {0}", message);
                    return JsonConvert.DeserializeObject<TResult>(message);
                }
            }
        }
    }
}