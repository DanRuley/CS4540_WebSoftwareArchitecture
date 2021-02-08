/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Controller class for the Research Opportunity Model and CRUD Views
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using nClam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using URC.Data;
using URC.Models;
using X.PagedList;

namespace URC.Controllers
{

    /// <summary>
    /// Controller class for Research opportunities and associated CRUD items.
    /// </summary>
    public class ResearchOpportunitiesController : Controller
    {

        public static double MAXFILESIZE;

        private IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ResearchOpportunityContext _context;

        public ResearchOpportunitiesController(ResearchOpportunityContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            MAXFILESIZE = 10_000_000;
            _context = context;
            _userManager = userManager;

        }

        /// <summary>
        /// Returns opportunity main public index view
        /// Uses the PagedList library to implement table pagination and sort/search functionality.
        /// Reference: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        /// </summary>

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentFilter = searchString != null ? searchString : currentFilter;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            IQueryable<ResearchOpportunity> opps;

            if (!String.IsNullOrEmpty(searchString))
                opps = from opp in _context.ResearchOpportunities
                       where opp.ProfName.Contains(searchString) || opp.Description.Contains(searchString) || opp.Skills.Contains(searchString)
                       select opp;
            else
                opps = _context.ResearchOpportunities;
             

            switch (sortOrder)
            {
                case "name_desc":
                    opps = opps.OrderByDescending(o => o.ProfName);
                    break;
                case "Date":
                    opps = opps.OrderBy(o => o.Start);
                    break;
                case "date_desc":
                    opps = opps.OrderByDescending(o => o.Start);
                    break;
                default:
                    opps = opps.OrderBy(o => o.ProfName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(opps.ToPagedList(pageNumber, pageSize));
        }


        /// <summary>
        /// Controller for Admin list view.  Uses the PagedList library to offer table pagination and search/sorting features.
        /// Reference: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        /// </summary>
        [Authorize(Roles = "Administrator")]
        public ViewResult List(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = searchString != null ? searchString : currentFilter;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            IQueryable<ResearchOpportunity> opps;

            if (!String.IsNullOrEmpty(searchString))
                opps = from opp in _context.ResearchOpportunities
                       where opp.ProfName.Contains(searchString) || opp.ProfEmail.Contains(searchString)
                       select opp;
            else
                opps = _context.ResearchOpportunities;


            switch (sortOrder)
            {
                case "name_desc":
                    opps = opps.OrderByDescending(o => o.ProfName);
                    break;
                default:
                    opps = opps.OrderBy(o => o.ProfName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(opps.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// View controller for Details page
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchOpportunity = await _context.ResearchOpportunities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (researchOpportunity == null)
            {
                return NotFound();
            }

            return View(researchOpportunity);
        }

        /// <summary>
        /// Controller for Opportunity creation view.
        /// </summary>
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Controller for adding opportunities to DB.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ID,ProjectName,ProfName,ProfEmail,Description,StudentMentor,Start,End,PhoneNumber,Filled,WeeklyHrs,HourlyPay,ProfessorImage,Skills,Tags")] ResearchOpportunity researchOpportunity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchOpportunity);
                await _context.SaveChangesAsync();
                await CreateNotification(researchOpportunity.ID);
                return RedirectToAction(nameof(Index), new { message = "Successfully Created a Research Opportunity" });
            }

            return View(researchOpportunity);
        }

        /// <summary>
        /// Controller for the edit view
        /// </summary>
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchOpportunity = await _context.ResearchOpportunities.FindAsync(id);
            if (researchOpportunity == null)
            {
                return NotFound();
            }
            return View(researchOpportunity);
        }

        /// <summary>
        ///Controller for Edit DB mofification
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProjectName,ProfName,ProfEmail,ProfessorImage,Tags,Skills,Description,StudentMentor,Start,End,PhoneNumber,Filled,WeeklyHrs,HourlyPay")] ResearchOpportunity researchOpportunity)
        {
            if (id != researchOpportunity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchOpportunity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchOpportunityExists(researchOpportunity.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { message = "Successfully Edited a Research Opportunity" });
            }
            return View(researchOpportunity);
        }

        /// <summary>
        /// Controller for the delete admin view.
        /// </summary>
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchOpportunity = await _context.ResearchOpportunities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (researchOpportunity == null)
            {
                return NotFound();
            }

            return View(researchOpportunity);
        }

        /// <summary>
        /// Controller for deleting opportunities
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchOpportunity = await _context.ResearchOpportunities.FindAsync(id);
            _context.ResearchOpportunities.Remove(researchOpportunity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List), new { message = "Successfully Deleted a Research Opportunity" });
        }

        /// <summary>
        /// Determines if the Opp. exists
        /// </summary>
        private bool ResearchOpportunityExists(int id)
        {
            return _context.ResearchOpportunities.Any(e => e.ID == id);
        }


        /// <summary>
        /// Post function - used to upload opportunity images to the server.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
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

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Apply(int opportunityId, int applicationId)
        {
            StudentOpportunityApplied applied = new StudentOpportunityApplied
            {
                ResearchOpportunityID = opportunityId,
                StudentApplicationID = applicationId,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Status = "Applied"
            };
            await _context.AddAsync(applied);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task CreateNotification(int opportunityId)
        {   
            List<IdentityUser> users = _userManager.Users.ToList();
            foreach (IdentityUser user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Student"))
                {
                    Notification notification = new Notification
                    {
                        OpportunityID = opportunityId,
                        Read = false,
                        UserID = user.Id
                    };
                    await _context.AddAsync(notification);
                }
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Displays the Students Application that applied to for the Opportunity
        /// </summary>
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> OpportunityApplicationList()
        {
            var p = _context.ResearchOpportunities.ToList();
            // get list of all opportunities that relates to the professor user
            var name = _userManager.GetUserName(User);
            var user = await _userManager.FindByNameAsync(name);
            var opportunities = _context.ResearchOpportunities.Where(s => s.ProfEmail == user.UserName).ToList();
            var opportunityApplicationList = new List<OpportunityApplication>();

            foreach (var opportunity in opportunities)
            {
                // find all student ids relating to this opportunity
                var applied = _context.StudentOpportunityApplied.Where(s => s.ResearchOpportunityID == opportunity.ID).ToList();
                List<StudentApplication> applications = new List<StudentApplication>();
                foreach(var apply in applied)
                {
                    applications.Add(_context.StudentApplications.Find(apply.StudentApplicationID));
                }
                opportunityApplicationList.Add(new OpportunityApplication
                {
                    ID = 0,
                    OpportunityName = opportunity.ProjectName,
                    OppotunityID = opportunity.ID,
                    StudentApplication = applications
                });
            }

            return View(opportunityApplicationList);
        }

        // ChangeStatus
        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> ChangeStatus(int studentOpportunityAppliedId, string status)
        {
            var applied = _context.StudentOpportunityApplied.Where(s => s.ID == studentOpportunityAppliedId).FirstOrDefault();
            if (applied != null)
            {
                applied.Status = status;
                applied.DateUpdated = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }
    }

}
