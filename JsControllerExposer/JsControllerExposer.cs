using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace JsControllerExposer {
    public class JsControllerExposer : Controller {

        [HttpGet]
        public ActionResult Index() {
            try {
                var myAssembly = FindApplicationAssembly();
                var controllers = myAssembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Controller)) && !type.IsSubclassOf(typeof(JsControllerExposer)));
                var actionResultType = typeof(ActionResult);
                var json = controllers.Select(controller =>
                    new Models.Controller(controller.Name, controller.GetMethods().Where(method => method.ReturnType == actionResultType)));
                return Json(json, JsonRequestBehavior.AllowGet);
            } catch (NullReferenceException) {
                return Content("You do not have no one class of type MvcApplication");
            }
        }

        private static Assembly FindApplicationAssembly(){
            var executingAssembly = Assembly.GetExecutingAssembly();
            var dllFile = new FileInfo(executingAssembly.CodeBase.Replace("file:///", ""));
            var dlls = Directory.EnumerateFiles(dllFile.Directory.FullName, "*.dll");
            Assembly myAssembly = null;
            foreach (var dll in dlls){
                var assembly = Assembly.LoadFile(dll);
                var type = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals("MvcApplication"));
                if (type != null)
                    myAssembly = assembly;
            }
            return myAssembly;
        }
    }
}
