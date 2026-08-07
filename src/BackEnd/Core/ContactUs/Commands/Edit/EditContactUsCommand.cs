using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.ContactUs.Commands.Edit
{
    public class EditContactUsCommand : IBaseCommand
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public bool IsAnswered { get; set; }
    }
    public class EditContactUsCommandHandler : IBaseCommandHandler<EditContactUsCommand>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public EditContactUsCommandHandler(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Handle(EditContactUsCommand request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var contactUs = await context.ContactUs.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (contactUs == null) return OperationResult.NotFound();
            
            contactUs.Edit(request.FullName, request.Subject,
               request.Message, request.PhoneNumber);

            contactUs.ChangeReadVisibility(request.IsRead);
            context.ContactUs.Update(contactUs);

            await context.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
