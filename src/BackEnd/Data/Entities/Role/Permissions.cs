using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Role
{
    [Flags]
    public enum Permissions
    {
        [Display(Name = "دسترسی به پنل مدیریت")]
        AccessToAdminPanel,
     
        [Display(Name = "افزودن کاربر")]
        CreateUser,
        [Display(Name = "ویرایش کاربر")]
        EditUser,
        [Display(Name = "حذف کاربر")]
        DeleteUser,
        [Display(Name = "دریافت کاربر")]
        GetUser,

        [Display(Name = "Create Api")]
        CreateApi,
        [Display(Name = "Edit Api")]
        EditApi,
        [Display(Name = "Delete Api")]
        DeleteApi,
        [Display(Name = "دریافت کاربر")]
        GetApi,

        [Display(Name = "افزودن مهارت ها")]
        CreateSkills,
        [Display(Name = "ویرایش مهارت ها")]
        EditSkills,
        [Display(Name = "حذف مهارت ها")]
        DeleteSkills,
        [Display(Name = "دریافت کاربر")]
        GetSkills,


        [Display(Name = "افزودن تنظیمات سایت")]
        CreateSiteSetting,
        [Display(Name = "ویرایش تنظیمات سایت")]
        EditSiteSetting,
        [Display(Name = "حذف تنظیمات سایت")]
        DeleteSiteSetting,
        [Display(Name = "دریافت کاربر")]
        GetSiteSetting,

        [Display(Name = "افزودن نقش")]
        CreateRole,
        [Display(Name = "ویرایش نقش")]
        EditRole,
        [Display(Name = "حذف نقش")]
        DeleteRole,
        [Display(Name = "دریافت کاربر")]
        GetRole,

        [Display(Name = "افزودن نمونه کار")]
        CreatePortfolio,
        [Display(Name = "ویرایش نمونه کار")]
        EditPortfolio,
        [Display(Name = "حذف نمونه کار")]
        DeletePortfolio,
        [Display(Name = "دریافت کاربر")]
        GetPortfolio,

        [Display(Name = "افزودن پروژه شخصی")]
        CreateCustomProject,
        [Display(Name = "ویرایش پروژه شخصی")]
        EditCustomProject,
        [Display(Name = "حذف پروژه شخصی")]
        DeleteCustomProject,
        [Display(Name = "دریافت کاربر")]
        GetCustomProject,
   
        [Display(Name = "افزودن صفحه در مدیریت صفحات")]
        CreatePageManagement,
        [Display(Name = "ویرایش صفحه در مدیریت صفحات")]
        EditPageManagement,
        [Display(Name = "حذف صفحه در مدیریت صفحات")]
        DeletePageManagement,
        [Display(Name = "دریافت صفحه در مدیریت صفحات")]
        GetPageManagement,
   
        [Display(Name = "افزودن پرسش و پاسخ")]
        CreateFAQ,
        [Display(Name = "ویرایش پرسش و پاسخ")]
        EditFAQ,
        [Display(Name = "حذف پرسش و پاسخ")]
        DeleteFAQ,
        [Display(Name = "دریافت پرسش و پاسخ")]
        GetFAQ,
   
        [Display(Name = "افزودن تماس با ما")]
        CreateContactUs,
        [Display(Name = "ویرایش تماس با ما")]
        EditContactUs,
        [Display(Name = "حذف تماس با ما")]
        DeleteContactUs,
        [Display(Name = "دریافت تماس با ما")]
        GetContactUs,
    }
}
