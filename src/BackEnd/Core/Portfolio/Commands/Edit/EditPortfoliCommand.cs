using BackEnd.Data.Entities.Portfolio;
using BackEnd.Data.Entities.Portfolio.Repository;
using BackEnd.Shared.CoreShared;
using BackEnd.Shared.Utilities.FileUtil;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Core.Portfolio.Commands.Edit
{
    public class EditPortfoliCommand : IBaseCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PortfolioCategory Category { get; set; }

        public string? Link { get; set; }
        public IFormFile File { get; set; }
    }
    public class EditPortfoliCommandHandler : IBaseCommandHandler<EditPortfoliCommand>
    {
        private readonly IPortfolioRepository _repository;
        private readonly IFileService _fileService;

        public EditPortfoliCommandHandler(IPortfolioRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditPortfoliCommand request, CancellationToken cancellationToken)
        {
            var portfolio = await _repository.GetTracking(request.Id);
            if (portfolio == null) return OperationResult.NotFound();

            portfolio.Edit(request.Title, request.Description, request.Category, request.Link);
            if (request.File != null)
            {
                using (var stream = request.File.OpenReadStream())
                {
                    if (_fileService.IsVideo(stream))
                    {
                        var fileName = await _fileService.SaveFileAndGenerateName(request.File, Directories.PortfolioVideo);
                        portfolio.SetFile(fileName);
                    }
                    else if (_fileService.IsImage(stream))
                    {
                        var fileName = await _fileService.SaveFileAndGenerateName(request.File, Directories.PortfolioImage);
                        portfolio.SetFile(fileName);
                    }
                }
            }

           
            try
            {
                await _repository.SaveChangeAsync();

            }
            catch (Exception ex)
            {

            }
            return OperationResult.Success();
        }
    }
}
