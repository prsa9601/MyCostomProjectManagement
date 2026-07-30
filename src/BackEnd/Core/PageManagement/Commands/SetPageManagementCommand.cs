using BackEnd.Data.DB;
using BackEnd.Data.Entities.PageManagement;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.PageManagement.Commands
{
    public class SetPageManagementCommand : IBaseCommand
    {
        public List<Data.Entities.PageManagement.PageManagement> pageManagements { get; set; }
    }
    public class SetPageManagementHandlerCommand : IBaseCommandHandler<SetPageManagementCommand>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public SetPageManagementHandlerCommand(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Handle(SetPageManagementCommand request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            context.PageManagements.RemoveRange(context.PageManagements);

            await context.PageManagements.AddRangeAsync(request.pageManagements);
            await context.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
