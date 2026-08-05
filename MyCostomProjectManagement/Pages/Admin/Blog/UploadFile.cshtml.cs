using BackEnd.Core;
using BackEnd.Shared.Utilities.FileUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyCostomProjectManagement.Pages.Admin.Blog
{
    public class UploadFileModel : PageModel
    {
        //private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;

        public UploadFileModel(IFileService fileService)
        {
            _fileService = fileService;
        }

        //public UploadFileModel(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}

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
        public async Task OnGet(IFormFile File)
        {
            var result = await _fileService.SaveFileAndGenerateName(File, Directories.BlogFiles);
        }
    }
}
