using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;
namespace Staj1_MvcProje.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        public ActionResult Index()
        {
            var degerler = db.TBLKULLANICI.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KayitOl()
        {
            
            return View();
        }
        
        [HttpPost]
        public ActionResult KayitOl(TBLKULLANICI p1)
        {
            if (!ModelState.IsValid)
            { 
                return View("KayitOl");

            }
            db.TBLKULLANICI.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
            
        }


    }     
}
