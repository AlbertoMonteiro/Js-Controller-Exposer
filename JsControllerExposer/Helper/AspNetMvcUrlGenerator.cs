/*
 * Code from https://github.com/mauricioaniche/restfulie.net/
 */
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JsControllerExposer.Helper {
    public class AspNetMvcUrlGenerator {
        public static string For(string controller, string action, IDictionary<string, object> values) {
            var httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            var urlHelper = new UrlHelper(new RequestContext(httpContextWrapper, RouteTable.Routes.GetRouteData(httpContextWrapper)));

            string v2 = urlHelper.Action(action, controller, new RouteValueDictionary(values));
            return v2.Replace("Controller","");
        }
    }
}