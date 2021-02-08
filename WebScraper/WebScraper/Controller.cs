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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebScraper
{
    /// <summary>
    /// Controller class that does the logic of scraping course info from the web.
    /// </summary>
    class Controller
    {
        protected HashSet<CourseInfo> ScrapedCourses;
        private ChromeDriver Driver;

        /// <summary>
        /// Construct a new Controller containing a hashset of all scraped courses.
        /// </summary>
        public Controller()
        {
            ScrapedCourses = new HashSet<CourseInfo>();
        }

        /// <summary>
        /// Called when the user clicks on the get courses button.
        /// </summary>
        /// <param name="LinkText"></param>
        /// <returns></returns>
        internal HashSet<CourseInfo> HandleCourseClick(string LinkText, int max)
        {
            Driver.Navigate().GoToUrl(LinkText);
            Driver.FindElementByLinkText("Seating availability for all CS classes").Click();
            return GetCourses(LinkText.Substring(0,64), max);
        }

        /// <summary>
        /// Scrapes an entire semester of CS course enrollments given a year.  Also scrapes the descriptions and edits.
        /// </summary>
        /// <param name="BaseLink"></param>
        /// <returns></returns>
        private HashSet<CourseInfo> GetCourses(string BaseLink, int max)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            HashSet<CourseInfo> CurrentBatch = new HashSet<CourseInfo>();
            Driver.Manage().Window.Maximize();

            string[] Header = Driver.FindElements(By.TagName("h1")).ElementAt(1).Text.Split(" ");
            var Courses = Driver.FindElements(By.TagName("tr"));
            int count = 0;

            foreach (var _course in Courses)
            {
                if (count == max)
                    break;

                var CourseTable = _course.FindElements(By.TagName("td"));

                //Skip the header row and courses that don't match our criteria
                if (CourseTable.Count == 0 || CourseTable.ElementAt(3).Text != "001" || CourseTable.ElementAt(4).Text.ToLower().Contains("seminar") || CourseTable.ElementAt(4).Text.ToLower().Contains("special topics"))
                    continue;

                CourseInfo Course = new CourseInfo();

                int num;
                Course.Num = CourseTable.ElementAt(2).Text;

                if (!int.TryParse(Course.Num, out num) || num < 1000 || num > 7000)
                    continue;
                
                //Course.Title = CourseTable.ElementAt(4).Text;
                Course.Enrollment = CourseTable.ElementAt(7).Text;

                Course.Dept = Header[0];
                Course.Semester = Header[2];
                Course.Year = Header[3];

                CurrentBatch.Add(Course);
                ScrapedCourses.Add(Course);
                count++;
            }

            foreach(CourseInfo Course in CurrentBatch)
            {
                Driver.Navigate().GoToUrl(BaseLink + "description.html?subj=CS&catno=" + Course.Num + "&section=001");
                var CourseCards = Driver.FindElements(By.ClassName("card-header"));
                Course.Title = Driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div[1]/section/h1/span")).Text;

                foreach(var Card in CourseCards)
                    if (Card.Text == "Course Detail")
                        Course.Credits = Card.FindElement(By.XPath("../div/div/span")).Text;
                    else if (Card.Text == "Description")
                        Course.Description = Card.FindElement(By.XPath("../div/div")).Text;
                
            }

            return CurrentBatch;
        }

        /// <summary>
        /// Starts up the controller.
        /// </summary>
        /// <param name="Driver"></param>
        internal void Start(ChromeDriver Driver)
        {
            this.Driver = Driver;
            Driver.Navigate().GoToUrl("https://cs.utah.edu");
        }

        /// <summary>
        /// Performs the scraping of the course details from the course catalog, given a dept and course number.
        /// </summary>
        /// <param name="CourseString"></param>
        /// <returns></returns>
        internal string HandleDetailsClick(string CourseString)
        {
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Navigate().GoToUrl("https://catalog.utah.edu");
            Driver.FindElementByLinkText("Courses").Click();
            var Form = Driver.FindElement(By.Id("Search"));
            Form.SendKeys(CourseString);
            Driver.FindElementByLinkText(CourseString).Click();

            var Elements = Driver.FindElements(By.ClassName("noBreak"));
            foreach(var CourseItem in Elements)
            {
                var Header = CourseItem.FindElement(By.TagName("h3"));
                if (Header.Text == "Course Description")
                    return CourseItem.FindElement(By.XPath("./span/div/div/div/div/div")).Text;
            }
            return "Not found";
        }
        
        /// <summary>
        /// Get all of the scraped courses from this session.
        /// </summary>
        /// <returns></returns>
        public HashSet<CourseInfo> GetAllCourses()
        {
            return ScrapedCourses;
        }
    }
}
