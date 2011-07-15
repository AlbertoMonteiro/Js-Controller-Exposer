using System.Web.Mvc;

namespace JavascriptControllersExposer.Controllers {
    public class HomeController : Controller {
        
        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }
        
        [HttpGet]
        public ActionResult About(int id, string nome) {
            return View();
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
