using System.Threading.Tasks;

namespace Commando.Core
{
    public interface ICommandDispatcher
    {
        TResult Dispatch<TMessage, TResult>(TMessage message);
        Task<TResult> DispatchAsync<TMessage, TResult>(TMessage message);
    }
}