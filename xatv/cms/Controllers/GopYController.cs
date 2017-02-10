using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms.Models;
using PagedList;

namespace cms.Controllers
{
    public class GopYController : Controller
    {
        //
        // GET: /GopY/
        CMSEntities db = new CMSEntities();
        public ActionResult Index(string name, int? page)
        {
            if (name == null) name = "";
            name = name.Replace("%20", " ");
            name = name.Trim();
            ViewBag.name = name;
            var p = (from q in db.gopies where q.name.Contains(name) select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

    }
}
