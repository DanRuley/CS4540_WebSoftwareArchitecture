/* Author:    Dan Ruley
  Partner:   None
  Date:      October, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Data context class for the UsersRolesDB.
*/
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace URC.Data
{
    public class UsersRolesDB : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// User manager - used by controller and views.
        /// </summary>
        public UserManager<IdentityUser> userManager { get; set; }


        /// <summary>
        /// Role manager - used by controller and views.
        /// </summary>
        public RoleManager<IdentityRole> roleManager { get; set; }

        /// <summary>
        /// Entity provides us with the DB context for the UsersRolesDB
        /// </summary>
        public UsersRolesDB(DbContextOptions<UsersRolesDB> options)
            : base(options)
        {
        }

        /// <summary>
        /// Create the models.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
