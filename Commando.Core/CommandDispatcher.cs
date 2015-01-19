using System.Threading.Tasks;
using Autofac;
using Commando.Core.Exceptions;
using log4net;

namespace Commando.Core
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext container;
        private readonly ILog log = LogManager.GetLogger(typeof (CommandDispatcher));

        public CommandDispatcher(IComponentContext container) {
            this.container = container;
        }

        public TResult Dispatch<TMessage, TResult>(TMessage message) {
            if (container.IsRegistered<ICommandHandler<TMessage, TResult>>()) {
                var handler = container.Resolve<ICommandHandler<TMessage, TResult>>();
                log.DebugFormat("Executing command handler for type {0} {1}", typeof (TMessage), typeof (TResult));
                return handler.Execute(message);
            }

            log.ErrorFormat("Cannot resolve command handler for command {0} {1}", typeof (TMessage), typeof (TResult));
            throw new CommandHandlerNotFoundException(typeof (TMessage));
        }

        public async Task<TResult> DispatchAsync<TMessage, TResult>(TMessage message) {
            return await Task.Run(() => Dispatch<TMessage, TResult>(message));
        }
    }
}