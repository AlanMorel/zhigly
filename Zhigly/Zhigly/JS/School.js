$(function () {

    $(".camera").hover(function () {
        $(this).find(".tooltip").show();
    });

    $(".camera").mouseleave(function () {
        $(this).find(".tooltip").hide();
    });
});