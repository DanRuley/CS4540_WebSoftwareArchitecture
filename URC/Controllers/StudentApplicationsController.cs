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

 StudentApplications Controller.  Displays page views, performs some authentication checks, and also provides HTTPPost functions.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using nClam;
using URC.Data;
using URC.Models;
using X.PagedList;

namespace URC.Controllers
{

    [Produces("application/json")]
    public class StudentApplicationsController : Controller
    {
        private ResearchOpportunityContext _context;
        private IConfiguration _configuration;
        public static double MAXFILESIZE;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// The controller takes the DB context and the configuration.
        /// </summary>
        public StudentApplicationsController(ResearchOpportunityContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            MAXFILESIZE = 10_000_000;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays the list view of all applications in the DB.
        /// </summary>
        [Authorize(Roles = "Professor, Administrator")]
        /// <summary>
        /// Returns opportunity main public index view
        /// Uses the PagedList library to implement table pagination and sort/search functionality.
        /// Reference: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        /// </summary>

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UIDSortParm = sortOrder == "UID" ? "uid_desc" : "UID";
            ViewBag.CurrentFilter = searchString != null ? searchString : currentFilter;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            IQueryable<StudentApplication> apps;

            if (!String.IsNullOrEmpty(searchString))
                apps = from app in _context.StudentApplications
                       where app.UID.Contains(searchString) || app.Name.Contains(searchString)
                       select app;
            else
                apps = _context.StudentApplications;


            switch (sortOrder)
            {
                case "name_desc":
                    apps = apps.OrderByDescending(a => a.Name);
                    break;
                case "UID":
                    apps = apps.OrderBy(a => a.UID);
                    break;
                case "uid_desc":
                    apps = apps.OrderByDescending(a => a.UID);
                    break;
                default:
                    apps = apps.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(apps.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Displays the details view.
        /// </summary>
        public async Task<IActionResult> Details(int? id, string? message)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentApplication = await _context.StudentApplications
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentApplication == null)
            {
                return NotFound();
            }

            ViewBag.Message = message != null ? message : "";
            return View(studentApplication);
        }

        /// <summary>
        /// Displays the Create view.
        /// </summary>
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Display the Student Search view.
        /// </summary>
        [Authorize(Roles = "Professor")]
        public IActionResult StudentSearch()
        {
            return View();
        }

        /// <summary>
        /// HTTP post function for creating applications.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Skills,ResumeFile,Interests,GradDate,CompletedCourses,Degree,GPA,UID,Name,Email,HoursPerWeek,PersonalStatement")] StudentApplication studentApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentApplication);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = studentApplication.ID, message = "Successfully Created an Application" });
            }
            return View(studentApplication);
        }

