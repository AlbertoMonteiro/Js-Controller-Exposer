using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using JsControllerExposer.Helper;

namespace JsControllerExposer.Models {
    public class ControllerAction {
        
        public string Name { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public List<Parameter> Parameters { get; set; }

        public ControllerAction(string controller, string name, IEnumerable<ParameterInfo> getParameters, IEnumerable<object> getCustomAttributes) {
            Name = name;

            Route = AspNetMvcUrlGenerator.For(controller, name, new Dictionary<string, object>());

            Parameters = new List<Parameter>();
            var attributes = getCustomAttributes.Where(attr => attr is ActionMethodSelectorAttribute);
            if (attributes.Any()) {
                var attribute = attributes.ElementAt(0);
                Method = HttpVerb.GetVerb(attribute.GetType().Name);
            } else
                Method = "Get";
            foreach (var parameter in getParameters.Select(param => new Parameter(param.Name, param.ParameterType.Name))) {
                Parameters.Add(parameter);
            }
        }
    }
}