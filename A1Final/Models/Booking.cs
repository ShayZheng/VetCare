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

    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Feedback = new HashSet<Feedback>();
        }
    
        public int Id { get; set; }
        [Required]
        public System.DateTime Date { get; set; }
        [Required]
        public string Descirption { get; set; }
        public string AspNetUsersId { get; set; }
        public int VetsId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Vets Vets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
