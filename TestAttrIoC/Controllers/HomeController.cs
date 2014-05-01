using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAttrIoC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IFirmRepository firm)
        {
            Trace.TraceInformation("HomeController -> IFirmRepository #" + firm.GetHashCode());
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
