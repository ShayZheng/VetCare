using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A1Final.Models
{
    public class InteractiveTable
    {
        public Booking bookingdetails { get; set; }
        public AspNetUsers userdetails { get; set; }

        public Vets vetdetails { get; set; }
    }
}