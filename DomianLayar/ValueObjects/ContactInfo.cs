using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.ValueObjects
{
    public class ContactInfo
    {
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }

        private ContactInfo() { }

        public ContactInfo(string phone, string email, string website)
        {
            Phone = phone;
            Email = email;
            Website = website;
        }
    }
}
