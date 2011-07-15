namespace JavascriptControllersExposer.Models {
    public class Parametro {
        public Parametro(string name, string typeName) {
            Name = name;
            TypeName = typeName;
        }

        public string Name { get; set; }
        public string TypeName { get; set; }
    }
}