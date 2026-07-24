using BackEnd.Data.Entities.Portfolio;
using BackEnd.Data.Entities.Portfolio.Repository;
using BackEnd.Shared.CoreShared;
using BackEnd.Shared.Utilities.FileUtil;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Core.Portfolio.Commands.Create
{
    public class CreatePortfoliCommand : IBaseCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PortfolioCategory Category { get; set; }

        public string? Link { get; set; }
        public IFormFile File { get; set; }
    }
    public class CreatePortfoliCommandHandler : IBaseCommandHandler<CreatePortfoliCommand>
    {
        private readonly IPortfolioRepository _repository;
        private readonly IFileService _fileService;

        public CreatePortfoliCommandHandler(IPortfolioRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreatePortfoliCommand request, CancellationToken cancellationToken)
        {
            var portfolio = new Data.Entities.Portfolio.Portfolio(request.Title,
                request.Description, request.Category, request.Link);

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
                        var fileName = await _fileService.SaveFileAndGenerateName(request.File, Directories.PortfolioVideo);
                        portfolio.SetFile(fileName);
                    }
                }
            }

            await _repository.AddAsync(portfolio);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
