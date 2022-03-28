using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;
using Staj1_MvcProje.Loglama;


namespace Staj1_MvcProje.Controllers
{
    [Log]
    public class SepetController : Controller
    {
        // GET: Sepet
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        public ActionResult Index(decimal?Tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                string kullaniciadi = User.Identity.Name;
                TBLKULLANICI kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                List<TBLSEPETT> model = db.TBLSEPETT.Where(m => m.KullanıcıID == kullanici.ID).ToList();
                TBLSEPETT kid = db.TBLSEPETT.FirstOrDefault(m => m.KullanıcıID == kullanici.ID);

                if (model!=null)
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
            }
            return HttpNotFound();
        }

        public ActionResult SepeteEkle(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string kullaniciadi = User.Identity.Name;
                TBLKULLANICI model = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                TBLURUNLER u = db.TBLURUNLER.Find(id);
                TBLSEPETT sepet = db.TBLSEPETT.FirstOrDefault(m => m.KullanıcıID == model.ID && m.UrunID == id);
                if (model != null)
                {
                    if (sepet != null)
                    {
                        sepet.ADET++;
                        sepet.FIYAT = u.FIYAT * sepet.ADET;
                        db.SaveChanges();
                        return RedirectToAction("Index","Urunler");
                    }
                    TBLSEPETT s = new TBLSEPETT
                    {
                        KullanıcıID = model.ID,
                        UrunID = u.URUNID,
                        ADET = 1,
                        FIYAT = u.FIYAT,
                        TARIH = DateTime.Now
                    };                    
                    db.TBLSEPETT.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index","Urunler");
                }
                return View();
            }
            return HttpNotFound();
        }
        
        public ActionResult SepetCount(int?count)
        {
            if(User.Identity.IsAuthenticated)
            {
                TBLKULLANICI model = db.TBLKULLANICI.FirstOrDefault(m => m.AD == User.Identity.Name);
                count = db.TBLSEPETT.Where(m => m.KullanıcıID == model.ID).Count();
                ViewBag.count = count;
                if (count == 0)
                {
                    ViewBag.count = "";
                }
                return PartialView();
            }
            return HttpNotFound();
        }
        
        public ActionResult Azalt(int id)
        {
            TBLSEPETT model = db.TBLSEPETT.Find(id);
            if (model.ADET==1)
            {
                db.TBLSEPETT.Remove(model);
                db.SaveChanges();
            }
            model.ADET--;
            model.FIYAT = model.FIYAT * model.ADET;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Arttir(int id)
        {
            TBLSEPETT model = db.TBLSEPETT.Find(id);
            model.ADET++;
            model.FIYAT = model.FIYAT * model.ADET;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void AdetYaz(int id,int miktari)
        {
            TBLSEPETT model = db.TBLSEPETT.Find(id);            
            model.ADET = miktari;
            model.FIYAT = model.FIYAT * model.ADET;
            db.SaveChanges();

        }
        public ActionResult SIL(int id)
        {
            TBLSEPETT sepet = db.TBLSEPETT.Find(id);
            db.TBLSEPETT.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult HepsiniSil()
        {
            if (User.Identity.IsAuthenticated)
            {
                string kullaniciadi = User.Identity.Name;
                TBLKULLANICI model = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                var sil = db.TBLSEPETT.Where(m => m.KullanıcıID == model.ID);
                db.TBLSEPETT.RemoveRange(sil);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return HttpNotFound();
        }

    }
}

