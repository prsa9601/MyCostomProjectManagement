using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Role.Commands.Remove
{
    public class RemoveRoleCommand : IBaseCommand
    {
        public Guid roleId { get; set; }
    }
    public class RemoveRoleCommandHandler : IBaseCommandHandler<RemoveRoleCommand>
    {
        private readonly IRoleRepository _repository;

        public RemoveRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetTracking(request.roleId);
            if (role == null) return OperationResult.NotFound();

            bool removeResult = await _repository.DeleteAsync(role);
            if(!removeResult) return OperationResult.Error("خطای سمت سرور \n لطفا بعدا تلاش کنید.");

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
