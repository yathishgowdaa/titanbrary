//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Titanbrary.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public System.Guid UserID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public Nullable<System.DateTime> MemberSince { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string SQAnwer1 { get; set; }
        public string SQAnswer2 { get; set; }
        public string SQAnswer3 { get; set; }
        public int UserType { get; set; }
    }
}
