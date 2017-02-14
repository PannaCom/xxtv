using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using System.Text;
using cms.Models;
using PagedList;
using Newtonsoft.Json;
namespace cms.Controllers
{
    public class HomeController : Controller
    {
        CMSEntities db = new CMSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadNewItems()
        {
            var data = (from n in db.news_item orderby n.id select n).Skip(3).Take(5).ToList();
            return PartialView("_LoadNewItems", data);
        }

        public ActionResult LoadNewTopItem()
        {
            var data = (from n in db.news_item orderby n.id select n).Take(3).ToList();
            return PartialView("_LoadNewTopItem", data);
        }

        public ActionResult LoadProjectXayTruong()
        {
            var data = (from p in db.projects_fund orderby p.date_init descending select p).Take(6).ToList();
            return PartialView("_LoadProjectXayTruong", data);
        }

        //public ActionResult Index(string name, int? page)
        //{
        //    if (name == null) name = "";
        //    name = name.Replace("%20", " ");
        //    name = name.Trim();
        //    ViewBag.name = name;
        //    try
        //    {
        //        var sk = (from q in db.saokes select q).OrderByDescending(o => o.id).Take(10);
        //        ViewBag.sk = sk.ToList();
        //        decimal total = (decimal)db.saokes.Sum(o => o.money);
        //        ViewBag.total = total;
        //        var projects = (from q in db.projects_fund select q).OrderByDescending(o => o.id).Take(10);
        //        ViewBag.projects = projects;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    var p = (from q in db.news_item where q.title.Contains(name) select q).OrderByDescending(o => o.id).Take(100);
        //    int pageSize = 6;
        //    int pageNumber = (page ?? 1);
        //    return View(p.ToPagedList(pageNumber, pageSize));
        //}

        //public ActionResult LoadNew

        public ActionResult Gift(int? page,int? menu_id)
        {
            var p = (from q in db.news_item where q.menu_id==menu_id select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.menu_id = menu_id;
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        public string Add_New_Gopy(string name, string phone, string message) {
            try
            {
                gopy gy = new gopy();
                gy.name = name;
                gy.phone = phone;
                gy.message = message;
                db.gopies.Add(gy);
                db.SaveChanges();
                return "1";
            }
            catch (Exception) {
                return "0";
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

    }
}
