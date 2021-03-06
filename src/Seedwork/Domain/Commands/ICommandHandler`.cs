using MediatR;

namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : class, ICommand<TResult>
        where TResult : class, ICommandResult
    {
    }
}
