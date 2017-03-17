using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManagerAPI.Models
{
    public class PropertySearch
    {
        public int PropertySearchId { get; set; }
        [Required]
        public int UserId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int MinimumRent { get; set; }
        public int MaximumRent { get; set; }
        public int SquareFootage { get; set; }
        public bool IsPetFriendly { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }

        public virtual User User { get; set; }
  
    }
}