using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string Governorate { get; private set; }

        private Address() { } 

        public Address(string street, string district, string city, string governorate)
        {
            Street = street;
            District = district;
            City = city;
            Governorate = governorate;
        }
    }
}
