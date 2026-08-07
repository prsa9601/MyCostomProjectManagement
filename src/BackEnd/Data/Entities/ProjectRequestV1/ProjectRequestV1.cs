using BackEnd.Shared.DataShared;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.ProjectRequestV1
{
    public class ProjectRequestV1 : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ProjectRequestType Type { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public bool IsWasSeen { get; set; }
        public bool IsWorked { get; set; }
        public bool IsDone { get; set; }

        public void Edit(string fullName, string email, 
            string phoneNumber, ProjectRequestType type, long price, 
            string description, bool isWasSeen, bool isWorked, bool isDone)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Type = type;
            Price = price;
            Description = description;
            IsWasSeen = isWasSeen;
            IsWorked = isWorked;
            IsDone = isDone;
        }

    }

    public enum ProjectRequestType
    {
        [Display(Name = "انتخاب کنید")]
        None = 0,

        [Display(Name = "سایت شرکتی / شخصی")]
        Website = 1,

        [Display(Name = "فروشگاه اینترنتی")]
        Shop = 2,

        [Display(Name = "اپلیکیشن وب / PWA")]
        App = 3,

        [Display(Name = "سرویس API / بک‌اند")]
        Api = 4,

        [Display(Name = "داشبورد مدیریتی")]
        Dashboard = 5,

        [Display(Name = "سایر")]
        Other = 6
    }
}
