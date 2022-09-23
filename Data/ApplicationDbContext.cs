using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PresonelManagmentBE.Models;

namespace PresonelManagmentBE.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
               {
               }
     
        public DbSet<UserEvents> UserEvents { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<ReportHistory> ReportHistories { get; set; }
        public DbSet<Event> Events { get; set; }
        
         protected override void OnModelCreating(ModelBuilder builder)
         { 
             base.OnModelCreating(builder); 
         }
    }
}