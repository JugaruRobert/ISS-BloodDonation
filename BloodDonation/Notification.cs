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

    public partial class Notification
    {
        [DataMember(Name = "NotificationID")]
        public System.Guid NotificationID { get; set; }

        [DataMember(Name = "DonorCNP")]
        public string DonorCNP { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        public Notification() { }
        public Notification(Guid notificationID, string donorCNP, string description, string status)
        {
            NotificationID = notificationID;
            DonorCNP = donorCNP;
            Description = description;
            Status = status;
        }



        //public virtual Donor Donor { get; set; }
    }
}
