using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms.Models
{
    public class newItems
    {
        public int id { get; set; }
        public string title { get; set; }
        public string des { get; set; }
        public string full_content { get; set; }
        public string image { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> deleted { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public Nullable<int> datetimeid { get; set; }
        public Nullable<int> total_views { get; set; }
        public string user_post { get; set; }
        public Nullable<int> menu_id { get; set; }
        public Nullable<System.DateTime> date_time_dau_gia { get; set; }
    }
}