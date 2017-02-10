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
using Excel;
using System.Web.Helpers;
using System.Collections;
namespace cms.Controllers
{
    public class ThongKeController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /ThongKe/

        public ActionResult Index(string name,string fromdate,string todate, int? page)
        {
            string fdate = fromdate;
            string tdate = todate;

            DateTime d1 = Config.toDateYMD(fdate);
            DateTime d2 = Config.toDateYMD(tdate);
            if (fdate == null) d1 = DateTime.Now.AddDays(-365);
            if (tdate == null) d2 = DateTime.Now;
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            ViewBag.fromdate = d1.Month.ToString("00") + "/" + d1.Day.ToString("00") + "/" + d1.Year;
            ViewBag.todate = d2.Month.ToString("00") + "/" + d2.Day.ToString("00") + "/" + d2.Year;
            var p = (from q in db.saokes where q.des.Contains(name) && q.date>=d1 && q.date<=d2 select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult List(string name, string fromdate, string todate, int? page)
        {
            string fdate = fromdate;
            string tdate = todate;

            DateTime d1 = Config.toDateYMD(fdate);
            DateTime d2 = Config.toDateYMD(tdate);
            if (fdate == null) d1 = DateTime.Now.AddDays(-365);
            if (tdate == null) d2 = DateTime.Now;
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            ViewBag.fromdate = d1.Month.ToString("00") + "/" + d1.Day.ToString("00") + "/" + d1.Year;
            ViewBag.todate = d2.Month.ToString("00") + "/" + d2.Day.ToString("00") + "/" + d2.Year;
            var p = (from q in db.saokes where q.des.Contains(name) && q.date >= d1 && q.date <= d2 select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        //
        // GET: /ThongKe/Details/5

        public ActionResult Details(int id = 0)
        {
            saoke saoke = db.saokes.Find(id);
            if (saoke == null)
            {
                return HttpNotFound();
            }
            return View(saoke);
        }

        //
        // GET: /ThongKe/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ThongKe/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(saoke saoke)
        {
            if (ModelState.IsValid)
            {
                db.saokes.Add(saoke);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saoke);
        }

        //
        // GET: /ThongKe/Edit/5

        public ActionResult Edit(int id = 0)
        {
            saoke saoke = db.saokes.Find(id);
            if (saoke == null)
            {
                return HttpNotFound();
            }
            return View(saoke);
        }

        //
        // POST: /ThongKe/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(saoke saoke)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saoke).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saoke);
        }

        //
        // GET: /ThongKe/Delete/5

        public ActionResult Delete(int id = 0)
        {
            saoke saoke = db.saokes.Find(id);
            if (saoke == null)
            {
                return HttpNotFound();
            }
            return View(saoke);
        }

        //
        // POST: /ThongKe/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            saoke saoke = db.saokes.Find(id);
            db.saokes.Remove(saoke);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public string autosearch(string keyword) {
            var p = (from q in db.saokes where q.des.Contains(keyword) select q.des).ToList();
            return JsonConvert.SerializeObject(p);
        }
        public string uploadFile(HttpPostedFileBase file)
        {
            try
            {
                string physicalPath = HttpContext.Server.MapPath("../" + Config.FileImagePath + "\\");
                string nameFile = String.Format("{0}.xlsx", Guid.NewGuid().ToString());
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
                //return Config.FileImagePath + "/" + nameFile;
                foreach (var worksheet in Workbook.Worksheets(fullPath))
                {
                    foreach (var row in worksheet.Rows)
                    {
                        string abc = "";
                        DateTime d1 = DateTime.Now;
                        decimal money = 0;
                        string des = "";
                        foreach (var cell in row.Cells)
                        {
                            if (cell == null) continue;
                            if (cell.ColumnIndex == 0) d1 = Config.toDate(cell.Text);
                            if (cell.ColumnIndex == 3 && cell.Text != "")
                            {
                                try
                                {
                                    decimal.TryParse(cell.Text, out money);
                                }
                                catch (Exception ex)
                                {
                                    money = 0;
                                }
                            }
                            if (cell.ColumnIndex == 5) des = cell.Text;
                            //if ((cell.ColumnIndex == 0 || cell.ColumnIndex == 3 || cell.ColumnIndex == 5) && cell.Text != null) abc += cell.Text.ToString();
                            if (cell.ColumnIndex == 5)
                            {
                                if (d1.Year != DateTime.Now.AddDays(-30000).Year && des != "")
                                {
                                    bool p = db.saokes.Any(o => o.des.Contains(des));
                                    if (!p && money != 0)
                                    {
                                        saoke sk = new saoke();
                                        sk.date = d1;
                                        sk.date_id = int.Parse(Config.convertToDateTimeId(d1.ToString()));
                                        sk.money = money;
                                        sk.des = des;
                                        db.saokes.Add(sk);
                                        db.SaveChanges();
                                    }
                                }
                                //money = 0;
                                //des = "";
                                break;
                            }
                            //sb.Append(abc + "\r\n<br>");
                        }

                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        public class saokechart
        {
            public int mo { get; set; }
            public decimal money { get; set; }
           
        }
        //chart
        public ActionResult chart() {
            //string[] mo = new string[] { };// "Peter", "bbbbb", "Julie", "Mary", "Dave" 
            //string[] money = new string[] { };// "2", "6", "4", "5", "3" 
            //ArrayList mo = new ArrayList();
            //ArrayList money = new ArrayList();
            //var data = db.Database.SqlQuery<saokechart>("select mo,sum(money) as money from (select  Month(date) as mo,money from saoke) as A group by mo", "USA");
            //foreach (var item in data)
            //{
            //    mo.Add(item.mo.ToString());
            //    money.Add(item.money.ToString());
            //}
            //string[] smo = (String[])mo.ToArray(typeof(string));
            //string[] smoney = (String[])money.ToArray(typeof(string));
            //var myChart = new Chart(width:800, height: 400)
            //.AddTitle("Thống kê quỹ theo tháng")
            //.AddSeries(
            //    name: "Tháng",
            //    xValue: smo,
            //    yValues: smoney)
            //.Write();
            //ViewBag.chart1 = myChart;
            //myChart = new Chart(width: 800, height: 400)
            //.AddTitle("Thống kê quỹ theo tiền")
            //.AddSeries(
            //    name: "Tiền",
            //    xValue: new[] {"90000"},
            //    yValues: new[] { "80000" })
            //.Write();
            //ViewBag.chart2 = myChart;
            return View();
        }
    }
}