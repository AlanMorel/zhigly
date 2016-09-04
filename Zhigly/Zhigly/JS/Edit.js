$(function () {

    $(".button.delete").click(function () {
        $(".prompt-div").show();
        $(".button.delete").hide();
    });

    $(".prompt-button.no").click(function () {
        $(".prompt-div").hide();
        $(".button.delete").show();
    });

    $(".button.add").click(function () {
        var images = $(".image-section");
        var length = images.length;
        for (i = 0; i < length; i++) {
            var image = $(images[i]);
            if (image.css("display") === "none") {
                image.show();
                return;
            }
        }
        $(".error-label").html("Zhigly supports up to " + length + " images per listing.");
        $(".error-label").show();
        $("html, body").animate({ scrollTop: 0 }, "medium");
    });

    $("#TitleTextBox").focusout(function () {
        var results = validateTitle($(this).val());
        $("#ErrorLabel").html(results);
    });

    $("#DescriptionTextBox").focusout(function () {
        var results = validateDescription($(this).val());
        $("#ErrorLabel").html(results);
    });
});