using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = System.Web.Mvc.Controller;

namespace JavascriptControllersExposer.Controllers {
    public class JavascriptExposerController : Controller {
        //
        // GET: /JavascriptExposer/
        [HttpGet]
        public ActionResult Index() {
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Controller)));
            var type1 = typeof(ActionResult);
            var json = controllers
                .Select(controller =>
                    new Models.Controller(controller.Name, controller.GetMethods().Where(method => method.ReturnType == type1))
                    );
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}
