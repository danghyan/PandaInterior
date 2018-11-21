using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaStroi.Models;


namespace PandaStroi.Controllers
{
    public class HomeController : Controller
    {
        PandaContext p = new PandaContext();

        public ActionResult Index()
        {
            return View();
        }


        public void SetCallback(string fio, string phone, string email)
        {
            Callback c = new Callback
            {
                FIO = fio,
                Phone = phone,
                email = email,
                StatusId = 1,
            };
            p.Callback.Add(c);
            p.SaveChanges();
            return;
        }

    }
}