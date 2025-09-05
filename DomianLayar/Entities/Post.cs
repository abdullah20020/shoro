using DomianLayar.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.Entities
{
    public class Post : BaseClass
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        // FKs
        public int? LawyerId { get; set; }
        public int? UserId { get; set; }
        public int? LawFirmId { get; set; }

        // Navigation
        public Lawyer Lawyer { get; set; }
        public client User { get; set; }
        public LawFirm LawFirm {get; set;}


       
    }
}
