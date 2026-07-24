using BackEnd.Data.Entities.Portfolio;
using BackEnd.Shared.CoreShared.Queries;
using System.Security.Principal;

namespace BackEnd.Core.Portfolio.Queries.DTOs
{
    public class PortfolioDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PortfolioCategory Category { get; set; }

        public string? Link { get; set; }
        public PortfolioFileDto PortfolioFile { get; set; }
    }
    public class PortfolioFileDto : BaseDto
    {
        public string FileAddress { get; set; }
        public bool IsImage { get; set; }

    }
    public class PortfolioFilterParam : BaseFilterParam
    {
        public GetPortfolioType GetPortfolioFileType { get; set; }
    }
    public class PortfolioFilterResult : BaseFilter<PortfolioDto, PortfolioFilterParam>
    {

    }

    public enum GetPortfolioType
    {
        GetOnlyVideo,
        GetOnlyImage,
        GetAll
    }
}
