;(function (options) {
    var options = options || {};
    var defaults = {
        id: options.id || "js_zoommer",
        dataSelector: options.dataSelector || "data-zoommer",
        width: options.width || 500,
        height: options.height || 500,
    };

    function ready(fn) {
        if (document.readyState != 'loading') {
            fn();
        } else if (document.addEventListener) {
            document.addEventListener('DOMContentLoaded', fn);
        } else {
            document.attachEvent('onreadystatechange', function () {
                if (document.readyState != 'loading')
                    fn();
            });
        }
    }

    function addEventListener(el, eventName, handler) {
        if (el.addEventListener) {
            el.addEventListener(eventName, handler);
        } else {
            el.attachEvent('on' + eventName, function () {
                handler.call(el);
            });
        }
    }

    var timer = null;

    function zoom() {
        clearTimeout(timer);
        timer = setTimeout(function () {
            var style = this.parentNode.style;
            style.position = "relative";
            style.display = "block";
            style.cursor = "crosshair"
            var zoomContainer = document.createElement("div");
            zoomContainer.id = defaults.id;
            var width = Number(this.offsetWidth) + Number(50);
            zoomContainer.style.position = "absolute";
            zoomContainer.style.display = "block";
            zoomContainer.style.top = 0;
            zoomContainer.style.left = width + "px";
            zoomContainer.style.zIndex = 100000;
            zoomContainer.style.width = defaults.width + "px";
            zoomContainer.style.height = defaults.height + "px";
            zoomContainer.style.background = "transparent";
            var zoomImage = document.createElement("img");
            zoomImage.src = this.src;
            zoomImage.style.display = "block";
            zoomImage.style.maxWidth = "100%";
            zoomImage.style.maxHeight = defaults.height + "px";
            zoomContainer.appendChild(zoomImage);
            this.parentNode.appendChild(zoomContainer);
        }.bind(this), 300);
    }

    function clearZoom() {
        clearTimeout(timer);
        var zoommer = document.getElementById(defaults.id);
        if (zoommer) zoommer.parentNode.removeChild(zoommer);
    }

    function forEach(array, fn) {
        for (var i = 0; i < array.length; i++)
            fn(array[i], i);
    }

    function onReady() {
        var selector = '[' + defaults.dataSelector + '="true"]';
        var elements = document.querySelectorAll(selector);
        forEach(elements, function (item) {
            addEventListener(item, "mouseenter", zoom);
            addEventListener(item, "mouseleave", clearZoom);
        });
    }

    ready(onReady);

})();


