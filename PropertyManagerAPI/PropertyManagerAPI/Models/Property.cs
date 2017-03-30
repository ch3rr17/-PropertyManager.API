using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManagerAPI.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public string PropertyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContactPhone { get; set; }
        public decimal Rent { get; set; }
        public int SquareFootage { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public bool IsPetFriendly { get; set; }
        public int LeaseTerm { get; set; }
        public string PropertyImage { get; set; }


        public virtual User User { get; set; }
        
        public virtual ICollection<Interest> Interests { get; set; }



    }
}