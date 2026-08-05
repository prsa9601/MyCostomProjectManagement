using BackEnd.Core;
using BackEnd.Shared.Utilities.FileUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyCostomProjectManagement.Infrastructure.UploadApi
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken] // ← غیرفعال کردن Anti-forgery برای API
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;

        public UploadController(IWebHostEnvironment env, IFileService fileService)
        {
            _env = env;
            _fileService = fileService;
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile upload)
        {
            // بررسی وجود فایل
            if (upload == null || upload.Length == 0)
                return BadRequest(new { error = new { message = "هیچ فایلی ارسال نشده است." } });

            // اعتبارسنجی پسوند فایل
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".mp4", ".mov", ".avi", ".mkv", ".webm" };
            var extension = Path.GetExtension(upload.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest(new { error = new { message = "نوع فایل مجاز نیست. فقط تصاویر و ویدیوها پشتیبانی می‌شوند." } });

            // محدودیت حجم (۱۰۰ مگابایت)
            if (upload.Length > 100 * 1024 * 1024)
                return BadRequest(new { error = new { message = "حجم فایل نباید بیشتر از ۱۰۰ مگابایت باشد." } });

            // تولید نام یکتا
            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

            // ایجاد پوشه در صورت نبود
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, fileName);

            // ذخیره فایل
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            // برگرداندن آدرس فایل
            var fileUrl = $"/uploads/{fileName}";
            return Ok(new { url = fileUrl });
        }
    
    //[HttpPost]
    //public async Task<IActionResult> Upload(IFormFile file)
    //{
    //    if (file == null || file.Length == 0)
    //        return BadRequest(new { error = "هیچ فایلی ارسال نشده است." });

    //    var uploadFolder = "uploads";
    //    var uploadPath = Path.Combine(_env.WebRootPath, uploadFolder);
    //    if (!Directory.Exists(uploadPath))
    //        Directory.CreateDirectory(uploadPath);

    //    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
    //    var filePath = Path.Combine(uploadPath, fileName);

    //    using (var stream = new FileStream(filePath, FileMode.Create))
    //    {
    //        await file.CopyToAsync(stream);
    //    }

    //    var url = $"/{uploadFolder}/{fileName}";
    //    return Ok(new { url });
    //}
    //public async Task OnGet(IFormFile File)
    //{
    //    var result = await _fileService.SaveFileAndGenerateName(File, Directories.BlogFiles);
    //}
}
}
