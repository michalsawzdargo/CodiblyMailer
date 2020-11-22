using System.Threading;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Commands;
using CodiblyTest.Mailer.Core.Queries;
using MediatR;

namespace CodiblyTest.Mailer.Core.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IMediator _mediator;

        public Dispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResponse>
            => await _mediator.Send(query, cancellationToken);

        public async Task Command<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
            => await _mediator.Send(command, cancellationToken);

        public async Task<TResult> Command<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>
            => await _mediator.Send(command, cancellationToken);
    }
}
