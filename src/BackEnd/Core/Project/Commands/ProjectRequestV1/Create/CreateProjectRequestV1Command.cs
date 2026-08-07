using BackEnd.Data.DB;
using BackEnd.Data.Entities.ProjectRequestV1;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Project.Commands.ProjectRequestV1.Create
{
    public class CreateProjectRequestV1Command : IBaseCommand
    {
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
    public class CreateProjectRequestV1CommandHandler : IBaseCommandHandler<CreateProjectRequestV1Command>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public CreateProjectRequestV1CommandHandler(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Handle(CreateProjectRequestV1Command request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var projectRequest = new Data.Entities.ProjectRequestV1.ProjectRequestV1()
            {
                FullName = request.FullName,
                Description = request.Description,
                IsDone = request. IsDone,
                Email = request.Email,
                IsWasSeen = request.IsWasSeen,
                IsWorked = request.IsWorked,
                PhoneNumber = request.PhoneNumber,
                Price = request.Price,
                Type = request.Type,
            };
            context.ProjectRequestV1.AddAsync(projectRequest);
            await context.SaveChangesAsync();

            return OperationResult.Success();
        }
    }
}
