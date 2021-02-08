/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  This class seeds the StudentApplications table in the ResearchOpportunity DB.
 */
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using URC.Models;

namespace URC.Data
{
    public class Application_Seeding
    {
        private static string[] skills = { "Java", "Python", "Ruby", "C", "C++", "Machine Learning", "AI", "Visualization", "Matlab",
        "Algorithms", "Rust", "Haskell", "Racket", "Fortran", "Linear Algebra", "Robotics", "Arduino", "Data Analysis", "Statistics", "Strong Math Background",
        "Functional Programming Experience", "Software Development", "Writing and Communication", "Creativity", "Time Management", "C#", ".NET", "Javascript",
        "Go", "Erlang", "BCPL", "Cobol", "Ada", "Systems Programming", "SQL", "Linux"};

        private static string[] courses = {"1410", "2420", "3500", "3505", "3810", "2100", "3520", "4150", "4400", "4540", "6150", "3130", "3100", "3200", "3390",
            "3470", "3700", "4190", "4230", "4300", "4440", "4470", "4480", "4530", "4640", "5140", "5310", "5530", "5460", "5490"};


        /// <summary>
        /// Seed the database with two initial student applications.
        /// </summary>
        public static void Initialize_Applications(ResearchOpportunityContext context)
        {
            var apps = new StudentApplication[]
                {
                new StudentApplication
                {
                    Skills = "Java, C#, Python, Linear Algebra, C, C++",
                    ResumeFile = "",
                    Interests = "Hiking, Music, Camping, Books",
                    GradDate = DateTime.Parse("2021-05-25"),
                    CompletedCourses = "2420, 3500, 3505, 4150, 3200, 3130, 3810",
                    Degree = "CS",
                    GPA = 3.7,
                    UID = "u0000001",
                    Name = "Chadsworth",
                    Email = "u0000001@utah.edu",
                    HoursPerWeek = 20,
                    PersonalStatement = "I am passionate in research and would love to find a research opportunity.",
                    Active = true
                },
                new StudentApplication
                {
                    Skills = "C, C++, Java, Matlab, Python, Numpy, Javascript, CSS, HTML",
                    ResumeFile = "",
                    Interests = "Video games, art, science, music, literature",
                    GradDate = DateTime.Parse("2022-05-01"),
                    CompletedCourses = "2420, 3500, 3505, 3100, 3810, 3130",
                    Degree = "CS",
                    GPA = 3.5,
                    UID = "u0000002",
                    Name = "Phillip Fry",
                    Email = "u0000002@utah.edu",
                    HoursPerWeek = 15,
                    PersonalStatement = "It would be great to find a research opportunity at the U.  I am passionate about CS and science and would love to get involved in an interesting project.",
                    Active = true
                }
            };

            context.StudentApplications.AddRange(apps);
            context.SaveChangesAsync().Wait();
        }

        public static void SeedMoreApps(ResearchOpportunityContext context)
        {
            Random rng = new Random();
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            List<StudentApplication> seedApps = new List<StudentApplication>();
            List<string> interests = ScrapeRandomInterests(rng, driver);
            List<string[]> names = ScrapeRandomNames(rng, driver);

            for (int i = 0; i < names.Count; i++)
            {
                StudentApplication app = new StudentApplication();
                app.Active = true;
                app.Skills = GetRandomSkills(rng);
                app.ResumeFile = "";
                app.GradDate = DateTime.Parse("2022-05-12");
                app.CompletedCourses = GetRandomCourses(rng);
                app.Interests = GetRandomInterests(rng, interests);
                app.Degree = "CS";
                app.GPA = 2.5 + (rng.Next(16) * 0.1);
                app.UID = "u00000" + (i < 10 ? ("0" + i) : ("" + i));
                app.Name = names[i][0] + " " + names[i][1];
                app.Email = app.UID + "@utah.edu";
                app.HoursPerWeek = 15 + (5 * rng.Next(6));
                app.PersonalStatement = "It would be awesome to find a professor to do research with here at the U.";
                seedApps.Add(app);
            }

            context.StudentApplications.AddRange(seedApps);
            context.SaveChangesAsync().Wait();
        }

        private static List<string> ScrapeRandomInterests(Random rng, ChromeDriver driver)
        {
            driver.Navigate().GoToUrl("https://cafewebmaster.com/list-hobbies-and-interests-text-database");
            
            var container = driver.FindElement(By.XPath("/html/body/div[1]/div/div/section/div/article/div/div/div/section[1]/div/p[2]"));
            string s = container.Text;
            string[] split = s.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            return new List<string>(split);
        }

        private static List<string[]> ScrapeRandomNames(Random rng, ChromeDriver driver)
        {
            List<string[]> names = new List<string[]>();

            driver.Navigate().GoToUrl("https://github.com/dominictarr/random-name/blob/master/first-names.txt");

            for(int i = 0; i < 100; i++)
            {
                int trIndex = rng.Next(4500);
                var text = driver.FindElement(By.Id("LC" + (trIndex + 1))).Text;
                names.Add(new string[] { text, "" });
            }

            driver.Navigate().GoToUrl("https://github.com/dominictarr/random-name/blob/master/places.txt");
            for (int i = 0; i < 100; i++)
            {
                int trIndex = rng.Next(10000);
                var text = driver.FindElement(By.Id("LC" + (trIndex + 1))).Text;
                names[i][1] = text;
            }

            return names;
        }

        /// <summary>
        /// Make a random skill string for DB seeding
        /// </summary>
        private static string GetRandomSkills(Random rng)
        {
            HashSet<int> indxs = new HashSet<int>();
            StringBuilder s = new StringBuilder();

            while (indxs.Count < 3)
                indxs.Add(rng.Next(skills.Length));

            foreach (int i in indxs)
                s.Append(skills[i] + ", ");

            s.Remove(s.Length - 2, 2);
            return s.ToString();
        }

        private static string GetRandomCourses(Random rng)
        {
            HashSet<int> indxs = new HashSet<int>();
            StringBuilder s = new StringBuilder();

            while (indxs.Count < 3)
                indxs.Add(rng.Next(courses.Length));

            foreach (int i in indxs)
                s.Append(courses[i] + ", ");

            s.Remove(s.Length - 2, 2);
            return s.ToString();
        }

        private static string GetRandomInterests(Random rng, List<string> interests)
        {
            HashSet<int> indxs = new HashSet<int>();
            StringBuilder s = new StringBuilder();

            while (indxs.Count < 3)
                indxs.Add(rng.Next(interests.Count));

            foreach (int i in indxs)
                s.Append(interests[i] + ", ");

            s.Remove(s.Length - 2, 2);
            return s.ToString();
        }
    }


}
