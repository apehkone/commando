using System;
using EasyNetQ;
using log4net;

namespace Commando.RabbitMQ
{
    public class RabbitMqSubscriber<TResult> : IDisposable where TResult : class
    {
        readonly ILog log = LogManager.GetLogger(typeof (RabbitMqSubscriber<TResult>));
        readonly IRabbitMqConfig config;
        IBus bus;

        public RabbitMqSubscriber(IRabbitMqConfig config) {
            this.config = config;
        }

        public void Subscribe(Action<TResult> callback) {
            bus = RabbitHutch.CreateBus(string.Format("host={0}", config.Host));
            log.InfoFormat("Subscribing to bus with callback {0}", callback);
            bus.Subscribe(config.SubscriberId, callback);
        }

        public void Dispose() {
            if(bus != null) bus.Dispose();
        }
    }
}