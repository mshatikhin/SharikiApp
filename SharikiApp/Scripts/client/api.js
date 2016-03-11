var Shariki;
(function (Shariki) {
    var ResultVoid = (function () {
        function ResultVoid() {
        }
        return ResultVoid;
    })();
    Shariki.ResultVoid = ResultVoid;
    var Result = (function () {
        function Result() {
        }
        return Result;
    })();
    Shariki.Result = Result;
    var DataLoader = (function () {
        function DataLoader(url, httpMethod) {
            this.load = function (data, success) {
                var jsonData = data == null ? null : ko.mapping.toJSON(data);
                var settings = {
                    url: url,
                    dataType: "json",
                    type: httpMethod,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    error: function () { alert("При запросе данных произошла ошибка"); },
                    beforeSend: function () { },
                    success: function (resultData) {
                        success(resultData);
                    },
                    complete: function () {
                        $("#loader").hide();
                    }
                };
                jQuery.ajax(settings);
            };
        }
        return DataLoader;
    })();
    Shariki.DataLoader = DataLoader;
    var AsyncClient = (function () {
        function AsyncClient() {
        }
        AsyncClient.current = new AsyncClient();
        return AsyncClient;
    })();
    Shariki.AsyncClient = AsyncClient;
})(Shariki || (Shariki = {}));
//# sourceMappingURL=api.js.map