using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poe_part2.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } // e.g., Lecturer, Coordinator, Admin
    }
}