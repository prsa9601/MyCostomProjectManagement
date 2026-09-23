namespace BackEnd.Core
{
    public class Directories
    {
        //public const string UserImageAccountPath = "api.6dongeh.ir/images/user/account";
        //public const string ProductImagePath = "api.6dongeh.ir/images/product/image";
        //public const string UserNationalCardPhotoPath = "api.6dongeh.ir/images/user/nationalityCode";
        //public const string UserBirthCertificatePhotoPath = "api.6dongeh.ir/images/user/birthCertificate";
        //public const string ProfitImages = "api.6dongeh.ir/images/user/ProfitImages";

        public const string PortfolioVideo = "wwwroot/Files/Portfolio/Video";
        public const string PortfolioImage = "wwwroot/Files/Portfolio/Image";
        public const string BlogFiles = "wwwroot/Files/Blog";
        public static string GetPortfolioImage(string path) => $"{SiteSettings.ServerPath}/{PortfolioImage.Replace("wwwroot/","")}/{path}";
        public static string GetPortfolioVideo(string path) => $"{SiteSettings.ServerPath}/{PortfolioVideo.Replace("wwwroot/", "")}/{path}";
        public static string GetBlogFiles(string path) => $"{SiteSettings.ServerPath}/{BlogFiles.Replace("wwwroot/", "")}/{path}";

    }
}
public static class SiteSettings
{
    public static string ServerPath { get; set; } = $"https://parsakarimidev.ir";
    //public static string ServerPath { get; set; } = $"http://parsakarimidev.ir/httpdocs";
    //public static string ServerPath { get; set; } = $"https://localhost:7259";
}
