using Autofac;
using Commando.Core;
using Commando.Core.Util;
using log4net.Config;
using NUnit.Framework;

namespace Commando.Test.HelloWorld.Test
{
    [TestFixture]
    public class HelloWorldCommandTest
    {
        private IComponentContext container;

        [SetUp]
        public void SetUp() {
            var builder = new ContainerBuilder();
            BasicConfigurator.Configure();

            builder.RegisterDispatcher();
            container = builder.Build();
        }

        [Test]
        public void ShouldExecuteHelloWorldCommand() {
            var message = new HelloWorldCommand {Input = "Hola!"};
            var dispatcher = container.Resolve<ICommandDispatcher>();
            Assert.AreEqual("Input: Hola! Output: Hello World!", dispatcher.SubmitHelloWorldCommand(message).Message);
        }
    }
}