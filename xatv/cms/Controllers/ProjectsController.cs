using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms.Models;
using PagedList;
using Newtonsoft.Json;
namespace cms.Controllers
{
    public class ProjectsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Projects/

        public ActionResult Index(string name, int? pg)
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            var p = (from q in db.projects_fund where q.name.Contains(name) orderby q.id descending select q);
            int pageSize = 25;
            int pageNumber = (pg ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Projects/Details/5

        public ActionResult Details(int id = 0)
        {
            projects_fund projects_fund = db.projects_fund.Find(id);
            if (projects_fund == null)
            {
                return HttpNotFound();
            }
            return View(projects_fund);
        }

        //
        // GET: /Projects/Create

        public ActionResult Create()
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            return View();
        }

       

        public string getcatproject(string keyword)
        {
            if (keyword == null) keyword = "";

            var p = (from q in db.project_cat orderby q.name ascending select new { label = q.name, value = q.id }).ToList().Distinct();
            return JsonConvert.SerializeObject(p);
        }
        public string gettinhthanh(string keyword)
        {
            if (keyword == null) keyword = "";

            var p = (from q in db.provinces orderby q.name ascending select new { label = q.name, value = q.id }).ToList().Distinct();
            return JsonConvert.SerializeObject(p);
        }
        //gettinhthanh

        //
        // POST: /Projects/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(projects_fundVM model)
        {
            var added = new object() { };
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            var _editor = Config.getCookie("editor");

            var user = db.editors.Where(x => x.name == _editor).Select(x => x.id).SingleOrDefault();
            if (model != null)
            {
                if (model.id == 0)
                {
                    if (model.project_cat == 0)
                    {
                        project_cat newcat = new project_cat();
                        newcat.name = model.project_cat_name ?? null;
                        db.project_cat.Add(newcat);
                        db.SaveChanges();
                        model.project_cat = newcat.id;
                        model.project_cat_name = newcat.name;
                    }

                    if (model.province_id == 0)
                    {
                        province newprovince = new province();
                        newprovince.name = model.province_name ?? null;
                        db.provinces.Add(newprovince);
                        db.SaveChanges();
                        model.province_id = newprovince.id;
                        model.province_name = newprovince.name;
                    }

                    projects_fund newproject = new projects_fund();
                    newproject.authorid = (int?)user ?? null;
                    newproject.date_finish = model.date_finish ?? null;
                    newproject.date_init = DateTime.Now;
                    newproject.des_detail = model.des_detail ?? null;
                    newproject.img = model.img ?? null;
                    newproject.info = model.info ?? null;
                    newproject.money1 = model.money1 ?? null;
                    newproject.money2 = model.money2 ?? null;
                    newproject.name = model.name ?? null;
                    newproject.project_cat = model.project_cat ?? null;
                    newproject.province_id = model.province_id ?? null;
                    newproject.view_count = 0;
                    db.projects_fund.Add(newproject);
                    db.SaveChanges();

                    added = new projects_fundVM()
                    {
                        id = newproject.id,
                        name = newproject.name,
                        money1 = newproject.money1,
                        money2 = newproject.money2,
                        img = newproject.img,
                        strdate_finish = newproject.date_finish.Value.ToString("yyyy/MM/dd"),
                        project_cat_name = model.project_cat_name,
                        project_cat = newproject.project_cat,
                        province_name = model.province_name,
                        province_id = newproject.province_id,
                        info = newproject.info,
                        des_detail = newproject.des_detail
                    };

                }
                else
                {
                    if (model.project_cat == 0)
                    {
                        project_cat newcat = new project_cat();
                        newcat.name = model.project_cat_name ?? null;
                        db.project_cat.Add(newcat);
                        db.SaveChanges();
                        model.project_cat = newcat.id;
                        model.project_cat_name = newcat.name;
                    }

                    if (model.province_id == 0)
                    {
                        province newprovince = new province();
                        newprovince.name = model.province_name ?? null;
                        db.provinces.Add(newprovince);
                        db.SaveChanges();
                        model.province_id = newprovince.id;
                        model.province_name = newprovince.name;
                    }
                    var editproject = db.projects_fund.Find(model.id);
                    if (editproject != null)
                    {
                        editproject.authorid = (int?)user ?? null;
                        editproject.date_finish = model.date_finish ?? null;
                        editproject.date_init = DateTime.Now;
                        editproject.des_detail = model.des_detail ?? null;
                        editproject.img = model.img ?? null;
                        editproject.info = model.info ?? null;
                        editproject.money1 = model.money1 ?? null;
                        editproject.money2 = model.money2 ?? null;
                        editproject.name = model.name ?? null;
                        editproject.project_cat = model.project_cat ?? null;
                        editproject.province_id = model.province_id ?? null;
                        db.Entry(editproject).State = EntityState.Modified;
                        db.SaveChanges();
                        added = new projects_fundVM()
                        {
                            id = editproject.id,
                            name = editproject.name,
                            money1 = editproject.money1,
                            money2 = editproject.money2,
                            img = editproject.img,
                            strdate_finish = editproject.date_finish.Value.ToString("yyyy/MM/dd"),
                            project_cat_name = model.project_cat_name,
                            project_cat = editproject.project_cat,
                            province_name = model.province_name,
                            province_id = editproject.province_id,
                            info = editproject.info,
                            des_detail = editproject.des_detail
                        };
                    }
                }
            }

            return Json(added, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Projects/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            projects_fund projects_fund = db.projects_fund.Find(id);
            if (projects_fund == null)
            {
                return HttpNotFound();
            }
            var data = new projects_fundVM()
            {
                id = projects_fund.id,
                name = projects_fund.name,
                money1 = projects_fund.money1,
                money2 = projects_fund.money2,
                img = projects_fund.img,
                strdate_finish = projects_fund.date_finish.Value.ToString("yyyy/MM/dd"),
                project_cat_name = db.project_cat.Find(projects_fund.project_cat).name,
                project_cat = projects_fund.project_cat,
                province_name = db.provinces.Find(projects_fund.province_id).name,
                province_id = projects_fund.province_id,
                info = projects_fund.info,
                des_detail = projects_fund.des_detail
            };

            return View(data);
        }

        //
        // POST: /Projects/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(projects_fund projects_fund)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projects_fund).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projects_fund);
        }

        //
        // GET: /Projects/Delete/5

        [HttpPost]
        public ActionResult delete(int? id)
        {
            string deleted = "0";
            var delete = db.projects_fund.Find(id);
            if (delete != null)
            {
                db.projects_fund.Remove(delete);
                db.SaveChanges();
                deleted = "1";
            }

            return Json(deleted, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}