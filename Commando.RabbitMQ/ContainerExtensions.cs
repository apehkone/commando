using Autofac;
using RabbitMQ.Client;

namespace Commando.RabbitMQ
{
    public static class ContainerExtensions
    {
        public static void RegisterRabbitMq(this ContainerBuilder builder) {
            builder.RegisterInstance(new ConnectionFactory {HostName = RabbitMqConfig.Host}).As<ConnectionFactory>();
        }
    }
}