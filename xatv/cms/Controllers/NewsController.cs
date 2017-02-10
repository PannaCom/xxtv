using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms.Models;
using PagedList;
namespace cms.Controllers
{
    public class NewsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /News/

        public ActionResult Index(string name, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            var p = (from q in db.news_item where q.title.Contains(name) && q.deleted == 0 select q).OrderBy(o => o.title).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            news_item news_item = db.news_item.Find(id);
            if (news_item == null)
            {
                return HttpNotFound();
            }
            var p = (from q in db.news_item where q.id != id select q).OrderByDescending(o => o.id).Take(10);
            ViewBag.news = p;
            if (news_item.menu_id == 6) {
                ViewBag.todate = news_item.date_time_dau_gia;
                var p2 = (from q in db.daugias where q.news_id == id select q).OrderByDescending(o => o.id).Take(1000).ToList();
                string daugialist = "";
                for (int i = p2.Count-1; i>=0; i--)
                {
                    daugialist += "<div class=bgheader4><div class=chatLine1><span class=userchat2><b><i><a href=\"https://www.facebook.com/" + p2[i].user_id + "\"><img src=\"http://graph.facebook.com/" + p2[i].user_id + "/picture\">" + p2[i].user_name + "</a></i></b></span></div><div class=chatLine2><span class=chatcontent>: ra giá mua <b>" + string.Format("{0:#,#}", p2[i].price) + "</b>," + p2[i].says + "(<i>"+p2[i].date_time+"</i>)</span></a></div></div>";
                }
                ViewBag.daugialist = daugialist;
                var user_win = (from q in db.daugias where q.news_id == id select q).OrderByDescending(o => o.price).FirstOrDefault();
                if (user_win != null)
                {
                    ViewBag.pricemax = user_win.price;
                    ViewBag.usermax = "<b><i><a href=\"https://www.facebook.com/" + user_win.user_id + "\"><img src=\"http://graph.facebook.com/" + user_win.user_id + "/picture\">" + user_win.user_name + "</a></i></b>";
                }
                else {
                    ViewBag.pricemax = 0;
                    ViewBag.usermax = "";
                }
                
            }
            
            return View(news_item);
        }
        [HttpPost]
        public string updateUser(string user, string id) {
            try
            {
               
                bool f = db.members.Any(o => o.user_name.Contains(user) && o.user_id.Contains(id));
                if (!f)
                {
                    member mb = new member();
                    mb.user_name = user;
                    mb.user_email = user;
                    mb.user_id = id;
                    db.members.Add(mb);
                    db.SaveChanges();
                }
                Config.setCookie("user_name", user);
                Config.setCookie("user_id", id);
                return "1";
            }
            catch (Exception ex) {
                return "";
            }
            return "";
        }
        [HttpPost]
        public string getUserMax(int id) {
            try
            {
                var user_win = (from q in db.daugias where q.news_id == id select q).OrderByDescending(o => o.price).FirstOrDefault();
                //ViewBag.pricemax = user_win.price;
                if (user_win != null)
                {
                    return "Giá cao nhất:" + string.Format("{0:#,#}", user_win.price) + ",<b><i><a href=\"https://www.facebook.com/" + user_win.user_id + "\"><img src=\"http://graph.facebook.com/" + user_win.user_id + "/picture\">" + user_win.user_name + "</a></i></b>";
                }
                else return "";
            }
            catch (Exception ex) {
                return "";
            }
        }
        //
        // GET: /News/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(news_item news_item)
        {
            if (ModelState.IsValid)
            {
                news_item.deleted = 0;
                news_item.datetime = DateTime.Now;
                news_item.datetimeid = Config.datetimeid();
                news_item.status = 0;
                news_item.total_views = 0;
                news_item.user_post = "";
                //news_item.modules_id = 0;
                db.news_item.Add(news_item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news_item);
        }
        [HttpPost]
        public string DauGia_Add_New(decimal price,string says,int news_id) {
            string user_name = Config.getCookie("user_name");
            try{
                if (user_name != "")
                {
                    
                    daugia dg = new daugia();
                    dg.date_time = DateTime.Now;
                    dg.news_id = news_id;
                    dg.price = price;
                    dg.says = says;
                    dg.user_email = "";
                    dg.user_id = Config.getCookie("user_id");
                    dg.user_name = user_name;
                    db.daugias.Add(dg);
                    db.SaveChanges();
                    return "1";
                }
            }catch(Exception ex){
                return "0";
            }
            return "0";
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcess(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.NewsImagePath + "\\");
            string nameFile = String.Format("{0}.jpg", Guid.NewGuid().ToString());
            int countFile = Request.Files.Count;
            string fullPath = physicalPath + System.IO.Path.GetFileName(nameFile);
            for (int i = 0; i < countFile; i++)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                Request.Files[i].SaveAs(fullPath);
                break;
            }
            //string ok = resizeImage(Config.imgWidthNews, Config.imgHeightNews, fullPath, Config.NewsImagePath + "/" + nameFile);
            return Config.NewsImagePath + "/" + nameFile;
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcessContent(HttpPostedFileBase file, string filename)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.NewsImagePath + "\\");
            string nameFile = String.Format("{0}.jpg", Guid.NewGuid().ToString());
            int countFile = Request.Files.Count;
            string fullPath = physicalPath + System.IO.Path.GetFileName(nameFile);
            string content = "";
            for (int i = 0; i < countFile; i++)
            {
                nameFile = String.Format("{0}.jpg", filename + "-" + Guid.NewGuid().ToString());
                fullPath = physicalPath + System.IO.Path.GetFileName(nameFile);
                content += "<img src=\"" + Config.NewsImagePath + "/" + nameFile + "\" width=200 height=126>";
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                Request.Files[i].SaveAs(fullPath);
                //break;
            }
            //string ok = resizeImage(Config.imgWidthNews, Config.imgHeightNews, fullPath, Config.NewsImagePath + "/" + nameFile);
            //return Config.NewsImagePath + "/" + nameFile;
            return content;
        }
        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            news_item news_item = db.news_item.Find(id);
            if (news_item == null)
            {
                return HttpNotFound();
            }
            return View(news_item);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(news_item news_item)
        {
            if (ModelState.IsValid)
            {
                news_item.deleted = 0;                
                news_item.status = 0;                
                news_item.user_post = "";
                //news_item.modules_id = 0;
                db.Entry(news_item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news_item);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            news_item news_item = db.news_item.Find(id);
            if (news_item == null)
            {
                return HttpNotFound();
            }
            return View(news_item);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            news_item news_item = db.news_item.Find(id);
            db.news_item.Remove(news_item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}