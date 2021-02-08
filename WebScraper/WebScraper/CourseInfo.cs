/* Author:    Dan Ruley
  Partner:   None
  Date:      November, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.
  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.
 */
using System;

namespace WebScraper
{
    /// <summary>
    /// Simple class that represents the information of a course scraped via the course enrollments.
    /// </summary>
    class CourseInfo
    {
        public string Dept { get; set; }
        public string Num { get; set; }
        public string Credits { get; set; }
        public string Title { get; set; }
        public string Enrollment { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Two courses are equal if their hash codes are equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            return other is CourseInfo && this.GetHashCode() == ((CourseInfo)other).GetHashCode();
        }

        /// <summary>
        /// Lets us store the courses in an efficient hash map.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Dept, Num, Semester, Year);
        }

        /// <summary>
        /// Return a string representation of the course to display in the list box.
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            return Semester + " | " + Year + " | " + Dept + " | " + Num + " | " + Title + " | " + Enrollment + " | " + Credits;
        }

        /// <summary>
        /// Get a string representation of the course to save in a csv file.
        /// </summary>
        /// <returns></returns>
        public string GetCSVString()
        {
            return Dept + "," + Num + "," + Credits + "," + Title.Replace(',', ' ') + "," + Enrollment + "," + Semester + "," + Year + "," + Description.Replace(',',' ');
        }

        /// <summary>
        /// Gets the CSV header string
        /// </summary>
        /// <returns></returns>
        public static string GetCSVHeader()
        {
            return "Course Dept,Course Number,Course Credits,Course Title,Course Enrollment,Course Semester,Course Year,Course Description";
        }
    }
}
