using Commando.Core;
using Commando.Core.Attributes;

namespace Commando.Test.HelloWorld.Commands
{
    [CommandHandler]
    public class HelloWorldCommandHandler : ICommandHandler<HelloWorldCommand, HelloWorldCommandResult>
    {
        public HelloWorldCommandResult Execute(HelloWorldCommand source) {
            return new HelloWorldCommandResult {Message = string.Format("Input: {0} Output: {1}", source.Input, "Hello World!")};
        }
    }
}