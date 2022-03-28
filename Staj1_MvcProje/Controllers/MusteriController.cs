using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;

namespace Staj1_MvcProje.Controllers
{
    //[Authorize] 
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
       
        public ActionResult Index()
        {
            while (User.IsInRole("Kullanıcı"))
            {
                var deger = db.TBLMUSTERILER.Where(m => m.STATUS == true);
                return View(deger);
            }
            var degerler = db.TBLMUSTERILER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            //db.TBLMUSTERILER.Remove(musteri);
            if((bool)(musteri.STATUS = true))
            {
                musteri.STATUS = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
        public ActionResult MusteriGuncel(int id)
        {
            var mstr = db.TBLMUSTERILER.Find(id);
            return View("MusteriGuncel", mstr);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var mstr = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            mstr.MUSTERIAD = p1.MUSTERIAD;
            mstr.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}