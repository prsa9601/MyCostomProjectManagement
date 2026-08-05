using BackEnd.Core.ContactUs.Commands.Create;
using BackEnd.Core.ContactUs.Commands.Remove;
using BackEnd.Core.ContactUs.Queries.DTOs;
using BackEnd.Core.ContactUs.Queries.GetFilter;
using BackEnd.Shared.CoreShared;
using MediatR;
using MyCostomProjectManagement.Shared.Attributes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyCostomProjectManagement.Facade.ContactUs
{
    public interface IContactUsFacade
    {
        Task<OperationResult> Create(CreateContactUsCommand command);
        Task<OperationResult> Remove(RemoveContactUsCommand command);

        Task<ContactUsFilterResult> GetFilter(ContactUsFilterParam filterParams);
    }
    public class ContactUsFacade : IContactUsFacade
    {
        private readonly IMediator _mediator;

        public ContactUsFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.CreateContactUs)]
        public async Task<OperationResult> Create(CreateContactUsCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<ContactUsFilterResult> GetFilter(ContactUsFilterParam filterParams)
        {
            return await _mediator.Send(new GetContactUsFilterQuery(filterParams));
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.DeleteContactUs)]
        public async Task<OperationResult> Remove(RemoveContactUsCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
