using System.Threading;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Commands;
using CodiblyTest.Mailer.Core.Queries;

namespace CodiblyTest.Mailer.Core.Dispatcher
{
    public interface IDispatcher
    {
        Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResponse>;

        Task Command<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
        Task<TResult> Command<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>;
    }
}