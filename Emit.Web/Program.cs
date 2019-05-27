using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eimt.Application.Jobs;
using Eimt.Domain.DomainModels;
using Eimt.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace Emit.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<EiMTDbContext>();
                SeedData(context);
            }
            host.Run();
        }

        private static void SeedData(EiMTDbContext context)
        {
            if (context.Users.FirstOrDefault(x => x.Email == "admin.user@hotmail.com") == null)
            {
                var admin = new Role("Admin");
                var menager = new Role("Menager");
                var employee = new Role("Employee");
                context.Roles.Add(admin);
                context.Roles.Add(menager);
                context.Roles.Add(employee);
                List<User> admins = new List<User>
                   {
                       new User("admin.user@hotmail.com","password"),
                       new User("admin.user2@hotmail.com","password"),
                       new User("admin.user3@hotmail.com","password")
                };
                List<User> menagers = new List<User>()
                {
                        new User("menager.user1@hotmail.com","password"),
                       new User("menager.user2@hotmail.com","password"),
                       new User("menager.user3@hotmail.com","password"),
                       new User("menager.user4@hotmail.com","password"),
                };
                List<User> regular = new List<User>()
                {
                       new User("regular.user1@hotmail.com","password"),
                       new User("regular.user2@hotmail.com","password"),
                       new User("regular.user3@hotmail.com","password"),
                       new User("regular.user4@hotmail.com","password"),
                       new User("regular.user5@hotmail.com","password"),
                       new User("regular.user6@hotmail.com","password"),
                       new User("regular.user7@hotmail.com","password"),
                       new User("regular.user8@hotmail.com","password"),
                       new User("regular.user9@hotmail.com","password"),
                       new User("regular.user10@hotmail.com","password"),
                };
                List<Sector> ssectors = new List<Sector>();
                var sectors = new List<string>
                {
                    "IT","Marketing","Menagment","Business"
                };
                int menId = 0;
                foreach (var user in admins)
                {
                    admin.AddUser(user);
                    user.IsEmailConfirmed = true;
                }
                foreach (var user in menagers)
                {
                    menager.AddUser(user);
                    var s = new Sector(sectors[menId], user);
                    context.Sectors.Add(s);
                    ssectors.Add(s);
                    menId++;
                    user.IsEmailConfirmed = true;

                }
                menId = menagers.Count - 1;
                foreach (var user in regular)
                {
                    menId = menId % menagers.Count;
                    employee.AddUser(user);
                    user.IsEmailConfirmed = true;
                    ssectors[menId].Employees.Add(user);
                    menId++;
                }
                context.Users.AddRange(admins);
                context.Users.AddRange(menagers);
                context.Users.AddRange(regular);
                context.SaveChanges();
                menId = 0;
                foreach(var sec in ssectors)
                {
                    sec.Employees.Add(menagers[menId]);
                        menId++;
                }
                context.SaveChanges();

            }
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
