using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poe_part2.Models
{
    public class ClaimModel
    {
        public int ClaimId { get; set; }  // Unique identifier for the claim
        public string? LecturerId { get; set; }  // Identifier for the lecturer (could be a username or user ID)
        public decimal Amount { get; set; }  // The total amount of the claim
        public int HoursWorked { get; set; }  // Number of hours worked
        public decimal HourlyRate { get; set; }  // The hourly rate of the lecturer
        public string? Status { get; set; }  // Status of the claim (e.g., "Pending", "Approved", "Rejected")
        public string? SupportingDocumentPath { get; set; }  // Path to any supporting document uploaded by the lecturer
        public DateTime SubmissionDate { get; set; }  // The date and time the claim was submitted
    }
}