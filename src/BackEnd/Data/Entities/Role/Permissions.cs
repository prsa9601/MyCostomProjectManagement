using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Role
{
    [Flags]
    public enum Permissions
    {
        [Display(Name = "افزودن کاربر")]
        CreateUser,
        [Display(Name = "ویرایش کاربر")]
        EditUser,
        [Display(Name = "حذف کاربر")]
        DeleteUser,

        [Display(Name = "Create Api")]
        CreateApi,
        [Display(Name = "Edit Api")]
        EditApi,
        [Display(Name = "Delete Api")]
        DeleteApi,

        [Display(Name = "افزودن مهارت ها")]
        CreateSkills,
        [Display(Name = "ویرایش مهارت ها")]
        EditSkills,
        [Display(Name = "حذف مهارت ها")]
        DeleteSkills,

        [Display(Name = "افزودن تنظیمات سایت")]
        CreateSiteSetting,
        [Display(Name = "ویرایش تنظیمات سایت")]
        EditSiteSetting,
        [Display(Name = "حذف تنظیمات سایت")]
        DeleteSiteSetting,

        [Display(Name = "افزودن نقش")]
        CreateRole,
        [Display(Name = "ویرایش نقش")]
        EditRole,
        [Display(Name = "حذف نقش")]
        DeleteRole,

        [Display(Name = "افزودن نمونه کار")]
        CreatePortfolio,
        [Display(Name = "ویرایش نمونه کار")]
        EditPortfolio,
        [Display(Name = "حذف نمونه کار")]
        DeletePortfolio,

        [Display(Name = "افزودن پروژه شخصی")]
        CreateCustomProject,
        [Display(Name = "ویرایش پروژه شخصی")]
        EditCustomProject,
        [Display(Name = "حذف پروژه شخصی")]
        DeleteCustomProject,
    }
}
