using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                db.Database.Migrate();

                if (!db.Roles.AnyAsync().Result)
                {
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                    Task.Run(async () =>
                    {
                        var adminRole = GlobalConstants.AdminRole;
                        var userRole = GlobalConstants.UserRole;
                        var ownerRole = GlobalConstants.OwnerRole;

                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = adminRole
                        });

                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = userRole
                        });

                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = ownerRole
                        });
                    }).Wait();
                }
            }

            return app;
        }

        //public static IApplicationBuilder AddOwnerUser(this IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        //        db.Database.Migrate();

        //        if (!db.Users.AnyAsync().Result)
        //        {
        //            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

        //            Task.Run(async () =>
        //            {
        //                var userName = "OwnerYV";
        //                var password = "$9819NaNYu#";

        //                var user = new User
        //                {
        //                    UserName = userName,
        //                    FirstName = "NoNe",
        //                    LastName = "eNoN",
        //                    Cart = new Cart(),
        //                    Email = "tep981@gmail.com",
        //                    PhoneNumber = "0876688427",
        //                };

        //                await userManager.CreateAsync(user, password);
        //                await userManager.AddToRoleAsync(user, GlobalConstants.OwnerRole);
        //            }).Wait();
        //        }
        //    }

        //    return app;
        //}


    }
}
