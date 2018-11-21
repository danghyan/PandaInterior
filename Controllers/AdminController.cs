using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PandaStroi.Models;

namespace PandaStroi.Controllers
{
    public class AdminController : Controller
    {
        PandaContext p = new PandaContext();

        [HttpGet]
        public ActionResult Auth()
        {
            return View();
        }


        public ActionResult Index(string username, string pass)
        {
            var user = p.Users.ToList().Where(x => x.login == username).FirstOrDefault();
            try
            {
                if (Cookie.get("SessionId").ToString() == Session.SessionID.ToString())
                    return View();
            }
            catch
            {
                //нечего. мы попытались по крайней мере
            }

            //проверка пользователя и задание пароля
            if (user == default(Users))
                return RedirectToAction("Auth");
            else if (user.pass == null && !string.IsNullOrEmpty(pass))
            {
                user.pass = MD5Hash(pass);
                p.SaveChanges();
                return RedirectToAction("Auth");
            }
            else if(user.pass != MD5Hash(pass))
                return RedirectToAction("Auth");

            
            HttpCookie c = new HttpCookie("SessionId")
            {
                Value = Session.SessionID,
                Domain = Request.Url.Host,
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(2)
            };
            Cookie.set(c);

            Session.Add("UserId", user.UserId);

            user.enterdt = DateTime.Now;
            p.SaveChanges();

            return View();
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public JsonResult getCallbacks([DataSourceRequest]DataSourceRequest request)
        {
            return Json(p.Callback.Select(x => new {
                x.CallbackId,
                x.Dt,
                x.email,
                x.FIO,
                x.isActive,
                x.Phone,
                x.StatusId ,
                StatusList = new { x.StatusList.Name },
                x.Comment,
                Log = x.Log.Select(a => new { a.Users.login, dt = a.dt.ToString(), a.Operation }).ToList(),
                login = new { x.Log.OrderByDescending(a => a.LogId).FirstOrDefault().Users.login } }).OrderByDescending(x => x.CallbackId).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult statuslistJson()
        {
            return Json(p.StatusList.Select(x => new { x.Name, x.StatusId }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveInfo(int CallbackId,int? StatusId, string comment)
        {
            var s = p.Callback.Where(x => x.CallbackId == CallbackId).FirstOrDefault();
            if(s.StatusId != StatusId)
            {
                Log l = new Log
                {
                    Operation = "Status:" + p.StatusList.Where(x => x.StatusId == StatusId).FirstOrDefault().Name,
                    UserId = Convert.ToInt32(Session["UserId"].ToString()),
                    CallbackId = CallbackId
                };
                p.Log.Add(l);
            }
            if(s.Comment != comment)
            {
                Log l = new Log
                {
                    Operation = "Comment:" + comment,
                    UserId = Convert.ToInt32(Session["UserId"].ToString()),
                    CallbackId = CallbackId
                };
                p.Log.Add(l);
            }
            s.StatusId = StatusId;
            s.Comment = comment;
            p.SaveChanges();
           
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }





    }
}