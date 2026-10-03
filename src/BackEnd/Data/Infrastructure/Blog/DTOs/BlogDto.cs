using BackEnd.Shared.CoreShared.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Data.Infrastructure.Blog.DTOs
{
    public class BlogDto : BaseDto
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Title { get; set; } = "";

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

    
    }
    public class BlogFilterParam : BaseFilterParam
    {
        public bool IsAdmin { get; set; }
        public string SearchTerm { get; set; }
        public string Level { get; set; }
        public string Category { get; set; }
        public BlogStatus? Status { get; set; }
    }
    public enum BlogStatus
    {
        Deleted,
        Approved,
        Pending,
        Active,
        Inactive,
        None
    }
    public class BlogFilterResult : BaseFilter<BlogDto, BlogFilterParam>
    {

    }
    public class BlogEntityFilterResult : BaseFilter<Data.Entities.Blogs.Blog, BlogFilterParam>
    {

    }
}
