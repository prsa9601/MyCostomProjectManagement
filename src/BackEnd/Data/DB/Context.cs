using BackEnd.Data.Entities.ContactUs;
using BackEnd.Data.Entities.Package;
using BackEnd.Data.Entities.Portfolio;
using BackEnd.Data.Entities.Projects;
using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Data.Entities.Skills;
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
        #endregion


    }
}
