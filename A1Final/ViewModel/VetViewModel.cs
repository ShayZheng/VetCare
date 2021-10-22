using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A1Final.ViewModel
{
    public class VetViewModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Location { get; set; }
        
        
        public decimal Latitude { get; set; }
        
        
        public decimal Longitude { get; set; }
    }
}