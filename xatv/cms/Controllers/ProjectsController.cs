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
    public class ProjectsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Projects/

        public ActionResult Index(string name, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            var p = (from q in db.projects_fund where q.name.Contains(name) select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
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
            return View();
        }

        //
        // POST: /Projects/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(projects_fund projects_fund)
        {
            if (ModelState.IsValid)
            {
                db.projects_fund.Add(projects_fund);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projects_fund);
        }

        //
        // GET: /Projects/Edit/5

        public ActionResult Edit(int id = 0)
        {
            projects_fund projects_fund = db.projects_fund.Find(id);
            if (projects_fund == null)
            {
                return HttpNotFound();
            }
            return View(projects_fund);
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

        public ActionResult Delete(int id = 0)
        {
            projects_fund projects_fund = db.projects_fund.Find(id);
            if (projects_fund == null)
            {
                return HttpNotFound();
            }
            return View(projects_fund);
        }

        //
        // POST: /Projects/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            projects_fund projects_fund = db.projects_fund.Find(id);
            db.projects_fund.Remove(projects_fund);
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