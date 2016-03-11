/// <reference path="../typings/spin/spin.d.ts" />
/// <reference path="../typings/moment/moment.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/bootstrap/bootstrap.d.ts" />

//<script type="text/javascript" >
//$.growl({ title: "Growl", message: "The kitten is awake!" });
//$.growl.error({ message: "The kitten is attacking!" });
//$.growl.notice({ message: "The kitten is cute!" });
//$.growl.warning({ message: "The kitten is ugly!" });
//</script>


var basketMainInfo = "#basket-main-info";

$(() => {
    var hash = location.hash;
    if (hash != null && hash !== "") {
        var link = $("a[href=\"" + hash.replace("-nav", "") + "\"]");
        link.tab("show");
    }

    $("a[data-toggle=\"tab\"]").on("shown.bs.tab", e => {
        var tabId = $(e.target).attr("href");
        location.hash = tabId + "-nav";
    });

    //$(window).on("hashchange", e => {
    //    e.preventDefault();
    //    return false;

    //});
});

$(document).ready(() => {

    (function refreshBasketInfo() {
        $.get("/basket/GetBasketInfo", data => {            
            $(basketMainInfo).text(data);
        });
    } ());


    var allsum = $("[data-allsum='true']");
    var allitems = $("[data-item-sum='true']");

    function recalcAllSum() {
        var sum = 0;
        allitems.each((i, el) => {
            sum += parseFloat($(el).text());
        });
        var text = sum.toFixed(2).replace(".", ",");
        allsum.text(text);
    }

    $(document).on("keyup", "[data-count='true']", function () {
        var el = $(this);
        var val = el.val();
        if (isNaN(val) || !isFinite(val) || val.toString() === "") {
            el.val("");
            val = 0;
        }
        var sumField = el
            .closest("td")
            .closest("tr")
            .find("[data-item-sum='true']");

        var price = parseFloat(sumField.attr("data-price"));
        var balloonId = parseInt(sumField.attr("data-balloonid"));
        if (isNaN(price) || !isFinite(price)) {
            price = 0;
        }
        

        $.post(`/basket/ChangeCount?balloonId=${balloonId}&count=${parseInt(val)}`, basketInfo => {
            $(basketMainInfo).text(basketInfo);
            $.post(`/basket/getPriceWithDiscount?balloonId=${balloonId}&count=${parseInt(val)}`, data => {
                let priceWithDiscount = Number(data).toFixed(2).replace(".", ",");
                $(`[data-balloon-price-id='${balloonId}']`).text(priceWithDiscount);
                var sum = parseInt(val) * parseFloat(data);
                var newSum = sum.toFixed(2).replace(".", ",");
                sumField.text(newSum);
                recalcAllSum();
            });
        });
    });

    $(document).on("click", "[data-addtobasket='true']", function () {
        var button = $(this);
        var messageBlock = button.find(".message-add-to-basket");
        var id = button.attr("data-balloonid");
        var input = button.closest("div").find("input");
        var count = input.val();
        var numberCount = Number(count);
        if (isNaN(numberCount) || !isFinite(numberCount) || numberCount === null || numberCount === undefined || numberCount.toString() === "") {
            input.val("");
        } else {
            if (id != null) {
                var url = "/basket/AddToBasket?balloonId=" + id + "&count=" + numberCount;
                $.post(url, data => {
                    $(basketMainInfo).text(data);
                });
            }
            messageBlock.show();
            input.val("");
            $["growl"].notice({ size: "large", location: "br", title: "", message: "Товар успешно добавлен в корзину." });
            setTimeout(() => { messageBlock.fadeOut() }, 500);
        }
    });

    $(".nav-menu_js > li > div a").each(function () {
        var href = $(location)
            .attr("href")
            .replace(location.search, "")
            .replace(location.hash, "");
        var active = href.indexOf(this.href) !== -1;
        if (active) {
            $(this).parent().addClass("active");
        }
    });

    $("#orderModal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var order = button.data("whatever"); // Extract info from data-* attributes
        var modal = $(this);
        modal.find(".modal-body #order-input").val(order.toString());
    });

    moment.locale("ru");
    //$.fn.datepicker.defaults.format = "dd.mm.yyyy";
    //$.fn.datepicker.defaults.language = "ru";
    //$.fn.datepicker.defaults.autoclose = true;
    //$.fn.datepicker.defaults.keyboardNavigation = false;

    var opts = {
        lines: 13, // The number of lines to draw
        length: 20, // The length of each line
        width: 10, // The line thickness
        radius: 30, // The radius of the inner circle
        corners: 1, // Corner roundness (0..1)
        rotate: 0, // The rotation offset
        direction: 1, // 1: clockwise, -1: counterclockwise
        color: "#000", // #rgb or #rrggbb or array of colors
        speed: 1.4, // Rounds per second
        trail: 60, // Afterglow percentage
        shadow: false, // Whether to render a shadow
        hwaccel: false, // Whether to use hardware acceleration
        className: "spinner", // The CSS class to assign to the spinner
        zIndex: 2e9, // The z-index (defaults to 2000000000)
        top: "auto", // Top position relative to parent in px
        left: "auto" // Left position relative to parent in px
    };

    var spinner = new Spinner(opts);
    //spinner.spin(document.getElementById("loaderSpin"));

    var mainElem = $("[data-main='true']");
    if (mainElem.length > 0) {
        //Shariki.AsyncClient.current.getAnalyticsLists((result) =>
        //{
        //    if (result.IsSuccess) {
        //        var mainVm = new Shariki.MainVm(mainElem[0], result.Data);               
        //        mainVm.loadRules(ruleTypeM, () => { spinner.stop(); });
        //    }
        //});
    }
});