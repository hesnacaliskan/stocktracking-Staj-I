using Staj1_MvcProje.Loglama;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staj1_MvcProje.Controllers
{
    [Log]
    public class AnasayfaController : Controller
    {
        
        // GET: Anasayfa
        public ActionResult Index()
        {
            return View();
        }
    }
}