using BackEnd.Data.Entities.Skills;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Data.DB.Initializer
{
    public static class SetSiteSetting
    {
        public static async Task SiteSettingInitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();

            // اگر دیتابیس وجود نداشته باشد، ایجادش کن
            await context.Database.EnsureCreatedAsync();

            // بررسی کن که آیا جدول Coupon (یا هر جدول دیگر) خالی است
            if (!context.SiteSettings.Any())
            {
                var siteSetting = new Data.Entities.SiteSettings.SiteSetting()
                {
                    AboutMeSection = new Entities.SiteSettings.AboutMeSection
                    {
                        Description = "  <p>\r\n                    سلام! من محمد پارسا کریمی هستم، برنامه‌نویس و توسعه‌دهنده‌ی <strong>سی‌شارپ</strong> و <strong>دات‌نت</strong> با بیش از @GetExperience() سال تجربه در ساخت اپلیکیشن‌های وب و سیستمی.\r\n                    از سال ۲۰۲۲ برنامه‌نویسی رو به‌صورت حرفه‌ای شروع کردم و تا الان توی پروژه‌های شخصی، فریلنسری و تیم‌های کوچک، تجربه‌ی ارزشمندی جمع کردم.\r\n                    عاشق خلق راه‌حل‌های کارآمد و مقیاس‌پذیر هستم و همیشه دنبال چالش‌های جدید می‌گردم.\r\n                </p>\r\n                <p>\r\n                    تخصص من در <strong>بک‌اند</strong> با <strong>ASP.NET Core</strong>، طراحی <strong>API</strong>های قدرتمند و ساخت <strong>داشبورد</strong>های مدیریتی است،\r\n                    و همیشه به دنبال یادگیری فناوری‌های جدید هستم.\r\n                </p>",
                        Image = "defaultAboutMe.png",
                        Title = "برنامه‌نویس خلاق با عشق به کد و حل مسئله",
                        AboutStats = new Data.Entities.SiteSettings.AboutStats(36, GetExperience(),
                        12, 100)
                    },
                    SiteLinks = new List<Entities.SiteSettings.SiteLinks>
                    {
                        new Entities.SiteSettings.SiteLinks(true, 1, "گیت‌هاب",  "https://github.com/prsa9601", "fab fa-github"),
                        new Entities.SiteSettings.SiteLinks(true, 2, "لینکدین", "https://www.linkedin.com/in/mohammad-parsa-karimi-49876728b/", "fab fa-linkedin"),
                        new Entities.SiteSettings.SiteLinks(true, 3, "تلگرام",  "https://github.com", "fab fa-telegram"),
                        new Entities.SiteSettings.SiteLinks(true, 4, "توییتر",  "https://github.com", "fab fa-twitter"),
                    },
                    SiteIsActive = false,
                    ProjectSectionSetting = default,
                    GeneralSiteInformation = new Entities.SiteSettings.GeneralSiteInformation
                    {
                        TeamName = "محمد پارسا کریمی",
                    },
                    MenuSettings = new(),
                    SpecializedServicesSection = default,

                };
                try
                {
                    await context.SiteSettings.AddAsync(siteSetting);
                    await context.SaveChangesAsync();

                }
                catch (Exception c)
                {

                }
            }
        }
        private static DateTime birthDate = new DateTime(2004, 7, 1); // تیر ۱۳۸۲ ≈ July 2003

        private static int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        private static int GetExperience()
        {
            return GetAge() - 18; // از ۱۸ سالگی شروع کرده
        }
    }
}
