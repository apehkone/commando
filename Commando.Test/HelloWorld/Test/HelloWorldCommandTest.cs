using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Commando.Core;
using Commando.Core.Util;
using Commando.Test.HelloWorld.Commands;
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
            BasicConfigurator.Configure();

            var builder = new ContainerBuilder();
            builder.RegisterBasicDispatcher(assembly => assembly.GetCustomAttributes(false).Cast<Attribute>().Any(attribute => attribute is AssemblyTrademarkAttribute && ((AssemblyTrademarkAttribute)attribute).Trademark == "Commando"));
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