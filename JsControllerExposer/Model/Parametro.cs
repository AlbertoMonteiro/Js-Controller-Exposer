namespace JsControllerExposer.Models {
    public class Parameter {

        public string Name { get; set; }
        public string TypeName { get; set; }
        
        public Parameter(string name, string typeName) {
            Name = name;
            TypeName = typeName;
        }
    }
}