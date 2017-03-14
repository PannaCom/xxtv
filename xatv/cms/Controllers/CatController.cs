using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using cms;
using cms.Models;
using System.IO;

namespace cms.Controllers
{
    public class CatController : Controller
    {
        //
        // GET: /Cat/

        private CMSEntities db = new CMSEntities();

        public ActionResult Index(int? pg)
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;

            var data = db.project_cat.OrderByDescending(x=>x.id).Select(x => x);

            return View(data.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult add()
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            return View();
        }

        public ActionResult edit(int id)
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            var editcat = db.project_cat.Find(id);
            if (editcat == null)
            {
                return RedirectToAction("Index");
            }
            return View(editcat);
        }

        public string getModalAdd()
        {
            string html = "";
            html += "<form class=\"form-horizontal\" method=\"post\" id=\"form_cat_add\" name=\"form_cat_add\" enctype=\"multipart/form-data\">"
                   + "<input type=\"hidden\" name=\"cat_id\" id=\"cat_id\" value=\"\" />"
                   + "<div class=\"form-group\">"
                   + "<div class=\"col-md-12\">"
                   + "<label class=\"control-label\">Tên danh mục: </label>"
                   + "<input type=\"text\" name=\"cat_name\" id=\"cat_name\" class=\"form-control\" placeholder=\"Tên danh mục\" />"
                   + "</div>"
                   + "</div>"
                   + "<div class=\"form-group\">"
                   + "<div class=\"col-md-12\">"
                   + "<label class=\"control-label\">Mô tả danh mục: </label>"
                   + "<textarea name=\"cat_info\" id=\"cat_info\" class=\"form-control\" placeholder=\"Mô tả danh mục\" rows=\"5\"></textarea>"
                   + "</div>"
                   + "</div>"
                   + "<div class=\"form-group\">"
                   + "<div class=\"col-md-6\">"
                   + "<label class=\"control-label\">Ảnh đại diện </label>"
                   + "<input type=\"hidden\" name=\"cat_img1\" id=\"cat_img1\" />"
                   + "<input type=\"file\" name=\"file_cat_img1\" id=\"file_cat_img1\" class=\"form-control\" />"
                   + "</div>"
                   + "</div>"
                   + "<div class=\"form-group\">"
                   + "<div class=\"col-md-12\">"
                   + "<label class=\"control-label\">Ảnh nền: </label>"
                   + "<input type=\"hidden\" name=\"cat_img2\" id=\"cat_img2\" />"
                   + "<input type=\"hidden\" name=\"file_cat_img2\" id=\"file_cat_img2\" class=\"form-control\" />"
                   + "</div>"
                   + "</div>"
                   + "<button type=\"button\" class=\"btn btn-primary\" id=\"btn_cat_add\" onclick=\"cat_save(event);\">Cập nhật</button>"
                   + "</form>";

            return html;
        }


        [HttpPost]
        public ActionResult add(project_cat model)
        {
            var added = new object() { };
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login", "Admin"); }
            if (model != null)
            {
                if (model.id == 0)
                {
                    project_cat newcat = new project_cat();
                    newcat.name = model.name ?? null;
                    newcat.info = model.info ?? null;
                    newcat.img = model.img ?? null;
                    newcat.img2 = model.img2 ?? null;
                    db.project_cat.Add(newcat);
                    db.SaveChanges();
                    added = newcat;
                }
                else
                {
                    var editcat = db.project_cat.Find(model.id);
                    editcat.name = model.name ?? null;
                    editcat.info = model.info ?? null;
                    editcat.img = model.img ?? null;
                    editcat.img2 = model.img2 ?? null;
                    db.Entry(editcat).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    added = editcat;
                }
            }

            return Json(added, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult delete(int? id)
        {
            string deleted = "0";
            var deletecat = db.project_cat.Find(id);
            if (deletecat != null)
            {
                db.project_cat.Remove(deletecat);
                db.SaveChanges();
                deleted = "1";
            }

            return Json(deleted, JsonRequestBehavior.AllowGet);
        }

    }
}
