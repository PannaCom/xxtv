//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cms.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class daugia
    {
        public long id { get; set; }
        public Nullable<int> news_id { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public Nullable<decimal> price { get; set; }
        public string says { get; set; }
        public Nullable<System.DateTime> date_time { get; set; }
    }
}
