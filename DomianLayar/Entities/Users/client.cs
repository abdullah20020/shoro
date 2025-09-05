using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.Entities.Users
{
    public class client
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
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public ICollection<Case> Cases { get; set; } = new HashSet<Case>();
        public ICollection<comment> Comments { get; set; } = new HashSet<comment>();
    }
}
