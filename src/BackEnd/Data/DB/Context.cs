using BackEnd.Data.Entities.ContactUs;
using BackEnd.Data.Entities.FAQ;
using BackEnd.Data.Entities.Package;
using BackEnd.Data.Entities.PageManagement;
using BackEnd.Data.Entities.Portfolio;
using BackEnd.Data.Entities.ProjectRequestV1;
using BackEnd.Data.Entities.Projects;
using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Data.Entities.Skills;
using BackEnd.Data.Entities.Subscription;
using BackEnd.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.DB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TechnicalSkills> TechnicalSkills { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<PageManagement> PageManagements { get; set; }
        public DbSet<ProjectRequestV1> ProjectRequestV1{ get; set; }
        public DbSet<Subscription> Subscriptions{ get; set; }
        public DbSet<SubscriptionUser> SubscriptionUsers{ get; set; }
        
        public DbSet<Models.Logs> Logs{ get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            //modelBuilder.Entity<Category>()
            //    .HasDiscriminator<string>("PersonType")
            //    .HasValue<ProductCategory>("ProductCategory")  // به جای Student
            //    .HasValue<BlogCategory>("BlogCategory"); // به جای Teacher

            modelBuilder.Entity<FAQ>(builder =>
            {
                builder.ToTable("faq", "FAQ");
            });
                
                modelBuilder.Entity<Portfolio>(builder =>
            {
                builder.ToTable("Portfolios", "portfolio");
                builder.OwnsOne(b => b.File, image =>
                {
                    builder.ToTable("PortfolioFiles", "portfolio");

                });
            });
                base.OnModelCreating(modelBuilder);
        }
    }
}
