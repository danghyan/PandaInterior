using System;
using System.Web;
namespace PandaStroi
{
    public class Cookie
    {
        public static HttpCookie get(string name)
        {
            if (string.IsNullOrEmpty(name) || HttpContext.Current == null || HttpContext.Current.Response == null)
                return null;
            HttpCookie cookie = null;
            int i = Array.IndexOf(HttpContext.Current.Response.Cookies.AllKeys, name);
            if (i >= 0)
            {
                cookie = HttpContext.Current.Response.Cookies.Get(i);
                return cookie.Expires < DateTime.Now ? null : cookie;
            }
            i = Array.IndexOf(HttpContext.Current.Request.Cookies.AllKeys, name);
            if (i < 0)
                return null;
            cookie = HttpContext.Current.Request.Cookies.Get(i);
            return cookie;
        }
        public static void set(HttpCookie cookie)
        {
            if (string.IsNullOrEmpty(cookie.Name) || HttpContext.Current == null || HttpContext.Current.Response == null)
                return;
            if (Array.IndexOf(HttpContext.Current.Response.Cookies.AllKeys, cookie.Name) < 0)
                HttpContext.Current.Response.Cookies.Add(cookie);
            else
                HttpContext.Current.Response.Cookies.Set(cookie);
            if (Array.IndexOf(HttpContext.Current.Request.Cookies.AllKeys, cookie.Name) >= 0)
                HttpContext.Current.Request.Cookies.Remove(cookie.Name);
            HttpContext.Current.Request.Cookies.Add(cookie);
        }
        public static void del(string name)
        {
            if (string.IsNullOrEmpty(name) || HttpContext.Current == null || HttpContext.Current.Response == null)
                return;
            if (Array.IndexOf(HttpContext.Current.Response.Cookies.AllKeys, name) >= 0)
                HttpContext.Current.Response.Cookies.Remove(name);
            HttpCookie cookie = new HttpCookie(name)
            {
                Expires = DateTime.Now.AddDays(-1),
                Value = null,
            };

            HttpContext.Current.Response.Cookies.Add(cookie);

            if (Array.IndexOf(HttpContext.Current.Request.Cookies.AllKeys, name) >= 0)
                HttpContext.Current.Request.Cookies.Remove(name);
        }
    }
}