        /// <summary>
        /// Display the edit view.
        /// </summary>
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentApplication = await _context.StudentApplications.FindAsync(id);
            if (studentApplication == null)
            {
                return NotFound();
            }
            return View(studentApplication);
        }

       /// <summary>
       /// HttpPost for the application edit page.
       /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Skills,ResumeFile,Interests,GradDate,CompletedCourses,Degree,GPA,Active,UID,Name,Email,HoursPerWeek,PersonalStatement,AppDate,LastModified")] StudentApplication studentApplication)
        {
            if (id != studentApplication.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentApplicationExists(studentApplication.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                ViewBag.Message = "Successfully Edited an Application";
                return View(studentApplication);
            }
            return View(studentApplication);
        }

        /// <summary>
        /// Displays the delete view
        /// </summary>
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentApplication = await _context.StudentApplications
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentApplication == null)
            {
                return NotFound();
            }

            return View(studentApplication);
        }

        /// <summary>
        /// Endpoint for the Student Summary search API
        /// </summary>
        [HttpPost]
        public JsonResult GetStudentSummary(string SearchString)
        {
            if (SearchString == null)
                return Json("Error");

            SearchString = SearchString.ToLower();
            List<StudentSummary> Summaries = new List<StudentSummary>();
            HashSet<string> SearchKeys = new HashSet<string>(SearchString.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries));
            bool found;

            foreach (StudentApplication sa in _context.StudentApplications)
            {
                found = false;
                foreach (string s in sa.Skills.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    if (SearchKeys.Contains(s.ToLower()))
                    {
                        Summaries.Add(new StudentSummary(sa));
                        found = true;
                        break;
                    }
                if (!found)
                {
                    foreach (string s in sa.Interests.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        if (SearchKeys.Contains(s.ToLower()))
                        {
                            Summaries.Add(new StudentSummary(sa));
                            break;
                        }
                }
            }
            return Json(Summaries);
        }

        /// <summary>
        /// Deletes the application from the DB.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentApplication = await _context.StudentApplications.FindAsync(id);
            _context.StudentApplications.Remove(studentApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Does the application exist?
        /// </summary>
        private bool StudentApplicationExists(int id)
        {
            return _context.StudentApplications.Any(e => e.ID == id);
        }

        /// <summary>
        /// Toggles the application status to active or inactive depending on input.
        /// </summary>
        [HttpPost]
        public IActionResult ToggleApplicationStatus(int id, string type)
        {
            try
            {
                var studentApplication = _context.StudentApplications
                .FirstOrDefaultAsync(m => m.ID == id).Result;

                if (type == "apply")
                    studentApplication.Active = true;
                else
                    studentApplication.Active = false;

                _context.Update(studentApplication);
                _context.SaveChangesAsync().Wait();

                Thread.Sleep(1200);
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception ex)
            {
                return Json("Error", ex.Message);
            }
        }


        /// <summary>
        /// Http post that uploads the given resume file to the DB.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UploadResume(List<IFormFile> files)
        {
            try
            {
                if (files.Count != 1)
                    return BadRequest(new { message = "Must Submit One File" });

                IFormFile f = files[0];

                //Scan file for viruses
                var ms = new MemoryStream();
                f.OpenReadStream().CopyTo(ms);
                byte[] filebytes = ms.ToArray();
                var clam = new ClamClient("localhost", 3310);
                var scanresult = await clam.SendAndScanFileAsync(filebytes);

                switch (scanresult.Result)
                {
                    case ClamScanResults.Clean:
                        break;
                    case ClamScanResults.VirusDetected:
                        return BadRequest(new { message = "Virus Detected" }); ;
                    case ClamScanResults.Error:
                        return BadRequest(new { message = "Error, something went wrong while trying to scan the upload for viruses " });
                    default:
                        break;
                }

                // Validation
                long Length = f.Length;
                if (Length > MAXFILESIZE)
                    return BadRequest(new { message = "File must be smaller than 10MB." });
                else if (Length < 1)
                    return BadRequest(new { message = "File size cannot be 0" });
                else if (!(f.FileName.EndsWith(".pdf") || f.FileName.EndsWith(".jpg") || f.FileName.EndsWith(".png") || f.FileName.EndsWith(".jpeg") || f.FileName.EndsWith(".PNG")))
                    return BadRequest(new { message = "Unsupported file format: Please upload a pdf, jpeg or png file" });


                // Save to filesystem
                var FilePath = Path.Combine(_configuration["StoreFilePath"], f.FileName);
                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    await f.CopyToAsync(stream);
                }
                return Ok(new { message = f.FileName });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error, something went wrong with upload: " + ex.Message });
            }
        }


        /// <summary>
        /// Displays the list of applied opportunities that application is connected to.
        /// </summary>
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> ApplicationStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _context.StudentOpportunityApplied.Where(s => s.StudentApplicationID == id).OrderByDescending(s => s.DateUpdated).ToListAsync());
        }

    }

    /// <summary>
    /// Student summary object - returned in a JSON serialized version from the student search.
    /// </summary>
    public class StudentSummary
    {
        public string name { get; set; }
        public int ID { get; set; }
        public double GPA { get; set; }
        public string Statement_Summary { get; set; }

        public StudentSummary(StudentApplication Student)
        {
            name = Student.Name;
            ID = Student.ID;
            GPA = Student.GPA;
            //The average word is around 5 chars. This is a rough approximation to limit how much data is returned.
            Statement_Summary = Student.PersonalStatement.Length > 500 ?
                Student.PersonalStatement.Substring(0, 500) + "..." : Student.PersonalStatement;
        }
    }
}
