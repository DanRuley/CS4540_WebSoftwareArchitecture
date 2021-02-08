/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Model class for the Research Opportunity
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace URC.Models
{

    public class ResearchOpportunity
    {
        /// <summary>
        /// Primary key (set by DB)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The name of the research project.
        /// </summary>
        public string ProjectName { get; set; }

        public string WebsiteLink { get; set; }

        /// <summary>
        /// The professor's name.
        /// </summary>
        public string ProfName { get; set; }

        /// <summary>
        ///The professor's email address - must correspond with the Professor user's email in the DB.
        /// </summary>
        public string ProfEmail { get; set; }

        /// <summary>
        /// The project description string.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The student mentor's name.
        /// </summary>
        public string StudentMentor { get; set; }

        /// <summary>
        /// The time when the position begins.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The time when the position ends.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// The associated phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// True if the position is filled.
        /// </summary>
        public bool Filled { get; set; }

        /// <summary>
        /// Number of weekly hours for the position.
        /// </summary>
        public short WeeklyHrs { get; set; }

        /// <summary>
        /// Hourly pay rate.
        /// </summary>
        public double HourlyPay { get; set; }

        /// <summary>
        /// String of relevant skills.
        /// </summary>
        public string Skills { get; set; }

        /// <summary>
        /// String of relevant serach tags.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Picture of the professor or similar
        /// </summary>
        [FileExtensions(Extensions = "jpeg, jpg, png")] 
        public string ProfessorImage { get; set; }

        /// <summary>
        /// Optional generic image to be used on the details page
        /// </summary>
        [FileExtensions(Extensions = "jpeg, jpg, png")]
        public string GenericImage { get; set; }
    }
}
