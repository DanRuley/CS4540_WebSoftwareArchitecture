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
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WebScraper
{
    /// <summary>
    /// View class for the Web Scraper application.  It mainly consists of event handlers for when the user clicks different buttons.
    /// </summary>
    public partial class ClassScraper : Form
    {

        /// <summary>
        /// The Controller class that does the logic
        /// </summary>
        private Controller Controller;

        /// <summary>
        /// Used for parsing user input
        /// </summary>
        private const string BADINPUT = "BADINPUT";

        /// <summary>
        /// The Chrome Driver
        /// </summary>
        private ChromeDriver Driver;


        /// <summary>
        /// Construct a new ClassScraper Form.
        /// </summary>
        public ClassScraper()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event that occurs when the user clicks the scrape courses button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Query_Click(object sender, EventArgs e)
        {
            if (!IsSeleniumStarted())
                return;

            string Link = GetLinkFromInput();

            if (Link == BADINPUT)
                return;

            CourseList.Items.Clear();

            if (!int.TryParse(MaxInput.Text, out int max) || max < 1 || max > 250)
            {
                MessageBox.Show("Error: MaxInput must be a valid integer between 1 and 250.");
                MaxInput.Text = "100";
                return;
            }

            try
            {
                HashSet<CourseInfo> Result = Controller.HandleCourseClick(Link, max);
                if (Result.Count > 0)
                {
                    ShowAllButton.Show();
                    ShowAllLabel.Show();
                    Save.Show();
                    foreach (CourseInfo Course in Result)
                        CourseList.Items.Add(Course.ToShortString());
                }
            }
            catch(Exception ex)
            {
                CourseList.Items.Add("An error occurred.  Please restart selenium\nDetails: " + ex.Message);
                Driver.Quit();
                Driver = null;
                WelcomeGrp.Show();
            }
        }


        /// <summary>
        /// Parses the input text to return a valid link.
        /// </summary>
        /// <returns></returns>
        private string GetLinkFromInput()
        {
            string year = YearInput.Text;
            string semester = SemesterInput.Text.ToLower();

            if (year.Length != 4 || !int.TryParse(year, out _))
                YearError.Text = "Invalid Year";
            else
                YearError.Text = "";

            if (semester != "fall" && semester != "spring" && semester != "summer")
                SemesterError.Text = "Invalid Semester";
            else
                SemesterError.Text = "";

            if (YearError.Text.Length > 0 || SemesterError.Text.Length > 0)
                return BADINPUT;

            switch (semester)
            {
                case "spring":
                    semester = "4";
                    break;

                case "summer":
                    semester = "6";
                    break;

                case "fall":
                    semester = "8";
                    break;
            }

            return "https://student.apps.utah.edu/uofu/stu/ClassSchedules/main/1" + year.Substring(2, 2) + semester + "/class_list.html?subject=CS";
        }

        /// <summary>
        /// Save_Click handler
        /// Write the lines to the file in csv format (note, I remove commas from course descriptions so they aren't all messed up in the csv file)
        /// Reference: I pretty much copied the save file code from my 3500 spreadsheet project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "csv|*.csv";
                SFD.AddExtension = true;

                SFD.ShowDialog();

                if (SFD.FileName != "")
                    SaveCSV(SFD.FileName);

                MessageBox.Show("File saved successfully!\nFilename: " + SFD.FileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error saving file, please try again.\nDetails: " + ex.Message);
            }
        }

        /// <summary>
        /// Write the lines to the file in csv format (note, I remove commas from course descriptions so they aren't all messed up in the csv file)
        /// Reference: I pretty much copied the save file code from my 3500 spreadsheet project
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveCSV(string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine(CourseInfo.GetCSVHeader());
                foreach(CourseInfo Course in Controller.GetAllCourses())
                    file.WriteLine(Course.GetCSVString());
                file.Dispose();
            }
        }

        /// <summary>
        /// Handles the event when the user clicks the Start Selenium button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver();
                Controller = new Controller();
                Controller.Start(Driver);
                WelcomeGrp.Hide();

                CourseList.Items.Clear();
                SemesterInput.Text = "";
                YearInput.Text = "";
                CourseNumber.Text = "";
                Department.Text = "";
                DetailsDisplay.Text = "";
            }
        }

        /// <summary>
        /// Error checking method to determine if selenium is started.
        /// </summary>
        /// <returns></returns>
        private bool IsSeleniumStarted()
        {
            if (Driver == null)
            {
                ConnectedError.Text = "Please start selenium";
                return false;
            }
            else
            {
                ConnectedError.Text = "";
                return true;
            }
        }

        /// <summary>
        /// Called when the details button is pressed.  Calls the controller method to scrape the data, then displays it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            if (!IsSeleniumStarted())
                return;
            try
            {
                DetailsDisplay.Text = Controller.HandleDetailsClick(Department.Text.ToUpper() + CourseNumber.Text);
            }
            catch(Exception ex)
            {
                DetailsDisplay.Text = "An error occurred.  Please restart selenium\nDetails: " + ex.Message;
                Driver.Quit();
                Driver = null;
                WelcomeGrp.Show();
            }
        }

        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            CourseList.Items.Clear();
            foreach (CourseInfo Course in Controller.GetAllCourses())
                CourseList.Items.Add(Course.ToShortString());
        }
    }
}
