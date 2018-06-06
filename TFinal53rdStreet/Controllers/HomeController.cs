using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFinal53rdStreet.Models;

namespace TFinal53rdStreet.Controllers
{
    public class HomeController : Controller
    {
        private MusicalDB db = new MusicalDB();
        public ActionResult Index()
        {
            var MusicalList = db.Musical.ToList().OrderBy(m => m.Title);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}