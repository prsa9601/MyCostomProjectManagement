using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.ContactUs.Queries.DTOs
{
    public class ContactUsDto : BaseDto
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }
}
