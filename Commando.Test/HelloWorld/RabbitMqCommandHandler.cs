using Commando.Core.Attributes;
using Commando.RabbitMQ;
using RabbitMQ.Client;

namespace Commando.Test.HelloWorld
{
    [CommandHandler]
    public class RabbitMqCommandHandler : RabbitMqCommandHandlerBase<HelloWorldCommand>
    {
        public RabbitMqCommandHandler(ConnectionFactory factory) : base(factory) {}
    }
}