/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  This class uses the Selenium ChromeDriver to scrape Professors from the cs.utah.edu website for database seeding
 */
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using URC.Models;

namespace URC.Data
{
    /// <summary>
    /// This class uses the Selenium ChromeDriver to scrape Professors from the cs.utah.edu website for database seeding
    /// </summary>
    public class OpportunityScraper
    {
        //static list of hardcoded skills, just to add some variety opportunities will pick three of these
        private static string[] skills = { "Java", "Python", "Ruby", "C", "C++", "Machine Learning", "AI", "Visualization", "Matlab",
        "Algorithms", "Rust", "Haskell", "Racket", "Fortran", "Linear Algebra", "Robotics", "Arduino", "Data Analysis", "Statistics", "Strong Math Background",
        "Functional Programming Experience", "Software Development", "Writing and Communication", "Creativity", "Time Management", "C#", ".NET", "Javascript",
        "Go", "Erlang", "BCPL", "Cobol", "Ada", "Systems Programming", "SQL", "Linux"};

        private ChromeDriver Driver;
        private Random Rng;
        List<ResearchOpportunity> Opps;

        /// <summary>
        /// Initialize internal data
        /// </summary>
        public OpportunityScraper()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Rng = new Random();
            Opps = new List<ResearchOpportunity>();
        }

        /// <summary>
        /// Driver for scraping U CS faculty
        /// </summary>
        public void Scrape()
        {
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("http://cs.utah.edu");
            Driver.FindElementByLinkText("Faculty").Click();
            ICollection<IWebElement> FacultyTables = Driver.FindElementsById("people");
            List<Tuple<int, string>> ScholarLinks = new List<Tuple<int, string>>();
            List<ResearchOpportunity> Scraped;
            foreach (var Table in FacultyTables)
            {
                Scraped = ReadProfTable(Table, ScholarLinks);
                if (Scraped != null)
                    Opps.AddRange(Scraped);
            }
            ScrapeTags(Opps, ScholarLinks);
            Driver.Quit();
        }

        /// <summary>
        /// Read a table of faculty and build up a list of opportunities
        /// </summary>
        private List<ResearchOpportunity> ReadProfTable(IWebElement FacultyTable, List<Tuple<int, string>> ScholarLinks)
        {
            List<ResearchOpportunity> ScrapedOpps = new List<ResearchOpportunity>();
            ResearchOpportunity opp;
            var NameInfo = FacultyTable.FindElements(By.Id("info"));

            if (NameInfo.Count == 0)
                return null;

            foreach (var Prof in NameInfo)
            {
                opp = new ResearchOpportunity();
                try
                {
                    opp.ProfName = Prof.FindElement(By.XPath("./tbody/tr[1]/td/a")).Text;
                    opp.WebsiteLink = Prof.FindElement(By.XPath("./tbody/tr[1]/td/a")).GetAttribute("href");
                }
                catch (NoSuchElementException)
                {
                    opp.ProfName = Prof.FindElement(By.XPath("./tbody/tr[1]/td")).Text;
                    opp.WebsiteLink = "No Website Provided";
                }

                try
                {
                    string scholar = Prof.FindElement(By.XPath("./tbody/tr[6]/td/a")).Text;
                    if (scholar == "Google Scholar")
                        ScholarLinks.Add(new Tuple<int, string>(ScrapedOpps.Count + Opps.Count, Prof.FindElement(By.XPath("./tbody/tr[6]/td/a")).GetAttribute("href")));
                }
                catch (NoSuchElementException)
                {
                    opp.Tags = "University of Utah, Computer Science, Research";
                }

                //"dummy" email
                opp.ProfEmail = opp.ProfName.Split(' ')[0] + "12345" + "@utah.edu";
                //"dummy" phone#
                opp.PhoneNumber = "801" + RandomPhone();
                opp.StudentMentor = opp.ProfName;
                opp.ProjectName = opp.ProfName + "'s Research Project";
                opp.Description = Prof.FindElement(By.XPath("../../td[3]/table/tbody/tr[2]/td")).Text;
                opp.ProfessorImage = Prof.FindElement(By.XPath("../../td/img")).GetAttribute("src");
                opp.Start = new DateTime(2021, Rng.Next(6) + 1, Rng.Next(28) + 1);
                opp.End = new DateTime(2021, Rng.Next(6) + 6, Rng.Next(30) + 1);
                opp.HourlyPay = 10 + Math.Floor(Rng.NextDouble() * 10) + (Rng.Next(2) == 1 ? 0.5 : 0);
                opp.WeeklyHrs = (short)(15 + Rng.Next(6) * 5);
                opp.Filled = false;
                opp.Skills = GetRandomSkills();
                opp.GenericImage = "/images/GenericImages/img" + Rng.Next(41) + ".jpg";

                Console.WriteLine(opp.ProfName);
                Console.WriteLine(opp.ProfEmail);
                Console.WriteLine(opp.PhoneNumber);
                Console.WriteLine(opp.Description);
                Console.WriteLine(opp.ProfessorImage);

                if (opp.Description.ToLower() == "na" || opp.Description.ToLower() == "n/a")
                    continue;

                ScrapedOpps.Add(opp);
            }

            return ScrapedOpps;
        }

        /// <summary>
        /// Return the total list of opportunities the scraper found.
        /// </summary>
        public List<ResearchOpportunity> GetOpps()
        {
            return Opps;
        }

        /// <summary>
        /// Make a random skill string for DB seeding
        /// </summary>
        private string GetRandomSkills()
        {
            HashSet<int> indxs = new HashSet<int>();
            StringBuilder s = new StringBuilder();

            while (indxs.Count < 3)
                indxs.Add(Rng.Next(skills.Length));

            foreach (int i in indxs)
                s.Append(skills[i] + ", ");

            s.Remove(s.Length - 2, 2);
            return s.ToString();
        }

        /// <summary>
        /// Make a random phone number for DB seeding
        /// </summary>
        private string RandomPhone()
        {
            StringBuilder pn = new StringBuilder();

            for (int i = 0; i < 7; i++)
                pn.Append("" + Rng.Next(10));

            return pn.ToString();
        }


        /// <summary>
        /// Scrape the tags from Prof. Google Scholar pages
        /// </summary>
        private void ScrapeTags(List<ResearchOpportunity> scrapedOpps, List<Tuple<int, string>> scholarLinks)
        {
            StringBuilder Tags;
            foreach (Tuple<int, string> scholar in scholarLinks)
            {
                Tags = new StringBuilder();
                Driver.Navigate().GoToUrl(scholar.Item2);
                var Stag = Driver.FindElementById("gsc_prf_int");


                foreach (var tag in Stag.FindElements(By.TagName("a")))
                    Tags.Append(tag.Text + ", ");

                if (Tags.Length < 2)
                    scrapedOpps[scholar.Item1].Tags = "University of Utah, Computer Science, Research";
                else
                {
                    Tags.Remove(Tags.Length - 2, 2);
                    scrapedOpps[scholar.Item1].Tags = Tags.ToString();
                }
            }
        }

        /// <summary>
        /// Print the opportunities
        /// </summary>
        private void PrintOpps()
        {
            foreach (var opp in Opps)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine(opp.ProfName);
                Console.WriteLine(opp.ProfEmail);
                Console.WriteLine(opp.PhoneNumber);
                Console.WriteLine(opp.Description);
                Console.WriteLine(opp.ProfessorImage);
                Console.WriteLine(opp.Tags);
            }
        }
    }
}
