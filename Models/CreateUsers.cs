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
    public static class CreateUsers
    {
         public static async Task AddUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var contextDb =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var categories = contextDb.Categories.ToList();
            if(contextDb.Users.Any())
            {
                return;
            }
            var userList = new List<ApplicationUser>();
            userList.Add(new ApplicationUser(){ 
                FirstName = "Alek", 
                LastName = "Sobczak",
                UserName = "AlekSobczyk",
                Email = "AlekS@mail.com",
                PhoneNumber = "739153092",
                CategoryId = categories[0].Id,
            });
            userList.Add(new ApplicationUser()
            { 
                FirstName = "Piotr", 
                LastName = "Sokołowski",
                UserName = "PiotrSokolowski",
                Email = "Piotr.S@mail.com", 
                PhoneNumber = "823612394",
                CategoryId = categories[0].Id,
            });
            userList.Add(new ApplicationUser()
            {
                FirstName = "Aleksandra",
                LastName = "Michalak",
                UserName = "AleksandarMichalak",
                Email = "Aleksandra.M@mail.com",
                PhoneNumber = "605612394",
                CategoryId = categories[1].Id,
            });
            userList.Add(new ApplicationUser()
            {
                FirstName = "Elena",
                LastName = "Kubiak",
                UserName = "ElenaKubiak",
                Email = "Elena.K@mail.com",
                PhoneNumber = "426782196",
                CategoryId = categories[1].Id
            });
            userList.Add(new ApplicationUser()
            {
                FirstName = "Eugeniusz",
                LastName = "Błaszczyk",
                UserName = "EugeniuszBlaszczyk",
                Email = "Eugeniusz.B@mail.com",
                PhoneNumber = "602348716",
                CategoryId = categories[2].Id
            });
            userList.Add(new ApplicationUser()
            {
                FirstName = "Adela",
                LastName = "Jakubowska",
                UserName = "AdelaJakubowska",
                Email = "Adela.J@mail.com",
                PhoneNumber = "579152486",
                CategoryId = categories[2].Id
            });
            userList.Add(new ApplicationUser()
            {
                FirstName = "Magda",
                LastName = "Mazur",
                UserName = "MagdaMazur",
                Email = "Magda.M@mail.com",
                PhoneNumber = "813649721",
                CategoryId = categories[1].Id,
            });
            
            
            string userPWD = "Password@1234";
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
                    CategoryId = user.CategoryId
                        
                };
                
                    var createPowerUser = await userManager.CreateAsync(newUser, userPWD);

                    if (!createPowerUser.Succeeded) continue;
                    if (newUser.FirstName == "Piotr")
                    {
                        await userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser, UserRoles.User); 
                    }

            }
        }
         
    }
}