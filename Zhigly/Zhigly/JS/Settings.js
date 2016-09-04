$(function () {

    $("#PrimaryNameTextbox").focusout(function () {
        var organization = $("#PrimaryNameLabel").html().indexOf("Organization") >= 0;
        var results = validatePrimaryName($(this).val(), organization);
        $("#InfoError").html(results);
    });

    $("#SecondaryNameTextbox").focusout(function () {
        var organization = $("#PrimaryNameLabel").html().indexOf("Organization") >= 0;
        var results = validateSecondaryName($(this).val(), organization);
        $("#InfoError").html(results);
    });
});