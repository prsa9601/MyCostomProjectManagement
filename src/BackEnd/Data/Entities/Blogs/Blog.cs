using BackEnd.Shared.DataShared;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Blogs
{
    public class Blog : BaseEntity
    {
        public int BlogApiId { get; set; }

        [MaxLength(500)]
        public string Title { get; set; } = "";

        [MaxLength(500)]
        public string Slug { get; set; } = "";

        public string Content { get; set; } = "";

        [MaxLength(300)]
        public string Topic { get; set; } = "";

        [MaxLength(50)]
        public string Mode { get; set; } = "";

        public int Score { get; set; }

        [MaxLength(1000)]
        public string ScoreReason { get; set; } = "";

        public bool IsUseful { get; set; }

        /// <summary>
        /// وضعیت مقالات سئو: "pending" | "approved" | "rejected" | null
        /// </summary>
        [MaxLength(20)]
        public string? SeoStatus { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; } = false;

        public bool IsDelete { get; set; } = false;

        public bool IsActive { get; set; } = false;


        public Blog()
        {

        }
    }
}
