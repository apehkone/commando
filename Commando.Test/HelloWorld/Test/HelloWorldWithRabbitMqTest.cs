using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Commando.Autofac;
using Commando.Core;
using Commando.RabbitMQ;
using Commando.Test.HelloWorld.Commands;
using log4net;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;

namespace Commando.Test.HelloWorld.Test
{
    [TestFixture]
    public class HelloWorldWithRabbitMqTest
    {
        [SetUp]
        public void SetUp() {
            BasicConfigurator.Configure();

            var builder = new ContainerBuilder();
            builder.RegisterBasicDispatcher(assembly => assembly.GetCustomAttributes(false).Cast<Attribute>().Any(attribute => attribute is AssemblyTrademarkAttribute && ((AssemblyTrademarkAttribute) attribute).Trademark == "Commando"));
            builder.RegisterType<RabbitMqConfig>().As<IRabbitMqConfig>();
            container = builder.Build();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        IComponentContext container;
        bool messageReceived;
        readonly ILog log = LogManager.GetLogger(typeof (HelloWorldWithRabbitMqTest));

        void AssertReceivedMessage(RabbitMqMessage<HelloWorldCommand> textMessage) {
            log.InfoFormat("Received a message with payload: {0}", textMessage.Payload);
            Assert.AreEqual("Hola!", textMessage.Payload.Input);
            messageReceived = true;
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
    }
}