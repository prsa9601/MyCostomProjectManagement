using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Portfolio
{
    public class PortfolioFile : BaseEntity
    {
        public bool IsVideo { get; set; } = false;
        public bool IsImage { get; set; } = false;
        public string? ImageAddress { get; set; }
        public string? VideoAddress { get; set; }

        private PortfolioFile()
        {
            
        }

        private void SetNewId()
        {
            Id = Guid.NewGuid();
        }

        public PortfolioFile(string fileAddress)
        {
            if (IsVideoFile(fileAddress))
            {
                IsVideo = true;
                IsImage = false;
                VideoAddress = fileAddress;
                ImageAddress = null;
            }
            else if (IsImageFile(fileAddress))
            {
                IsVideo = false;
                IsImage = true;
                ImageAddress = fileAddress;
                VideoAddress = null;
            }
            else
            {
                IsVideo = false;
                IsImage = false;
                VideoAddress = null;
                ImageAddress = null;
            }
        }

        public (string address, bool IsImage) GetFile()
        {
            if (IsImage)
            {
                return (ImageAddress, true);
            }
            if (IsVideo)
            {
                return (VideoAddress, false);
            }
            return("Default.png", true);
        }


        // ============================================================
        // متد تشخیص سریع با پسوند
        // ============================================================
        private bool IsVideoFile(string fileAddress)
        {
            var ext = Path.GetExtension(fileAddress)?.ToLowerInvariant();
            return ext == ".mp4" || ext == ".mov" || ext == ".mkv" ||
                   ext == ".avi" || ext == ".wmv" || ext == ".flv" ||
                   ext == ".webm" || ext == ".m4v" || ext == ".3gp";
        }

        private bool IsImageFile(string fileAddress)
        {
            var ext = Path.GetExtension(fileAddress)?.ToLowerInvariant();
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png" ||
                   ext == ".gif" || ext == ".bmp" || ext == ".tiff" ||
                   ext == ".webp" || ext == ".svg";
        }

    }
}
