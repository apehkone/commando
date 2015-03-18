using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Commando.Core;
using Commando.Core.Util;
using Commando.RabbitMQ;
using Commando.Test.HelloWorld.Commands;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Commando.Test.HelloWorld.Test
{
    [TestFixture]
    public class HelloWorldWithRabbitMqTest
    {
        private IComponentContext container;
        bool messageReceived;
        readonly ILog log = LogManager.GetLogger(typeof (HelloWorldWithRabbitMqTest));

        [SetUp]
        public void SetUp() {
            BasicConfigurator.Configure();

            var builder = new ContainerBuilder();
            builder.RegisterBasicDispatcher(assembly => assembly.GetCustomAttributes(false).Cast<Attribute>().Any(attribute => attribute is AssemblyTrademarkAttribute && ((AssemblyTrademarkAttribute)attribute).Trademark == "Commando"));
            builder.RegisterType<RabbitMqConfig>().As<IRabbitMqConfig>();
            container = builder.Build();
        }

        [Test]
        public void ShouldExecuteHelloWorldCommand() {
            using (var receiver = new RabbitMqSubscriber<RabbitMqMessage<HelloWorldCommand>>(new RabbitMqConfig())) {
                receiver.Subscribe(AssertReceivedMessage);

                var message = new RabbitMqMessage<HelloWorldCommand> {Payload = new HelloWorldCommand {Input = "Hola!"}};
                var dispatcher = container.Resolve<ICommandDispatcher>();
                dispatcher.SubmitRabbitMqCommand(message);

                while (!messageReceived) {
                    log.Info("Waiting for a message receiver");
                }
            }
        }

        void AssertReceivedMessage(RabbitMqMessage<HelloWorldCommand> textMessage) {
            log.InfoFormat("Received a message with payload: {0}", textMessage.Payload);
            Assert.AreEqual("Hola!", textMessage.Payload.Input);
            messageReceived = true;
        }
    }
}