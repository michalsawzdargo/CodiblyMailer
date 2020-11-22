using MediatR;

namespace CodiblyTest.Mailer.Core.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
