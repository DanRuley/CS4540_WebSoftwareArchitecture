/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Main entry point for the application.  Sets up DB if it doesn't exist.
 */

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;

namespace URC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();

        }


        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var opp_context = services.GetRequiredService<ResearchOpportunityContext>();
                    //context.Database.EnsureCreated();
                    DbInitializer.Initialize(opp_context);

                    var role_context = services.GetRequiredService<UsersRolesDB>();
                    UserManager<IdentityUser> _um = services.GetRequiredService<UserManager<IdentityUser>>();
                    RoleManager<IdentityRole> _rm = services.GetRequiredService<RoleManager<IdentityRole>>();

                    SeedUsersRolesDB.SeedDB(role_context, _um, _rm, opp_context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }

                
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
