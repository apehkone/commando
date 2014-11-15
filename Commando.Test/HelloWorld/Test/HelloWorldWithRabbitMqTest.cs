using Autofac;
using Commando.Core;
using Commando.Core.Util;
using Commando.RabbitMQ;
using log4net.Config;
using NUnit.Framework;

namespace Commando.Test.HelloWorld.Test
{
    [TestFixture]
    public class HelloWorldWithRabbitMqTest
    {
        private IComponentContext container;

        [SetUp]
        public void SetUp() {
            BasicConfigurator.Configure();
            var builder = new ContainerBuilder();
            builder.RegisterBasicDispatcher();
            builder.RegisterMessageHandlers(AssemblyUtil.LoadAllKnownAssemblies());
            builder.RegisterRabbitMq();
            builder.RegisterType<RabbitMqReceiver>();
            builder.RegisterInstance(new ReceiverConfig {RoutingKey = "hello"});
            container = builder.Build();
        }

        [Test]
        public void ShouldExecuteHelloWorldCommand() {
            var message = new RabbitMqMessage<HelloWorldCommand> {Payload = new HelloWorldCommand {Input = "Hola!"}, RoutingKey = "hello"};
            var dispatcher = container.Resolve<ICommandDispatcher>();
            dispatcher.SubmitRabbitMqCommand(message);

            var receiver = container.Resolve<RabbitMqReceiver>();
            Assert.AreEqual("Hola!", receiver.Receive<RabbitMqMessage<HelloWorldCommand>>().Payload.Input);
        }
    }
}