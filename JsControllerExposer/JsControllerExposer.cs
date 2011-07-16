using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace JsControllerExposer {
    public class JsControllerExposer : Controller {
        private static Stream defaultjs;
        private static Stream createController;
        private static Stream createRoutes;
        private static Stream createControllerActionFunction;

        [HttpGet]
        public ActionResult Index() {
            try {
                var myAssembly = FindApplicationAssembly();
                var controllers = myAssembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Controller)) && !type.IsSubclassOf(typeof(JsControllerExposer)));
                var actionResultType = typeof(ActionResult);
                var modelControllers = controllers.Select(controller =>
                    new Models.Controller(controller.Name, controller.GetMethods().Where(method => method.ReturnType == actionResultType)));
                var js = new StringBuilder();

                var defaultjsReader = new StreamReader(defaultjs);
                var createControllerReader = new StreamReader(createController);
                var createRoutesReader = new StreamReader(createRoutes);
                var createControllerActionFunctionReader = new StreamReader(createControllerActionFunction);

                js.Append(defaultjsReader.ReadToEnd());

                string str = createControllerReader.ReadToEnd();
                foreach (var controller in modelControllers){
                    var strFormated = string.Format(str, controller.Name);
                    strFormated = strFormated.Replace("@", "{").Replace("#", "}");
                    js.AppendLine(strFormated);
                }

                str = createRoutesReader.ReadToEnd();
                foreach (var controller in modelControllers) {
                    foreach (var action in controller.Actions) {
                        var strFormated = string.Format(str, controller.Name, action.Name, action.Route, action.Method);
                        strFormated = strFormated.Replace("@", "{").Replace("#", "}");
                        js.AppendLine(strFormated);
                    }
                }

                str = createControllerActionFunctionReader.ReadToEnd();
                foreach (var controller in modelControllers) {
                    foreach (var action in controller.Actions) {
                        var strFormated = string.Format(str, controller.Name, action.Name);
                        strFormated = strFormated.Replace("@", "{").Replace("#", "}");
                        js.AppendLine(strFormated);
                    }
                }

                defaultjsReader.Close(); createControllerReader.Close(); createRoutesReader.Close(); createControllerActionFunctionReader.Close();

                defaultjs.Close(); createController.Close(); createRoutes.Close(); createControllerActionFunction.Close();

                return Content(js.ToString(), "application/javascript");
            } catch (NullReferenceException) {
                return Content("You do not have no one class of type MvcApplication");
            }
        }

        private static Assembly FindApplicationAssembly() {
            var executingAssembly = Assembly.GetExecutingAssembly();

            defaultjs = executingAssembly.GetManifestResourceStream("JsControllerExposer.defaultjs.txt");
            createController = executingAssembly.GetManifestResourceStream("JsControllerExposer.CreateController.txt");
            createRoutes = executingAssembly.GetManifestResourceStream("JsControllerExposer.CreateRoutes.txt");
            createControllerActionFunction = executingAssembly.GetManifestResourceStream("JsControllerExposer.CreateControllerActionFunction.txt");

            var dllPath = new FileInfo(executingAssembly.CodeBase.Replace("file:///", "")).Directory.FullName;
            var dlls = Directory.EnumerateFiles(dllPath, "*.dll");
            Assembly myAssembly = null;
            foreach (var dll in dlls) {
                var assembly = Assembly.LoadFile(dll);
                var type = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals("MvcApplication"));
                if (type != null)
                    myAssembly = assembly;
            }
            return myAssembly;
        }
    }
}
