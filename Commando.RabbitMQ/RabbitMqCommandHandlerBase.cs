using Commando.Core;
using EasyNetQ;

namespace Commando.RabbitMQ
{
    public class RabbitMqCommandHandlerBase<TPayload> : ICommandHandler<RabbitMqMessage<TPayload>, RabbitMqCommandResult>
    {
        readonly IRabbitMqConfig config;

        public RabbitMqCommandHandlerBase(IRabbitMqConfig config) {
            this.config = config;
        }

        public RabbitMqCommandResult Execute(RabbitMqMessage<TPayload> source) {
            using (var bus = RabbitHutch.CreateBus(string.Format("host={0}", config.Host))) {
                bus.Publish(source);
            }
            return new RabbitMqCommandResult {Success = true};
        }
    }
}