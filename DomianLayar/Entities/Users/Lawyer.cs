using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DomianLayar.Entities.Users
{
    public class Lawyer 
    {
        public int Id { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        public string Photo { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }
        public string Phone { get; set; }
        public string CV { get; set; }
        public string Experience { get; set; }
        public string Type { get; set; }
        public string AnotherEmail { get; set; }
        public bool IsApproved { get; set; }
        public bool Available { get; set; }

        // Relationships
        public int? LawFirmId { get; set; }
        public LawFirm LawFirm { get; set; }
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }

}
