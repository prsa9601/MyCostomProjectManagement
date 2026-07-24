using Microsoft.AspNetCore.Components.Forms;

namespace MyCostomProjectManagement.Shared.Extensions
{
    public class FileExtensions
    {
        public async Task<IFormFile> ConvertToIFormFile(IBrowserFile browserFile)
        {
            try
            {
                // محدودیت حجم (مطابق با نیازتان تغییر دهید)
                const long maxFileSize = 200 * 1024 * 1024; // 2 مگابایت

                // ایجاد یک MemoryStream و کپی کردن محتوای فایل در آن
                var memoryStream = new MemoryStream();
                await using (var fileStream = browserFile.OpenReadStream(maxFileSize))
                {
                    await fileStream.CopyToAsync(memoryStream);

                    // موقعیت stream را به ابتدا برگردانید
                    memoryStream.Position = 0;

                    // ساخت IFormFile واقعی
                    var formFile = new FormFile(
                        baseStream: memoryStream,
                        baseStreamOffset: 0,
                        length: memoryStream.Length,
                        name: browserFile.Name,          // نام فیلد در فرم
                        fileName: browserFile.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = browserFile.ContentType
                    };

                    return formFile;
                }
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool IsImage(Stream stream)
        {
            try
            {
                byte[] header = new byte[8];
                stream.Read(header, 0, header.Length);
                stream.Position = 0; // بازگردانی موقعیت استریم

                // JPEG: FF D8 FF
                if (header.Take(3).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF }))
                    return true;

                // PNG: 89 50 4E 47 0D 0A 1A 0A
                if (header.SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }))
                    return true;

                // GIF: "GIF" در ASCII
                if (header.Take(3).SequenceEqual(new byte[] { 0x47, 0x49, 0x46 }))
                    return true;

                // BMP: "BM" در ASCII
                if (header.Take(2).SequenceEqual(new byte[] { 0x42, 0x4D }))
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsVideo(Stream stream)
        {
            try
            {
                byte[] header = new byte[12];
                stream.Read(header, 0, header.Length);
                stream.Position = 0;

                // MP4: فایل‌های MP4 با "ftyp" شروع می‌شوند (از بایت ۴)
                if (header.Skip(4).Take(4).SequenceEqual(new byte[] { 0x66, 0x74, 0x79, 0x70 }))
                    return true;

                // AVI: RIFF فرمت
                if (header.Take(4).SequenceEqual(new byte[] { 0x52, 0x49, 0x46, 0x46 }) &&
                    header.Skip(8).Take(4).SequenceEqual(new byte[] { 0x41, 0x56, 0x49, 0x20 }))
                    return true;

                // MOV: مشابه MP4
                if (header.Skip(4).Take(4).SequenceEqual(new byte[] { 0x66, 0x74, 0x79, 0x70 }))
                    return true;

                // MKV: امضای EBML
                if (header.Take(4).SequenceEqual(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }))
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
        public string GetVideoMimeType(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return "video/mp4";
            var ext = Path.GetExtension(imageUrl).ToLowerInvariant();
            return ext switch
            {
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                ".ogg" => "video/ogg",
                ".mov" => "video/quicktime",
                ".avi" => "video/x-msvideo",
                _ => "video/mp4"
            };
        }
    }
}