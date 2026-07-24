namespace BackEnd.Shared.Utilities.FileUtil
{
    using System;
    using System.IO;
    using System.Linq;

    public static class MediaTypeDetector
    {
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp", ".svg" };
        private static readonly string[] VideoExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm", ".m4v", ".3gp" };

        /// <summary>
        /// تشخیص بر اساس پسوند فایل
        /// </summary>
        public static bool IsImage(string filePath) =>
            ImageExtensions.Contains(Path.GetExtension(filePath)?.ToLowerInvariant() ?? "");

        public static bool IsVideo(string filePath) =>
            VideoExtensions.Contains(Path.GetExtension(filePath)?.ToLowerInvariant() ?? "");

        /// <summary>
        /// تشخیص بر اساس هدر فایل (Magic Bytes) - دقیق‌تر
        /// </summary>
        public static string GetFileType(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 4) return "unknown";

            // تصاویر
            if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF) return "image"; // JPEG
            if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47) return "image"; // PNG
            if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46) return "image"; // GIF
            if (bytes[0] == 0x42 && bytes[1] == 0x4D) return "image"; // BMP
            if (bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46) return "image"; // WebP

            // ویدیوها
            if (bytes.Length > 8 &&
                bytes[4] == 0x66 && bytes[5] == 0x74 && bytes[6] == 0x79 && bytes[7] == 0x70) return "video"; // MP4
            if (bytes[0] == 0x41 && bytes[1] == 0x56 && bytes[2] == 0x49 && bytes[3] == 0x20) return "video"; // AVI
            if (bytes.Length > 4 &&
                bytes[4] == 0x6D && bytes[5] == 0x6F && bytes[6] == 0x6F && bytes[7] == 0x76) return "video"; // MOV
            if (bytes[0] == 0x1A && bytes[1] == 0x45 && bytes[2] == 0xDF && bytes[3] == 0xA3) return "video"; // WebM

            return "unknown";
        }

        /// <summary>
        /// متد ترکیبی: ابتدا پسوند، سپس محتوا
        /// </summary>
        public static string Detect(string filePath, byte[] content = null)
        {
            var ext = Path.GetExtension(filePath)?.ToLowerInvariant();
            if (ImageExtensions.Contains(ext)) return "image";
            if (VideoExtensions.Contains(ext)) return "video";

            if (content != null) return GetFileType(content);

            if (File.Exists(filePath))
            {
                var header = new byte[12];
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                fs.Read(header, 0, Math.Min(header.Length, (int)fs.Length));
                return GetFileType(header);
            }

            return "unknown";
        }

        //private async Task HandleFileSelected(InputFileChangeEventArgs e)
        //{
        //    var file = e.File;
        //    var buffer = new byte[file.Size];
        //    await file.OpenReadStream().ReadAsync(buffer);

        //    var type = MediaTypeDetector.Detect(file.Name, buffer);
        //    if (type == "image") { }
        //    // پردازش تصویر
        //    else if (type == "video") { }
        //    // پردازش ویدیو
        //    else { }
        //    // نوع ناشناس
        //}
    }
}
