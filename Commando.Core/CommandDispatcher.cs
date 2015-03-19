using System.Threading.Tasks;
using log4net;
using Microsoft.Practices.ServiceLocation;

namespace Commando.Core
{
    public class CommandDispatcher : ICommandDispatcher
    {
        readonly IServiceLocator serviceLocator;
        readonly ILog log = LogManager.GetLogger(typeof (CommandDispatcher));

        public CommandDispatcher() {
            serviceLocator = ServiceLocator.Current;
        }

        public TResult Dispatch<TMessage, TResult>(TMessage message) {
            //if (container.IsRegistered<ICommandHandler<TMessage, TResult>>()) {
            var handler = serviceLocator.GetInstance<ICommandHandler<TMessage, TResult>>();
            log.DebugFormat("Executing command handler for type {0} {1}", typeof (TMessage), typeof (TResult));
            return handler.Execute(message);
            //}

            //log.ErrorFormat("Cannot resolve command handler for command {0} {1}", typeof (TMessage), typeof (TResult));
            //throw new CommandHandlerNotFoundException(typeof (TMessage));
        }

        public async Task<TResult> DispatchAsync<TMessage, TResult>(TMessage message) {
            return await Task.Run(() => Dispatch<TMessage, TResult>(message));
        }
    }
}