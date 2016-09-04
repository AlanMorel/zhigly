$(function () {

    $("#Name").focusout(function () {
        var results = validateName($(this).val());
        $("#ErrorLabel").html(results);
    });

    $("#Email").focusout(function () {
        var results = validateEmail($(this).val());
        $("#ErrorLabel").html(results);
    });

    $("#Message").focusout(function () {
        var results = validateMessage($(this).val());
        $("#ErrorLabel").html(results);
    });
});