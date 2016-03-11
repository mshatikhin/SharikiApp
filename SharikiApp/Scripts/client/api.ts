module Shariki
{
    export interface IResultVoid
    {
        IsSuccess: boolean;
        Errors: string[];
    }
    export class ResultVoid implements IResultVoid
    {
        IsSuccess: boolean;
        Errors: string[];
        Warnings: string[];
    }
    export class Result<T> {
        Data: T;
        IsSuccess: boolean;
        Errors: string[];
        Warnings: string[];
    }

    export class DataLoader
    {
        load: (data: any, success: (result: any) => void) => void;

        constructor(url: string, httpMethod: string)
        {
            this.load = (data, success) =>
            {
                var jsonData = data == null ? null : ko.mapping.toJSON(data);
                var settings: JQueryAjaxSettings = {
                    url: url,
                    dataType: "json",
                    type: httpMethod,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    error() { alert("При запросе данных произошла ошибка"); },
                    beforeSend() { },
                    success(resultData) {
                        success(resultData);
                    },
                    complete() {
                        $("#loader").hide();
                    }
                };
                jQuery.ajax(settings);
            };
        }
    }


    export class AsyncClient
    {
        static current = new AsyncClient();
    }
} 