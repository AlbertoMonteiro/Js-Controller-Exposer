namespace JsControllerExposer.Helper {
    public class HttpVerb {

        public static string GetVerb(string verb) {
            return verb.Replace("Http", "").Replace("Attribute", "");
        }

    }
}
