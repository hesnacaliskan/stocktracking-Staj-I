using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;


namespace Staj1_MvcProje.Controllers
{
    public class IptalController : Controller
    {
        // GET: Iptal
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        public ActionResult Index()
        {
            if (User.IsInRole("Kullanıcı"))
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.TBLKULLANICI.FirstOrDefault(m => m.AD == kullaniciadi);
                var model = db.TBLSATISLAR.Where(m => m.KullanıcıID == kullanici.ID).ToList();
                return View(model);
            }
            else
            {
                var degerler = db.TBLSATISLAR.Where(m => m.STATUS == false).ToList();
                return View(degerler);
            }

        }

        [HttpGet]
        public ActionResult IPTAL()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult IPTAL(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TBLSATISLAR model = db.TBLSATISLAR.Find(id);
                    if ((bool)(model.STATUS = true || (bool)(model.STATUS = null)))
                    {
                        model.STATUS = false;                   

                    }                    
                    db.SaveChanges();
                    ViewBag.islem = "İptal Etme İşleminiz Başarıyla Gerçekleşmiştir";                  

                }
            }
            catch (Exception)
            {
                ViewBag.islem = "İptal Etme İşleminiz Başarısız, Lütfen İptal Etme Sebebini Giriniz";
            }

            return View("islem");

        }
        /*[HttpGet]
        public ActionResult Sebep()
        {            
            return PartialView("Sebep");
        }*/
        //[HttpPost]
        /*public ActionResult Sebep2(int id)
        {
            var iptal = db.TBLSATISLAR.Find(id);
            //var model = db.TBLSATISLAR.Where(m => m.SATISID == data.SATISID).FirstOrDefault();
            iptal.SEBEP = (string)Session["SEBEP"]; 
            db.SaveChanges();
            return PartialView();

        }*/

    }
}