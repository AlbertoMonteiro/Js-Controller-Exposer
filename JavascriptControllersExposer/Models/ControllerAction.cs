using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace JavascriptControllersExposer.Models {
    public class ControllerAction {
        public ControllerAction(string name, IEnumerable<ParameterInfo> getParameters, IEnumerable<object> getCustomAttributes) {
            Name = name;
            Parametros = new List<Parametro>();
            var attributes = getCustomAttributes.Where(x => x is ActionMethodSelectorAttribute);
            if (attributes.Any()) {
                var attr = attributes.ElementAt(0);
                Method = attr.GetType().Name.Replace("Http", "").Replace("Attribute", "");
            }
            foreach (var p in getParameters.Select(parameterInfo => new Parametro(parameterInfo.Name, parameterInfo.ParameterType.Name))){
                Parametros.Add(p);
            }
        }


        public string Name { get; set; }
        public string Method { get; set; }
        public List<Parametro> Parametros { get; set; }

    }
}