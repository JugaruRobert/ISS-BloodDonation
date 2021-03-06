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

    public partial class Plasma
    {
        public Plasma()
        {

        }

        public Plasma(Guid plasmaID, Guid bankID, Guid bloodTypeID, DateTime expiration, int quantity)
        {
            PlasmaID = plasmaID;
            BankID = bankID;
            BloodTypeID = bloodTypeID;
            ExpirationDate = expiration;
            Quantity = quantity;
        }

        [DataMember(Name = "PlasmaID")]
        public System.Guid PlasmaID { get; set; }

        [DataMember(Name = "BankID")]
        public Nullable<System.Guid> BankID { get; set; }

        [DataMember(Name = "BloodTypeID")]
        public Nullable<System.Guid> BloodTypeID { get; set; }

        [DataMember(Name = "ExpirationDate")]
        public Nullable<System.DateTime> ExpirationDate { get; set; }

        [DataMember(Name = "Quantity")]
        public Nullable<int> Quantity { get; set; }
    
        //public virtual BloodBank BloodBank { get; set; }
        //public virtual BloodType BloodType { get; set; }
    }
}
