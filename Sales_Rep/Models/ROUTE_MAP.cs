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
    
    public partial class ROUTE_MAP
    {
        public int RM_ID { get; set; }
        public Nullable<int> RM_ROUTE_NO { get; set; }
        public string RM_DESTINATION_BEGIN { get; set; }
        public string RM_DESTINATION_END { get; set; }
        public Nullable<int> RM_LENGTH { get; set; }
        public Nullable<int> RM_STATUS { get; set; }
    }
}