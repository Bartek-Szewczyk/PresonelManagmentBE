using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresonelManagmentBE.Data;

namespace PresonelManagmentBE.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var contextDb =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();



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
               var categories = context.Categories.ToList();
               context.Events.AddRange(
                   new Event()
                   {
                       Title = "Event 1",
                       DateStart = new DateTime(2022,6,25,12,50,00),
                       DateEnd = new DateTime(2022,6,25,12,55,00),
                       AllDay = true,
                       Category = categories[0],
                       StaffNumber = 6,
                       BackgroundColor = "#74CCC4"
                   },new Event()
                   {
                       Title = "Event 2",
                       DateStart = new DateTime(2022,6,30,12,50,00),
                       DateEnd = new DateTime(2022,6,30,12,55,00),
                       AllDay = true,
                       Category = categories[1],
                       StaffNumber = 8,
                       BackgroundColor = "#7D9DDD"
                   },new Event()
                   {
                       Title = "Event 3",
                       DateStart = new DateTime(2022,6,25,12,50,00),
                       DateEnd = new DateTime(2022,6,25,15,50,00),
                       AllDay = false,
                       Category = categories[2],
                       StaffNumber = 10,
                       BackgroundColor = "#6376C1"
                   },new Event()
                   {
                       Title = "Event 4",
                       DateStart = new DateTime(2022,6,20,12,50,00),
                       DateEnd = new DateTime(2022,6,20,12,55,00),
                       AllDay = false,
                       Category = categories[0],
                       StaffNumber = 2,
                       BackgroundColor = "#74CCC4"
                   },new Event()
                   {
                       Title = "Event 5",
                       DateStart = new DateTime(2022,6,28,10,00,00),
                       DateEnd = new DateTime(2022,6,28,20,00,00),
                       AllDay = false,
                       Category = categories[0],
                       StaffNumber = 5,
                       BackgroundColor = "#74CCC4"
                   });
               await context.SaveChangesAsync();
              
                // context.ReportHistories.AddRange(
                //     new ReportHistory()
                //     {
                //         SumOfHours = 189,
                //         SumOfReports = 19,
                //         DateTime = new DateTime(2022,01,02)
                //     });
                // var reports = context.ReportHistories.ToList();
                // var users = context.Users.ToList();
                // context.StaffUsers.AddRange(
                //     
                //     new StaffUser()
                //     {
                //         Id = 1,
                //         User = users[0],
                //         CategoryId = 1,
                //       //  ReportHistory = reports[0]
                //     });
                // context.SaveChanges();
            }
            var categoriesDb = contextDb.Categories.ToList();
            Console.WriteLine(categoriesDb[0]);
             var userList = new List<ApplicationUser>();
               userList.Add(new ApplicationUser(){ 
                   FirstName = "Alek", 
                   LastName = "Sobczak",
                   UserName = "Alek Sobczyk",
                   Email = "AlekS@mail.com",
                   PhoneNumber = "739153092",
                   Category = categoriesDb[0],
               });
               userList.Add(new ApplicationUser()
               { 
                   FirstName = "Piotr", 
                   LastName = "Sokołowski",
                   UserName = "Piotr Sokolowski",
                   Email = "Piotr.S@mail.com", 
                   PhoneNumber = "823612394",
                   Category = categoriesDb[0],
               });
               // userList.Add();
               // userList.Add();
               // userList.Add();
               //
               //
               //      ,new ApplicationUser()
               //      {
               //          FirstName = "Aleksandra",
               //          LastName = "Michalak",
               //          Email = "Aleksandra.M@mail.com",
               //          PhoneNumber = "605612394",
               //          Category = categories[1],
               //      },new ApplicationUser()
               //      {
               //          FirstName = "Elena",
               //          LastName = "Kubiak",
               //          Email = "Elena.K@mail.com",
               //          PhoneNumber = "426782196",
               //          Category = categories[1]
               //      },new ApplicationUser()
               //      {
               //          FirstName = "Eugeniusz",
               //          LastName = "Błaszczyk",
               //          Email = "Eugeniusz.B@mail.com",
               //          PhoneNumber = "602348716",
               //          Category = categories[2]
               //      },new ApplicationUser()
               //      {
               //          FirstName = "Adela",
               //          LastName = "Jakubowska",
               //          Email = "Adela.J@mail.com",
               //          PhoneNumber = "579152486",
               //          Category = categories[2]
               //      },new ApplicationUser()
               //      {
               //          FirstName = "Magda",
               //          LastName = "Mazur",
               //          Email = "Magda.M@mail.com",
               //          PhoneNumber = "813649721",
               //          Category = categories[0]
               //      });
               //  await context.SaveChangesAsync();
               //  
                
                string userPWD = "Password@1234";
                ApplicationUser lol = new()
                { 
                    FirstName = "imie",
                    LastName = "nazwisko", 
                    UserName = "name", 
                    Email = "mail@mial.coom",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "1278331",
                    Category = categoriesDb[1],
                        
                };
                await userManager.CreateAsync(lol, userPWD);
                foreach (var user in userList)
                {
                    ApplicationUser newUser = new()
                    { 
                        FirstName = user.FirstName,
                        LastName = user.LastName, 
                        UserName = user.UserName, 
                        Email = user.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = user.PhoneNumber,
                        Category = categoriesDb[1],
                        
                    };
                    var createPowerUser = await userManager.CreateAsync(newUser, userPWD);
                    // if (createPowerUser.Succeeded) 
                    //     await roleManager.AddToRoleAsync(newUser, UserRoles.User);
                            
                }

        }

       
    }
}