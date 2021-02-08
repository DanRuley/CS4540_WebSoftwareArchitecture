/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  The DbInitializer ensured the DB is created, seeding it with some values if it is not.
 */

using System.Linq;

namespace URC.Data
{
    public class DbInitializer
    {
        /// <summary>
        /// If the DB does not have any entries, make sure to seed them.
        /// </summary>
        /// <param name="context">db context</param>
        public static void Initialize(ResearchOpportunityContext context)
        {
            context.Database.EnsureCreated();

            if (context.ResearchOpportunities.Count() < 20)
                Opportunity_Seeding.SeedCS(context);

            // Look for any research opportunitites.
            if (!context.ResearchOpportunities.Any())         
                Opportunity_Seeding.Initialize_Opportunities(context);

            if (context.StudentApplications.Count() < 20)
                Application_Seeding.SeedMoreApps(context);

            if (!context.StudentApplications.Any())
                Application_Seeding.Initialize_Applications(context);
        }
    }
}
