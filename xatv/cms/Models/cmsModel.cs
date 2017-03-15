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

    public class project_cat_vm
    {
        public int id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string img { get; set; }
        public string img2 { get; set; }
        public int numProject { get; set; }
    }

    public class projects_fundVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> money1 { get; set; }
        public Nullable<decimal> money2 { get; set; }
        public string img { get; set; }
        public Nullable<System.DateTime> date_finish { get; set; }
        public string strdate_finish { get; set; }
        public Nullable<int> project_cat { get; set; }
        public string project_cat_name { get; set; }
        public Nullable<int> province_id { get; set; }
        public string province_name { get; set; }
        public string info { get; set; }
        public string des_detail { get; set; }
    }

}