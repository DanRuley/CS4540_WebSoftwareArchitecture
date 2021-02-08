/*
 Author:    Dan Ruley
 Partner:   None
 Date:      September, 2020
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

 I, Dan Ruley, certify that I wrote this code from scratch and did
 not copy it in part or whole from another source.  Any references used
 in the completion of the assignment are cited in my README file and in
 the appropriate method header.

 This is the Controller class that helps display the page view to the user.  It is pretty basic for the time being.
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using URC.Models;
using Microsoft.AspNetCore.Http;
using URC.Data;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using X.PagedList;
using System.Text.RegularExpressions;

namespace URC.Controllers
{
    public struct Event
    {
        public string Title { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// Controller class for the /Home/ project files.
    /// </summary>
    public class HomeController : Controller
    {
       
        UserManager<IdentityUser> userManager;
        ILogger<HomeController> _logger;
        private readonly ResearchOpportunityContext _context;

        public HomeController(ILogger<HomeController> logger, ResearchOpportunityContext context, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this._context = context;
            _logger = logger;
        }
        /// <summary>
        /// Display the Home Page
        /// </summary>
        /// <returns>Page View</returns>
        public IActionResult Index()
        {
            string url = "https://www.trumba.com/calendars/uu-college-of-engineering-electrical-computer-engineering.rss";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            List<Event> list = new List<Event>();
            foreach(SyndicationItem item in feed.Items)
            {
                string title = item.Title.Text;
                string dateStr = item.Summary.Text;
                string date = dateStr.Split(new char[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)[0];
                Match m = Regex.Match(date.Substring(date.Length - 6), @"[,][ ]\d{4}");
                if (m.Success)
                    date = date.Substring(0, date.Length - 6);
                //if(date.Substring(date.Length-6))
                list.Add(new Event { Title = title, Date = date }); 
            }
            return View(list);
        }


        /// <summary>
        /// Display the Privacy Page
        /// </summary>
        /// <returns>Page View</returns>
        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// Display the Opportunities Page
        /// </summary>
        /// <returns>Page View</returns>
        public IActionResult Handmade_Opportunities()
        {
            return View();
        }


        /// <summary>
        /// Display the Details Page
        /// </summary>
        /// <returns>Page View</returns>
        public IActionResult Handmade_Details()
        {
            return View();
        }

        /// <summary>
        /// Roles table
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ViewResult RolesTable(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.CurrentFilter = searchString != null ? searchString : currentFilter;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            List<IdentityUser> users = new List<IdentityUser>();
            
            if (!String.IsNullOrEmpty(searchString)) {
                //make sure to allow any capitalization for roles while searching - the DB is case sensitive so we need to normalize the search string first.
                searchString = searchString.ToLower() == "admin" || searchString.ToLower() == "administrator" ? "Administrator" : searchString;
                searchString = searchString.ToLower() == "student" ? "Student" : searchString;
                searchString = searchString.ToLower() == "professor" ? "Professor" : searchString;

                foreach (var usr in userManager.Users)
                    if (userManager.IsInRoleAsync(usr, searchString).Result)
                        users.Add(usr);
                    else if (usr.Email.Contains(searchString))
                        users.Add(usr);
            }
            else {
                users = userManager.Users.ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users.Sort((u1, u2) => u2.Email.CompareTo(u1.Email));
                    break;
                default:
                    users.Sort((u1, u2) => u1.Email.CompareTo(u2.Email));
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
        }
       


        /// <summary>
        /// Method I made for myself to delete users
        /// </summary>
        /// <param name="_ud"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteUser(UserData _ud)
        {
            try
            {
                var user = userManager.FindByNameAsync(_ud.userName).Result;

                userManager.DeleteAsync(user).Wait();

            }
            catch (Exception ex)
            {
                return Json("Error", ex.Message);
            }

            return Json("Success");
        }
        /// <summary>
        /// HTTP Post function for AJAX DB call triggered by user input on Roles Table view.
        /// </summary>
        /// <param name="_ud"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ChangeRole(UserData _ud)
        {
            try
            {
                var user = userManager.FindByNameAsync(_ud.userName).Result;

                if (_ud.action == "add")
                    userManager.AddToRoleAsync(user, _ud.role).Wait();

                else
                    userManager.RemoveFromRoleAsync(user, _ud.role).Wait();

            }
            catch (Exception ex)
            {
                return Json("Error", ex.Message);
            }

            return Json("Success");
        }

        /// <summary>
        /// Error View
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Clears the notification (by setting Read to true) related to the id
        /// </summary>
        /// <param name="_ud"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ClearNotification(int notificationId)
        {
            try
            {
                var notification = _context.Notifications.Find(notificationId);
                notification.Read = true;
                _context.Update(notification);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json("Error", ex.Message);
            }

            return Json("Success");
        }

        /// <summary>
        /// Clears the notification (by setting Read to true) related to the id
        /// </summary>
        /// <param name="_ud"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ClearAllNotification(string userId)
        {
            try
            {
                var notifications = _context.Notifications.Where(n => n.UserID == userId && n.Read == false);
                foreach(var notification in notifications)
                {
                    notification.Read = true;
                    _context.Update(notification);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json("Error", ex.Message);
            }
            return Json("Success");
        }
    }

    /// <summary>
    /// Class for passing AJAX User data from view to controller
    /// </summary>
    public class UserData
    {
        public string userName { get; set; }
        public string role { get; set; }
        public string action { get; set; }
    }
}
