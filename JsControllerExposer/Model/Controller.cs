using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JsControllerExposer.Models {
    public class Controller {
        public Controller(string name, IEnumerable<MethodInfo> methodInfos) {
            Name = name;
            Actions = new List<ControllerAction>();
            foreach (var controllerAction in methodInfos.Select(methodInfo => new ControllerAction(Name, methodInfo.Name, methodInfo.GetParameters(), methodInfo.GetCustomAttributes(true)))){
                Actions.Add(controllerAction);
            }
        }

        public string Name { get; set; }
        public List<ControllerAction> Actions { get; set; }
    }
}