using BackEnd.Core.Auth.Commands.SendOtpCode;
using BackEnd.Core.Auth.Commands.VerifyOtpCode;
using BackEnd.Shared.CoreShared;
using MediatR;

namespace MyCostomProjectManagement.Facade.Auth
{
    public interface IAuthFacade
    {
        Task<OperationResult> SendOtpCode(SendOtpCodeCommand command);
        Task<OperationResult<VerifyOtpCodeResponse>> VerifyOtpCode(VerifyOtpCodeCommand command);
    }
    public class AuthFacade : IAuthFacade
    {
        private readonly IMediator _mediator;

        public AuthFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> SendOtpCode(SendOtpCodeCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<VerifyOtpCodeResponse>> VerifyOtpCode(VerifyOtpCodeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
