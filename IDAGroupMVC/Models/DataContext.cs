using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Models
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions options) :base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyImages> CompanyImages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ViewCount> ViewCounts { get; set; }
        public DbSet<ClickDate> ClickDates { get; set; }
        public DbSet<EmailSetting> EmailSetting { get; set; }
    }
}
