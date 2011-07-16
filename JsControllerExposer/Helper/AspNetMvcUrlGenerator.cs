﻿/*
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

            return FullApplicationPath(httpContextWrapper.Request) + urlHelper.Action(action, controller, new RouteValueDictionary(values));
        }

        private static string FullApplicationPath(HttpRequestBase request) {
            var url = request.Url.AbsoluteUri.Replace(request.Url.AbsolutePath, string.Empty) + request.ApplicationPath;
            return url.EndsWith("/") ? url.Substring(0, url.Length - 1) : url;
        }
    }
}