//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sales_Rep.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Login
    {
        public int ID { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
        public int Status { get; set; }
        public string Role { get; set; }
        public System.DateTime Enter_Date { get; set; }
        public string Enter_User { get; set; }
        public Nullable<int> Login_Id { get; set; }
    }
}
