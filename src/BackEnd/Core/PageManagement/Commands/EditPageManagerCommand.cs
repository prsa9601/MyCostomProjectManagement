using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.PageManagement.Commands
{
    public class EditPageManagerCommand : IBaseCommand
    {
        public Data.Entities.PageManagement.PageManagement pageManagement { get; set; }
    }
    public class EditPageManagerCommandHandler : IBaseCommandHandler<EditPageManagerCommand>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public EditPageManagerCommandHandler(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Handle(EditPageManagerCommand request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            context.PageManagements.Update(request.pageManagement);

            await context.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
