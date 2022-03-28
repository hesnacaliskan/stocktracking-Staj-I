using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;
using Staj1_MvcProje.Loglama;
using PagedList;
using PagedList.Mvc;

namespace Staj1_MvcProje.Controllers
{
    
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORILER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGuncel(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGuncel", ktgr);
        }
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    } 
}