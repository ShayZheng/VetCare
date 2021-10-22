using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A1Final.ViewModel
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        
        public int Rating { get; set; }

        public string Comments { get; set; }
        public string AspNetUsersId { get; set; }
        public int VetsId { get; set; }
    }
}