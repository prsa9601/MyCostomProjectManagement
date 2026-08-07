using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.ContactUs
{
    public class ContactUs : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public bool IsAnswered { get; set; }

        public ContactUs(string fullName, string subject, string message, string phoneNumber)
        {
            FullName = fullName;
            Subject = subject;
            Message = message;
            PhoneNumber = phoneNumber;
            IsRead = false;
            IsAnswered = false;
        }
     
        public void Edit(string fullName, string subject, string message, string phoneNumber)
        {
            FullName = fullName;
            Subject = subject;
            Message = message;
            PhoneNumber = phoneNumber;
            IsRead = false;
            IsAnswered = false;
        }

        public void ChangeAnswerVisibility(bool isAnswered)
        {
            IsAnswered = isAnswered;
        }
        
        public void ChangeReadVisibility(bool isRead)
        {
            IsRead = isRead;
        }

    }
}
