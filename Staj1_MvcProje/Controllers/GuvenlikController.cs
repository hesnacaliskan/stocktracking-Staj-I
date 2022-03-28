using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Staj1_MvcProje.Models.Entity;
using Staj1_MvcProje.Loglama;

namespace Staj1_MvcProje.Controllers 
{ 
    
    [Log]
    public class GuvenlikController : Controller
    {
        MvcDbStokEntities2 db = new MvcDbStokEntities2();            
        [AllowAnonymous]
        // GET: Guvenlik
        public ActionResult GirisYap()
        {
            return View();
        }            
        
        [HttpPost]
        [AllowAnonymous] 
        public ActionResult GirisYap(TBLKULLANICI t)
        {
            var bilgiler = db.TBLKULLANICI.FirstOrDefault(x => x.AD == t.AD && x.ŞİFRE == t.ŞİFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.AD, false);
                Session["Ad"] = bilgiler.ISIM.ToString();
                Session["Soyad"] = bilgiler.SOYISIM.ToString();
                return RedirectToAction("Index", "Anasayfa");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }     

        }
        public ActionResult Cikisyap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Guvenlik");
        }

    }
}