//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace A1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Feedback
    {
        public int Id { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        public string Comments { get; set; }
        public string AspNetUsersId { get; set; }
        public int VetsId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Vets Vets { get; set; }
    }
}
