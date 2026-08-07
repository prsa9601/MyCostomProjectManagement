using BackEnd.Data.DB;
using BackEnd.Data.Entities.ProjectRequestV1;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.Project.Commands.ProjectRequestV1.Edit
{
    public class EditProjectRequestV1Command : IBaseCommand
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ProjectRequestType Type { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public bool IsWasSeen { get; set; }
        public bool IsWorked { get; set; }
        public bool IsDone { get; set; }
    }
    public class EditProjectRequestV1CommandHandler : IBaseCommandHandler<EditProjectRequestV1Command>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public EditProjectRequestV1CommandHandler(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Handle(EditProjectRequestV1Command request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var projectRequest = await context.ProjectRequestV1.FirstOrDefaultAsync(i => i.Id == request.Id);
            if (projectRequest == null) return OperationResult.NotFound();

            projectRequest.Edit(request.FullName, request.Email, request.PhoneNumber,
                request.Type, request.Price, request.Description, request.IsWasSeen, request.IsWorked, request.IsDone);
            
            context.ProjectRequestV1.Update(projectRequest);
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }
    }
}

