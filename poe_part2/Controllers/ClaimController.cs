using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poe_part2.Data;
using poe_part2.Models;

namespace poe_part2.Controllers
{
    public class ClaimController : Controller
    {
        // GET: Claim/Create (Form for creating a new claim)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claim/Create (Handles claim submission)
        [HttpPost]
        public IActionResult Create(ClaimModel claim)
        {
            if (ModelState.IsValid)
            {
                // Auto-increment ClaimId
                claim.ClaimId = ClaimStorage.Claims.Count + 1;  // Increment based on existing claims

                // Perform the calculation: Amount = HoursWorked * HourlyRate
                claim.Amount = claim.HoursWorked * claim.HourlyRate;

                claim.Status = "Pending";  // Set status to Pending
                claim.SubmissionDate = DateTime.Now;  // Set submission date

                // Add the claim to the in-memory ClaimStorage
                ClaimStorage.Claims.Add(claim);

                // Redirect to the lecturer's status page
                return RedirectToAction("Status");
            }
            return View(claim);
        }
        //GET: View the claim status
        [HttpGet]
        public IActionResult ViewClaim(int id)
        {
            var claim = ClaimStorage.Claims.FirstOrDefault(c => c.ClaimId == id);

            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claim/Status (Lecturer's claim status page)
        public IActionResult Status()
        {
            var lecturerId = User.Identity?.Name;
            var claims = ClaimStorage.Claims
                .Where(c => c.LecturerId == lecturerId) // Get claims for logged-in lecturer
                .ToList();

            return View(claims);
        }
    }
}