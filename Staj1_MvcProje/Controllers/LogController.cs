using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;


namespace Staj1_MvcProje.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        MvcDbStokEntities2 db = new MvcDbStokEntities2();

        public ActionResult Index()
        {

            if (User.IsInRole("Kullanıcı"))
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                var model = db.TBLLOG.Where(m => m.KULLANICI == kullanici.AD).ToList();
                return View(model);

            }
            if (User.IsInRole("Admin"))
            {
                var degerler = db.TBLLOG.ToList();
                return View(degerler);
            }

            return View();
        }
    }
}