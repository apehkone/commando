namespace Commando.Core
{
    public interface ICommandHandler<in TMessage, out TResult>
    {
        TResult Execute(TMessage source);
    }
}