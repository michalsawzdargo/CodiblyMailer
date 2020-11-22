using MediatR;

namespace CodiblyTest.Mailer.Core.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }

    public interface ICommandHandler<in T, TResult> : IRequestHandler<T, TResult>
        where T : ICommand<TResult>
    {
    }
}