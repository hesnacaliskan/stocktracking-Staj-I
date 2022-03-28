using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;
using Staj1_MvcProje.Loglama;




namespace Staj1_MvcProje.Controllers
{
    [Log]
    public class UrunlerController : Controller
    {
        
        // GET: Urunler
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        
        public ActionResult Index()
        { 
            while (User.IsInRole("Kullanıcı"))
            {
                var deger = db.TBLURUNLER.Where(m => m.STATUS == true).ToList();
                return View(deger);
 
            }
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
                     
        }
        
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString(),

                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {            
            TBLKATEGORILER ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            //db.TBLURUNLER.Add(data);
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*public ActionResult ResimYukle(TBLURUNLER data)
        {

            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetFileName(Request.Files[0].FileName);
                string yol = "~/Image" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                data.RESIM = "/Image/" + dosyaadi + uzanti;
            }

            db.TBLURUNLER.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");




                    //string path = Path.Combine("~/Image" + File.FileName);
            //File.SaveAs(Server.MapPath(path));
            //data.RESIM = File.FileName.ToString();
            /*if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetFileName(Request.Files[0].FileName);
                string yol = "~/Image" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                data.RESIM = "/Image/" + dosyaadi + uzanti;
            }
        }*/

        public ActionResult SIL(int id)
        {
            /*var urun = db.TBLURUNLER.Find(id);
              db.TBLURUNLER.Remove(urun);
              db.SaveChanges();
              return RedirectToAction("Index");*/

            var urun = db.TBLURUNLER.Find(id);
            //urun.STATUS = false;
            if ((bool)(urun.STATUS = true))
            {
                urun.STATUS = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunGuncel(int id)
        {
            var urn = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString(),

                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGuncel", urn);
        
        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urn = db.TBLURUNLER.Find(p1.URUNID);
            urn.URUNAD = p1.URUNAD;
            //urn.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = ktg.KATEGORIID;
            urn.FIYAT = p1.FIYAT;
            urn.MARKA = p1.MARKA;
            urn.STOK = p1.STOK;            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles ="Admin")]
        public ActionResult KritikStok()
        {
            var kritik = db.TBLURUNLER.Where(x => x.STOK <= 50).ToList();
            return View(kritik);
        }

        public PartialViewResult StokCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = db.TBLURUNLER.Where(x => x.STOK < 50).Count();
                ViewBag.count = count;
                var azalan = db.TBLURUNLER.Where(x => x.STOK == 50).Count();
                ViewBag.azalan = azalan;
            }
            return PartialView();
        }
         public ActionResult StokGrafik()
        {
            ArrayList deger1 = new ArrayList();
            ArrayList deger2 = new ArrayList();
            var veriler = db.TBLURUNLER.ToList();
            veriler.ToList().ForEach(x => deger1.Add(x.URUNAD));
            veriler.ToList().ForEach(x => deger2.Add(x.STOK));
            var grafik = new Chart(width: 1000, height: 500).AddTitle("Ürün-stok Grafiği").AddSeries(chartType: "Column", name: "URUNAD", xValue: deger1, yValues: deger2);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

    }
}