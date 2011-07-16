Array.prototype.add = function (el) {
    this[this.length] = el;
    return this;
};

function toMap(object) {
    var arr = [];
    for (var item in object) arr[arr.length] = { key: item, value: object[item] };
    return arr;
};

var DJR = function () {
    this.data = "";
    this.error = {};
    this.format = ".json";
    this.constructor = function () {
        return this.constructor.name;
    };

    this.formatURL = function (url, params) {
        toMap(params).forEach(function (param) {
            url = url.replace((new RegExp(param.key)).source, param.value);
        });
        return url;
    };
    this.ajax = function (object, callback, error, method, url) {
        var params = [];
        params["\(.:format\)"] = this.format;
        toMap(object).forEach(function (param) { params[":" + param.key] = param.value; });
        $.ajax({
            data: JSON.stringify(object),
            dataType: 'json',
            error: error,
            contentType: "application/json",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            success: callback,
            type: method,
            url: this.formatURL(url, params)
        });
    };
    this.callbackDefault = function (data) {
        this.data = data;
    };
    this.errorDefault = function (error) {
        this.error = error;
    };

};

var UseDJR = function () {
    this.useDJR = DJR;
    this.useDJR();
    delete this.useDJR;
};

function BalanceteController() {
    UseDJR.call(this);
};
BalanceteController.prototype.routes = {};

function ContasController() {
    UseDJR.call(this);
};
ContasController.prototype.routes = {};

function EmpresasController() {
    UseDJR.call(this);
};
EmpresasController.prototype.routes = {};

function HomeController() {
    UseDJR.call(this);
};
HomeController.prototype.routes = {};

function LancamentosController() {
    UseDJR.call(this);
};
LancamentosController.prototype.routes = {};

function RazaoController() {
    UseDJR.call(this);
};
RazaoController.prototype.routes = {};

ContasController.prototype.routes.index = { url: "/contas(.:format)", method: "GET" };
ContasController.prototype.routes.create = { url: "/contas(.:format)", method: "POST" };
ContasController.prototype.routes["new"] = { url: "/contas/new(.:format)", method: "GET" };
ContasController.prototype.routes.edit = { url: "/contas/:id/edit(.:format)", method: "GET" };
ContasController.prototype.routes.show = { url: "/contas/:id(.:format)", method: "GET" };
ContasController.prototype.routes.update = { url: "/contas/:id(.:format)", method: "PUT" };
ContasController.prototype.routes.destroy = { url: "/contas/:id(.:format)", method: "DELETE" };
ContasController.prototype.routes.tree = { url: "/tree/contas(.:format)", method: "" };

ContasController.prototype.index = function (object, config) {
    if (typeof object === "function") {
        callback = object;
        error = callback;
    }
    this.ajax(object, this.routes.index.method, this.routes.index.url, callback, error);
    return this;
};

ContasController.prototype.update = function (object, callback, error) {
    if (typeof object === "function") {
        callback = object;
        error = callback;
    }
    this.ajax(object, this.routes.update.method, this.routes.update.url, callback, error);
    return this;
};

ContasController.prototype["new"] = function (object, callback, error) {
    if (typeof object === "function") {
        callback = object;
        error = callback;
    }
    this.ajax(object, this.routes["new"].method, this.routes["new"].url, callback, error);
    return this;
};

var Contas = new ContasController();

var callback = function (contas) {
    console.log("entrou?", contas);
};

Contas.index({ callback: callback });
//Contas.update({id: 27}, callback, function() { console.log(arguments) });