using BackEnd.Shared.DataShared;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Tutorial
{
    public class Tutorial : BaseEntity
    {
        public int TutorialApiId { get; set; }

        [MaxLength(500)]
        public string Title { get; set; } = "";

        [MaxLength(200)]
        public string Topic { get; set; } = "";

        /// <summary>
        /// زبان، فریمورک یا موضوع آموزش: "C#", "React", "Docker", "Git", ...
        /// </summary>
        [MaxLength(100)]
        public string Subject { get; set; } = "";

        /// <summary>
        /// سطح: "beginner" | "intermediate" | "advanced" | "all"
        /// </summary>
        [MaxLength(20)]
        public string Level { get; set; } = "all";

        /// <summary>
        /// دسته‌بندی: "language" | "framework" | "tool" | "concept"
        /// </summary>
        [MaxLength(30)]
        public string Category { get; set; } = "concept";

        /// <summary>
        /// محتوای کامل Markdown
        /// </summary>
        public string Content { get; set; } = "";

        /// <summary>
        /// تعداد بخش‌ها (برای نمایش)
        /// </summary>
        public int SectionCount { get; set; }

        /// <summary>
        /// تعداد بلوک‌های کد
        /// </summary>
        public int CodeBlockCount { get; set; }

        /// <summary>
        /// آیا تأیید شده (اختیاری - برای بررسی دستی)
        /// </summary>
        public bool IsApproved { get; set; } = false;

        public bool IsDelete { get; set; } = false;

        public bool IsActive { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Tutorial()
        {
            
        }
    }
}
