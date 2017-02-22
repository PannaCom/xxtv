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

        public ActionResult LoadDonors()
        {
            var data = (from p in db.donors orderby p.date descending select p).Take(9).ToList();
            return PartialView("_LoadDonors", data);
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

        public ActionResult LoadLienKetWeb()
        {
            var data = (from p in db.web_link orderby p.pos ascending select p).ToList();
            return PartialView("_LoadLienKetWeb", data);
        }

        public ActionResult donggop()
        {
            return View();
        }

        public ActionResult ProjectDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            projects_fund project = db.projects_fund.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            // load tên danh mục:
            var cat = db.project_cat.Find(project.project_cat);
            if (cat != null)
            {
                ViewBag.cat_name = cat.name;
            }
            return View(project);
        }

        public ActionResult ProjectCat(int? id, int? pg, string orderby)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var projects = db.projects_fund.Where(x => x.project_cat == id).Select(x => x);
            if (projects == null)
            {
                return HttpNotFound();
            }

            int pageSize = 18;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;

            if (orderby == null) orderby = "";
            if (orderby != null && orderby != "")
            {
                projects = projects.Where(x => x.orderby == orderby);
                ViewBag.orderby = orderby;
            }

            // load tên danh mục:
            var cat = db.project_cat.Find(id);
            if (cat != null)
            {
                ViewBag.catmodel = cat;
            }

            return View(projects.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LoadProjectCat()
        {
            var data = (from c in db.project_cat
                       select new project_cat_vm()
                       {
                           id = c.id,
                           img = c.img,
                           img2 = c.img2,
                           info = c.info,
                           name = c.name,
                           numProject = db.projects_fund.Where(x => x.project_cat == c.id).Count()
                       }).ToList();
            return PartialView("_LoadProjectCat", data);
        }
        public ActionResult LoadProjectAnother(int? id)
        {
            var data = (from p in db.projects_fund where p.id != id orderby p.date_init descending select p).Take(5).ToList();
            return PartialView("_LoadProjectAnother", data);
        }

    }
}
