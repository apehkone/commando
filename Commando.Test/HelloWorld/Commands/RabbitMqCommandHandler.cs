using Commando.Core.Attributes;
using Commando.RabbitMQ;

namespace Commando.Test.HelloWorld.Commands
{
    [CommandHandler]
    public class RabbitMqCommandHandler : RabbitMqCommandHandlerBase<HelloWorldCommand>
    {
        public RabbitMqCommandHandler(IRabbitMqConfig config) : base(config) {}
    }
}