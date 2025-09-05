using DomianLayar.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DomianLayar.Entities
{
    public class comment : BaseClass
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        // FK: User
        public int? UserId { get; set; }
        public client User { get; set; }
        public int? LawyerId { get; set; }
        public int? LawFirmId { get; set; }

        // Navigation
        public Lawyer Lawyer { get; set; }

        public LawFirm LawFirm { get; set; }


        // Self Relation (Replies)
        public int? ParentCommentId { get; set; }
        public comment ParentComment { get; set; }
        public ICollection<comment> Replies { get; set; } = new HashSet<comment>();
    }
}
