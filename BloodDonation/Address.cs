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

    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            //this.Donors = new HashSet<Donor>();
            //this.BloodBanks = new HashSet<BloodBank>();
            //this.Hospitals = new HashSet<Hospital>();
            //this.Donors1 = new HashSet<Donor>();
        }

        public Address(Guid addressID, string address, Guid cityID)
        {
            this.AddressID = addressID;
            this.Address1 = address;
            this.CityID = cityID;
        }

        [DataMember(Name = "AddressID")]
        public System.Guid AddressID { get; set; }

        [DataMember(Name = "Address")]
        public string Address1 { get; set; }

        [DataMember(Name = "CityID")]
        public Nullable<System.Guid> CityID { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Donor> Donors { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BloodBank> BloodBanks { get; set; }
        //public virtual City City { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Hospital> Hospitals { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Donor> Donors1 { get; set; }
    }
}
