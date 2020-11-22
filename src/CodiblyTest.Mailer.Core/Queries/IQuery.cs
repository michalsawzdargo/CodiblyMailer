using MediatR;

namespace CodiblyTest.Mailer.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
