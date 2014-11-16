using Commando.Core;
using Commando.RabbitMQ;
using Commando.Test.HelloWorld.Commands;

namespace Commando.Test.HelloWorld
{
    public static class HelloWorldServices
    {
        public static HelloWorldCommandResult SubmitHelloWorldCommand(this ICommandDispatcher dispatcher, HelloWorldCommand command) {
            return dispatcher.Dispatch<HelloWorldCommand, HelloWorldCommandResult>(command);
        }

        public static RabbitMqCommandResult SubmitRabbitMqCommand(this ICommandDispatcher dispatcher, RabbitMqMessage<HelloWorldCommand> message) {
            return dispatcher.Dispatch<RabbitMqMessage<HelloWorldCommand>, RabbitMqCommandResult>(message);
        }
    }
}