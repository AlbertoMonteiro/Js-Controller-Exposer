using System.Web.Mvc;

namespace JsControllerExpose.Example.Controllers {
    public class MyHomeController : Controller {
        
        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }
        
        [HttpGet]
        public ActionResult About() {
            return Json(new { name = "Alberto"}, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult About2() {
            return Content("");
        }

        [HttpPut]
        public ActionResult About3() {
            return Content("");
        }
    }
}
