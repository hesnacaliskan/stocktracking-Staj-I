using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;
using PagedList;
using PagedList.Mvc;
using Staj1_MvcProje.Loglama;

namespace Staj1_MvcProje.Controllers
{
    [Log]
    public class SatisController : Controller
    {
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        
        // GET: Satis
        public ActionResult Index(int sayfa=1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                var model = db.TBLSATISLAR.Where(m => m.KullanıcıID == kullanici.ID).ToList().ToPagedList(sayfa,5);
                return View(model);

            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p1)
        {
            db.TBLSATISLAR.Add(p1);
            db.SaveChanges();
            return View("Index");
        }

        [HttpGet]
        public ActionResult SatinAl(int id)
        {
            var model = db.TBLSEPETT.FirstOrDefault(m => m.ID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SatinAl2(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.TBLSEPETT.FirstOrDefault(m => m.ID == id);

                    var satis = new TBLSATISLAR
                    {
                        KullanıcıID = model.KullanıcıID,
                        URUN = model.UrunID,
                        ADET = model.ADET,
                        FIYAT = model.FIYAT,
                        TARIH = model.TARIH,
                        STATUS = true,
                        SEBEP = "a"
                    };                 
                    db.TBLSEPETT.Remove(model);
                    db.TBLSATISLAR.Add(satis);
                    db.SaveChanges();
                    ViewBag.islem = "Satın Alma İşleminiz Başarıyla Gerçekleşmiştir";
                }
            }
            catch(Exception)
            {
                ViewBag.islem = "Satın Alma Başarısız";
            }

            return View("islem");
        }
        
        
        [HttpGet]
        public ActionResult HepsiniSatinAl(decimal? Tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                var model = db.TBLSEPETT.Where(m => m.KullanıcıID == kullanici.ID).ToList();
                var kid = db.TBLSEPETT.FirstOrDefault(m => m.KullanıcıID == kullanici.ID);
                if (model != null)
                {
                    if (kid == null)
                    {
                        ViewBag.Tutar = "Sepetinizde Ürün Bulunmamaktadır";
                    }
                    else if (kid != null)
                    {
                        Tutar = db.TBLSEPETT.Where(m => m.KullanıcıID == kid.KullanıcıID).Sum(m => m.TBLURUNLER.FIYAT * m.ADET);
                        ViewBag.Tutar = "Toplam Tutar = " + Tutar + "TL";
                    }
                    return View(model);
                }
                return View();
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult HepsiniSatinAl2()
        {
            var username = User.Identity.Name;
            var kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == username);
            var model = db.TBLSEPETT.Where(m => m.KullanıcıID == kullanici.ID).ToList();
            int satir = 0;

            foreach(var item in model)
           {
                var satis = new TBLSATISLAR
               {
                    KullanıcıID = model[satir].KullanıcıID,
                    URUN = model[satir].UrunID,
                    ADET = model[satir].ADET,
                    FIYAT = model[satir].FIYAT,
                    TARIH = DateTime.Now,

               };
                db.TBLSATISLAR.Add(satis);
                db.SaveChanges();
                satir++;                    
           }
            db.TBLSEPETT.RemoveRange(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Sepet");
            
        }
    }
}