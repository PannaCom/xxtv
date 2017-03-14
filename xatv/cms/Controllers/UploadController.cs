using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cms.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult UploadPhoto()
        {
            bool isSaved = true;
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images", Server.MapPath(@"\")));
                        string strDay = DateTime.Now.ToString("yyyyMM");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), strDay);

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                        
                        file.SaveAs(path);
                        fName = "/Images/" + strDay + "/" + _fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                Config.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Máy chủ đang quá tải, vui lòng quay lại sau ít phút." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
