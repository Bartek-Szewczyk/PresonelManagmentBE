using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresonelManagmentBE.Data;

namespace PresonelManagmentBE.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Categories.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }
                
               
               context.Categories.AddRange(
                   new Category()
                   {
                       Id = 1,
                       Name = "Barman"
                   },
                   new Category()
                   {
                       Id = 2,
                       Name = "Kelner"
                   },
                   new Category()
                   {
                       Id = 3,
                       Name = "Kucharz"
                   });
               context.SaveChanges();
               
                context.Users.AddRange(
                    new ApplicationUser()
                    {
                        FirstName = "Alek",
                        LastName = "Sobczak",
                        Email = "AlekS@mail.com",
                        PhoneNumber = "739153092"
                    });
                context.SaveChanges();
                context.ReportHistories.AddRange(
                    new ReportHistory()
                    {
                        SumOfHours = 189,
                        SumOfReports = 19,
                        DateTime = new DateTime(2022,01,02)
                    });
                var reports = context.ReportHistories.ToList();
                var users = context.Users.ToList();
                context.StaffUsers.AddRange(
                    
                    new StaffUser()
                    {
                        Id = 1,
                        User = users[0],
                        CategoryId = 1,
                      //  ReportHistory = reports[0]
                    });
                context.SaveChanges();
            }
        }
        
    }
}