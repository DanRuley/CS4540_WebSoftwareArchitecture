/* Author:    Dan Ruley
  Partner:   None
  Date:      October, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  This class is called on startup and 1.) ensures the UsersRolesDB is created, and 2.) Seeds it with some initial values if no users/roles already exist.
*/
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using URC.Data;
using URC.Models;

namespace URC.Areas.Identity.Data
{
    public class SeedUsersRolesDB
    {

        /// <summary>
        /// Ensure DB exists and then seed it with data if it is empty.
        /// </summary>
        public static void SeedDB(UsersRolesDB context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ResearchOpportunityContext opp_context)
        {
            context.Database.EnsureCreated();


            SeedRoles(context, roleManager);

            SeedUsers(context, userManager, opp_context);

        }


        /// <summary>
        /// Seed the Users DB with some default users and add give them the appropriate roles.
        /// </summary>
        private static void SeedUsers(UsersRolesDB context, UserManager<IdentityUser> userManager, ResearchOpportunityContext opp_context)
        {
            foreach(ResearchOpportunity opp in opp_context.ResearchOpportunities)
                if (userManager.FindByEmailAsync(opp.ProfEmail).Result == null)
                {
                    IdentityUser urcUser = new IdentityUser();
                    urcUser.Email = opp.ProfEmail;
                    urcUser.UserName = opp.ProfEmail;
                    urcUser.EmailConfirmed = true;
                    userManager.CreateAsync(urcUser, "123ABC!@#def").Wait();
                    userManager.AddToRoleAsync(urcUser, "Professor").Wait();
                }

            foreach (StudentApplication app in opp_context.StudentApplications)
                if (userManager.FindByEmailAsync(app.Email).Result == null)
                {
                    IdentityUser urcUser = new IdentityUser();
                    urcUser.Email = app.Email;
                    urcUser.UserName = app.Email;
                    urcUser.EmailConfirmed = true;
                    userManager.CreateAsync(urcUser, "123ABC!@#def").Wait();
                    userManager.AddToRoleAsync(urcUser, "Student").Wait();
                }

            //Create the required seed data in array to simplify code
            Tuple<string, string, string>[] seedUsers = {
                    new Tuple<string, string, string>("admin@utah.edu", "Administrator", "The Overlord"),
                    new Tuple<string, string, string>("professor@utah.edu","Professor", "Dr. Strangelove"),
                    new Tuple<string, string, string>("professor_mary@utah.edu","Professor", "Professor Mary"),
                    new Tuple<string, string, string>("u0000001@utah.edu","Student", "Chadsworth"),
                    new Tuple<string, string, string>("u0000002@utah.edu", "Student", "Phillip J. Fry"),
                    new Tuple<string, string, string>("u0000003@utah.edu", "Student", "Barnabus"),
                    new Tuple<string, string, string>("u1234567@utah.edu","Student", "Boris"),
                    new Tuple<string, string, string>("bobjones@utah.edu", "Professor", "Bob Jones"),
                    new Tuple<string, string, string>("drjeeves@utah.edu", "Professor", "Dr Jeeves"),
                    new Tuple<string, string, string>("johndoe@utah.edu", "Professor", "John Doe")};


            for (int i = 0; i < seedUsers.Length; i++)
            {
                if (userManager.FindByEmailAsync(seedUsers[i].Item1).Result == null)
                {
                    IdentityUser urcUser = new IdentityUser();
                    urcUser.Email = seedUsers[i].Item1;
                    urcUser.UserName = seedUsers[i].Item1;
                    urcUser.EmailConfirmed = true;
                    userManager.CreateAsync(urcUser, "123ABC!@#def").Wait();
                    userManager.AddToRoleAsync(urcUser, seedUsers[i].Item2).Wait();
                }
            }
        }


        /// <summary>
        /// Seed the Roles DB with the necessary roles.
        /// </summary>
        private static void SeedRoles(UsersRolesDB context, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Student";
                    roleManager.CreateAsync(role).Wait();


                    role = new IdentityRole();
                    role.Name = "Professor";
                    roleManager.CreateAsync(role).Wait();


                    role = new IdentityRole();
                    role.Name = "Administrator";
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }

        //Old seed users
        //private static void SeedUsers(UsersRolesDB context, UserManager<IdentityUser> userManager)
        //{
        //    //Create the required seed data in array to simplify code
        //    Tuple<string, string, string>[] seedUsers = {
        //        new Tuple<string, string, string>("admin@utah.edu", "Administrator", "The Overlord"),
        //        new Tuple<string, string, string>("professor@utah.edu","Professor", "Dr. Strangelove"),
        //        new Tuple<string, string, string>("professor_mary@utah.edu","Professor", "Professor Mary"),
        //        new Tuple<string, string, string>("u0000001@utah.edu","Student", "Chadsworth"),
        //        new Tuple<string, string, string>("u0000002@utah.edu", "Student", "Phillip J. Fry"),
        //        new Tuple<string, string, string>("u0000003@utah.edu", "Student", "Barnabus"),
        //        new Tuple<string, string, string>("u1234567@utah.edu","Student", "Boris"),
        //        new Tuple<string, string, string>("bobjones@utah.edu", "Professor", "Bob Jones"),
        //        new Tuple<string, string, string>("drjeeves@utah.edu", "Professor", "Dr Jeeves"),
        //        new Tuple<string, string, string>("johndoe@utah.edu", "Professor", "John Doe")};


        //    for (int i = 0; i < seedUsers.Length; i++)
        //    {
        //        if (userManager.FindByEmailAsync(seedUsers[i].Item1).Result == null)
        //        {
        //            IdentityUser urcUser = new IdentityUser();
        //            urcUser.Email = seedUsers[i].Item1;
        //            urcUser.UserName = seedUsers[i].Item1;
        //            urcUser.EmailConfirmed = true;
        //            userManager.CreateAsync(urcUser, "123ABC!@#def").Wait();
        //            userManager.AddToRoleAsync(urcUser, seedUsers[i].Item2).Wait();
        //        }
        //    }
        //}
    }
}

