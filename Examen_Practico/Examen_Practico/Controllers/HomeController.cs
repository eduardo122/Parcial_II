using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen_Practico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Asignaturas()
        {
            return View();
        }
        [Authorize]
        public ActionResult Agenda()
        {
            return View();
        }
        [Authorize]
        public ActionResult Lector()
        {
            return View();
        }
    }
}