using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace Staj1_MvcProje.Controllers
{
    public class TumSatislarController : Controller
    {
        // GET: TumSatislar
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        
        public ActionResult Index()
        {
            var degerler = db.TBLSATISLAR.ToList();
            return View(degerler);
        }
    }
}