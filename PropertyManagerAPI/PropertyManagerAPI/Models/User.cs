using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManagerAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public bool IsLandlord { get; set; }
        [Required]
        public string UserName { get; set; }


        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PropertySearch> PropertySearches { get; set; }

    }
}