$(document).ready(function () {
    $("img.phone-image").each(function () {
        $(this).on("load", updateImageSize);
        if (this.complete)
            $(this).trigger("load");
    });

    function updateImageSize() {
        if ($(this).width() != $(this).height()) {
            $(this).addClass("resized");
            if ($(this).width() < $(this).height()) {
                var width = $(this).width();
                $(this).css({ "height": width + "px", "width": "auto" }).attr("data-height", width).addClass("height-resized");
            } else {
                var height = $(this).height();
                $(this).css({ "width": height + "px", "height": "auto" }).attr("data-width", height).addClass("width-resized");
            }
        }
    }
});