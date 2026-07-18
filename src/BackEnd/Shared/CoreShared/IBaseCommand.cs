using MediatR;
using System.ClientModel.Primitives;

namespace BackEnd.Shared.CoreShared
{
    public interface IBaseCommand : IRequest<OperationResult>
    {
    }

    public interface IBaseCommand<TData> : IRequest<OperationResult<TData>>
    {
    }
}
