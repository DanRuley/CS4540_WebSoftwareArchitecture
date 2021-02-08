/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Seeds the DB with some hard-coded Opportunities.
 */

using System;
using URC.Models;

namespace URC.Data
{
    public class Opportunity_Seeding
    {
        /// <summary>
        /// Create some default values and add them to the ResearchOpportunity DB.
        /// </summary>
        public static void Initialize_Opportunities(ResearchOpportunityContext context)
        {
            var opportunities = new ResearchOpportunity[]
                {
                new ResearchOpportunity{ProjectName = "Graphics and Visualization Lab", ProfName = "Dr. John Doe", Description = "We are looking for a paid research assistant in the Graphics and Visualization lab.", StudentMentor = "Dr. John Doe", Start=DateTime.Parse("2020-09-25"),End=DateTime.Parse("2021-1-1"), PhoneNumber = "801-867-5309", Filled = false, WeeklyHrs = 15, HourlyPay = 16.00, ProfessorImage = "/images/FAE_visualization.jpg", Skills = "Matlab, C++, Blender", Tags = "CS, Graphics, Research, Visualization", ProfEmail = "johndoe@utah.edu"},


                new ResearchOpportunity{ProjectName = "Robotics Lab: Motion Tracking", ProfName = "Dr. Mary", ProfEmail = "professor_mary@utah.edu", Description = "We are looking for a paid research assistant for assistance in laying the groundwork for preventing the robot uprising.", StudentMentor = "John Connor", Start=DateTime.Parse("2020-09-25"),End=DateTime.Parse("2021-05-25"), PhoneNumber = "801-123-4567", Filled = false, WeeklyHrs = 10, HourlyPay = 14.50, ProfessorImage = "/images/Robot.jpg", Skills = "Arduino, Assembly, C++", Tags = "Computer Science, U of U, Utah, Robots"},


                new ResearchOpportunity{ProjectName = "Machine Learning Lab", ProfName = "Dr. Bob Jones", ProfEmail = "bobJones@utah.edu", Description = "Our lab is seeking undergraduates with a solid background in machine learning for assistance on several projects.  Please fill out an application and contact us with any questions.", StudentMentor = "Dr. Bob Jones", Start=DateTime.Parse("2020-12-21"),End=DateTime.Parse("2021-05-25"), PhoneNumber = "801-111-2222", Filled = false, WeeklyHrs = 15, HourlyPay = 15, ProfessorImage = "/images/ML.jpg", Skills = "Python, numpy, Statistics", Tags = "AI, Machine Learning, CS, Computer Science"},

                new ResearchOpportunity{ProjectName = "Scientific Computing Lab", ProfName ="Dr. Strangelove", ProfEmail = "professor@utah.edu", Description="Our project needs an undergraduate research assistant with a strong background in scientific computing and linear algebra.", StudentMentor="Dr. Strangelove", Start=DateTime.Parse("2020-12-1"),End = DateTime.Parse("2021-9-20"), PhoneNumber ="801-713-2918", Filled = false, WeeklyHrs = 15, HourlyPay = 20, ProfessorImage="/images/SciComp.png", Skills = "Matlab, R, numpy, Python, Linear Algebra", Tags = "Scientific Computing, University of Utah, The U, Research" },


                new ResearchOpportunity{ProjectName = "Natural Language Processing Research Assistant", ProfName ="Dr. Jeeves", ProfEmail = "drjeeves@utah.edu", Description="Our lab is looking for qualified students to help with our NLP research.  A strong background in statistics and machine learning is preferred.", StudentMentor="Dr. Jeeves", Start=DateTime.Parse("2020-11-20"),End = DateTime.Parse("2021-11-20"), PhoneNumber ="801-781-5923", Filled = false, WeeklyHrs = 18, HourlyPay = 15, ProfessorImage="/images/natural-language-processing.png", Skills = "Java, C++, Python", Tags = "NLP, Natural Language Processing, CS, Computer Science" }
                };
            
            context.ResearchOpportunities.AddRange(opportunities);
            context.SaveChanges();
        }

        public static void SeedCS(ResearchOpportunityContext context)
        {
            OpportunityScraper Scraper = new OpportunityScraper();
            Scraper.Scrape();
            context.AddRange(Scraper.GetOpps());
            context.SaveChanges();
        }

    }



}
