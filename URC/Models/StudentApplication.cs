
/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Model class for the Student Application.
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URC.Models
{
    public class StudentApplication
    {
        /// <summary>
        /// Primary Key (auto set by DB)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Student Skill (comma separated string)
        /// </summary>
        [Required]
        public string Skills { get; set; }

        /// <summary>
        /// Resume file name
        /// </summary>
        [FileExtensions(Extensions = "txt, pdf, docx")] 
        public string ResumeFile { get; set; }

        /// <summary>
        /// The student's interests.
        /// </summary>
        [Required]
        public string Interests { get; set; }

        /// <summary>
        /// The student's graduation date.
        /// </summary>
        [Required]
        public DateTime GradDate { get; set; }


        /// <summary>
        /// Comma separated string of relevant CS courses the student has completed.
        /// </summary>
        [Required]
        public string CompletedCourses { get; set; }

        /// <summary>
        /// Degree path string.
        /// </summary>
        [Required]
        public string Degree { get; set; }

        /// <summary>
        /// Student gpa.
        /// </summary>
        [Required]
        [Range(0, 4, ErrorMessage = "Value must be between 0.0 and 4.0")]
        public double GPA { get; set; }


        /// <summary>
        /// Student UID.  Must be a u followed by 7 digits.
        /// </summary>
        [Required]
        [RegularExpression(@"^u[0-9]{7}$", ErrorMessage = "Error")]
        public string UID { get; set; }

        /// <summary>
        /// Student name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Student Email (this needs to corrospond with the student's User email in the DB)
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Number of hours per week desired.
        /// </summary>
        [Required]
        [Range(5, 60, ErrorMessage = "Hours per week must be between 5 and 60")]
        public int HoursPerWeek { get; set; }

        /// <summary>
        /// Student's personal statement.
        /// </summary>
        [Required]
        [MaxLength(1500, ErrorMessage = "Personal statement must be less than 1500 characters."), MinLength(20, ErrorMessage = "Personal statement must be at least 20 characters.")]
        public string PersonalStatement { get; set; }

        /// <summary>
        /// The time the application was submitted.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AppDate { get; set; }

        /// <summary>
        /// The time the application was last updated.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModified { get; set; }

        /// <summary>
        /// True if the student is seeking a position, false otherwise.
        /// </summary>
        [ScaffoldColumn(false)]
        public bool Active { get; set; }
    }
}
