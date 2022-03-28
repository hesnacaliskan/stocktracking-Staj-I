using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Staj1_MvcProje.Models.Entity;

namespace Staj1_MvcProje.Loglama
{
    public class LogAttribute : FilterAttribute, IActionFilter
    {
        #region IActionFilter Members
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            MvcDbStokEntities2 db = new MvcDbStokEntities2();
            TBLLOG LogBilgi = new TBLLOG();

            LogBilgi.TARIH = DateTime.Now;
            LogBilgi.METOT = filterContext.ActionDescriptor.ActionName;
            LogBilgi.METIN = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            LogBilgi.KULLANICI = filterContext.HttpContext.User.Identity.Name;

            db.TBLLOG.Add(LogBilgi);
            db.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MvcDbStokEntities2 db = new MvcDbStokEntities2();
            TBLLOG LogBilgi = new TBLLOG();

            LogBilgi.TARIH = DateTime.Now;
            LogBilgi.METOT = filterContext.ActionDescriptor.ActionName;
            LogBilgi.METIN = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            LogBilgi.KULLANICI = filterContext.HttpContext.User.Identity.Name;

            db.TBLLOG.Add(LogBilgi);
            db.SaveChanges();
        }
    }
    #endregion
}