using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms.Models;
using System.Security.Cryptography;

namespace cms.Controllers
{
    public class AdminController : Controller
    {
        private CMSEntities db = new CMSEntities();
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (Config.getCookie("editor") == "") { return RedirectToAction("Login"); }
            return View();
        }

        public ActionResult Login()
        {
            if (Config.getCookie("editor") != "")
            {
                return RedirectToAction("Index"); 
            }
            return View();
        }

        [HttpPost]
        public string Login(editor login)
        {
            MD5 md5Hash = MD5.Create();
            login.pass = Config.GetMd5Hash(md5Hash, login.pass);
            var p = (from q in db.editors where q.name.Contains(login.name) && q.pass.Contains(login.pass) select q.name).SingleOrDefault();
            if (p != null && p != "")
            {
                //Ghi ra cookie
                Config.setCookie("editor", p);
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public ActionResult logout()
        {
            Config.RemoveCookie("editor");
            return RedirectToAction("Login");
        }

    }
}
