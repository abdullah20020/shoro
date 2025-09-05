using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.Entities.Users
{
    public class LawFirm
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
        public string Website { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public bool IsApproved { get; set; }

        // Relationships
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public ICollection<Lawyer> Lawyers { get; set; } = new HashSet<Lawyer>();
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
