using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.Portfolio.Queries.DTOs
{
    public class PortfolioDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
