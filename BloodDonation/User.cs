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

    public partial class User
    {
        public User()
        {

        }

        public User(string username, string password, string email, string role, string cnp)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Role = role;
            this.CNP = cnp;
        }
        [DataMember(Name = "Username")]
        public string Username { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "Role")]
        public string Role { get; set; }

        [DataMember(Name = "CNP")]
        public string CNP { get; set; }
    }
}
