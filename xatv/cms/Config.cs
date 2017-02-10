using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace cms
{
    public class Config
    {
        public static string NewsImagePath = "/images/news";
        public static string FileImagePath = "/images/file/";
        
        public static int datetimeid()
        {
            DateTime d1;
            try
            {

                d1 = DateTime.Now;//.ToUniversalTime();
                string rs = d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
                return int.Parse(rs);

            }
            catch (Exception ex)
            {
                d1 = DateTime.Now;//.ToUniversalTime();
                string rs = d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
                return int.Parse(rs);
            }
            //return DateTime.Now.Year * 365 + DateTime.Now.Month * 30 + DateTime.Now.Day;
        }
        public static string convertToDateTimeId(string d)
        {
            DateTime d1;
            try
            {
                d1 = DateTime.Parse(d);//ToUniversalTime();
                return d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
            }
            catch (Exception ex)
            {
                d1 = DateTime.Now;
                return d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
            }
            d1 = DateTime.Now;
            return d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
        }
        public static DateTime toDate(string d) {
            try
            {
                //DateTime d1 = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);//DateTime.Parse(d);//ToUniversalTime();
                DateTime d1 = DateTime.FromOADate(double.Parse(d));
                return d1;
            }
            catch (Exception ex)
            {
                return DateTime.Now.AddDays(-30000);
            }
        }
        public static DateTime toDateYMD(string d)
        {
            try
            {
                //d = d.Replace("/", "-");
                DateTime d1 = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);//yyyy/MM/dd
                return d1;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
        //convert tieng viet thanh khong dau va them dau -
        public static string unicodeToNoMark(string input)
        {
            input = input.ToLowerInvariant().Trim();
            if (input == null) return "";
            string noMark = "a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,e,u,u,u,u,u,u,u,u,u,u,u,u,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,i,i,i,i,i,i,y,y,y,y,y,y,d,A,A,E,U,O,O,D";
            string unicode = "a,á,à,ả,ã,ạ,â,ấ,ầ,ẩ,ẫ,ậ,ă,ắ,ằ,ẳ,ẵ,ặ,e,é,è,ẻ,ẽ,ẹ,ê,ế,ề,ể,ễ,ệ,u,ú,ù,ủ,ũ,ụ,ư,ứ,ừ,ử,ữ,ự,o,ó,ò,ỏ,õ,ọ,ơ,ớ,ờ,ở,ỡ,ợ,ô,ố,ồ,ổ,ỗ,ộ,i,í,ì,ỉ,ĩ,ị,y,ý,ỳ,ỷ,ỹ,ỵ,đ,Â,Ă,Ê,Ư,Ơ,Ô,Đ";
            string[] a_n = noMark.Split(',');
            string[] a_u = unicode.Split(',');
            for (int i = 0; i < a_n.Length; i++)
            {
                input = input.Replace(a_u[i], a_n[i]);
            }
            input = input.Replace("  ", " ");
            input = Regex.Replace(input, "[^a-zA-Z0-9% ._]", string.Empty);
            input = removeSpecialChar(input);
            input = input.Replace(" ", "-");
            input = input.Replace("--", "-");
            return input;
        }
        public static string removeSpecialChar(string input)
        {
            input = input.Replace("-", "").Replace(":", "").Replace(",", "").Replace("_", "").Replace("'", "").Replace("\"", "").Replace(";", "").Replace("”", "").Replace(".", "").Replace("%", "").Replace("&", "");
            return input;
        }
        public static void setCookie(string field, string value)
        {
            HttpCookie MyCookie = new HttpCookie(field);
            MyCookie.Value = HttpUtility.UrlEncode(value);
            MyCookie.Expires = DateTime.Now.AddDays(365);
            HttpContext.Current.Response.Cookies.Add(MyCookie);
            //Response.Cookies.Add(MyCookie);           
        }
        public static string getCookie(string v)
        {
            try
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[v].Value.ToString());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}