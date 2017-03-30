using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManagerAPI.Models
{
    public class Interest
    {
        //Scalar properties
        public int UserId { get; set; }
        public int PropertyId { get; set; }

        //Navigation properties
        public virtual User User { get; set; }
        public virtual Property Property { get; set; }


    }
}