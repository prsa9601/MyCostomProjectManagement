using MediatR;

namespace BackEnd.Shared.CoreShared.Queries
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : class?
    {

    }
}
