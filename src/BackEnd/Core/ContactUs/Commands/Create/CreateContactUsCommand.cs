using BackEnd.Data.DB;
using BackEnd.Data.Entities.ContactUs;
using BackEnd.Data.Entities.ContactUs.Repository;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.ContactUs.Commands.Create
{
    public class CreateContactUsCommand : IBaseCommand
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
    public class CreateContactUsCommandHandler : IBaseCommandHandler<CreateContactUsCommand>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public CreateContactUsCommandHandler(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<OperationResult> Handle(CreateContactUsCommand request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var contact = new Data.Entities.ContactUs.ContactUs(request.FullName, request.Subject,
                request.Message, request.PhoneNumber);

            await context.ContactUs.AddAsync(contact);
            await context.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
