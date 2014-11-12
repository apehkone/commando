using Commando.Core;

namespace Commando.Test.HelloWorld
{
    public static class HelloWorldServices
    {
        public static HelloWorldCommandResult SubmitHelloWorldCommand(this ICommandDispatcher dispatcher, HelloWorldCommand command) {
            return dispatcher.Dispatch<HelloWorldCommand, HelloWorldCommandResult>(command);
        }
    }
}