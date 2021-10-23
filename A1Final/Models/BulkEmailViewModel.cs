using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A1Final.Models
{
    public class BulkEmailViewModel
    {
        
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }
    }
}