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

    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            //this.BloodDonationHistories = new HashSet<BloodDonationHistory>();
        }

        public Patient(string cnp, string lastName, string firstName, Guid hospitalID, Guid bloodTypeID)
        {
            CNP = cnp;
            LastName = lastName;
            FirstName = firstName;
            HospitalID = hospitalID;
            BloodTypeID = bloodTypeID;
        }

        [DataMember(Name = "CNP")]
        public string CNP { get; set; }

        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "HospitalID")]
        public Nullable<System.Guid> HospitalID { get; set; }

        [DataMember(Name = "BloodTypeID")]
        public Nullable<System.Guid> BloodTypeID { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BloodDonationHistory> BloodDonationHistories { get; set; }
        //public virtual BloodType BloodType { get; set; }
        //public virtual Hospital Hospital { get; set; }
        //public virtual Request Request { get; set; }
    }
}
