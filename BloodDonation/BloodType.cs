//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodDonation
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public partial class BloodType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BloodType()
        {
            //this.Donors = new HashSet<Donor>();
            //this.Plasmas = new HashSet<Plasma>();
            //this.Trombocites = new HashSet<Trombocite>();
            //this.Patients = new HashSet<Patient>();
            //this.RedBloodCells = new HashSet<RedBloodCell>();
        }

        [DataMember(Name = "BloodTypeID")]
        public System.Guid BloodTypeID { get; set; }

        [DataMember(Name = "Blood")]
        public string Blood { get; set; }

        [DataMember(Name = "RH")]
        public Nullable<bool> RH { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Donor> Donors { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Plasma> Plasmas { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Trombocite> Trombocites { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Patient> Patients { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<RedBloodCell> RedBloodCells { get; set; }
    }
}
