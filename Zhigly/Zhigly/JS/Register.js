$(function () {

    $("[type='radio']").click(function () {
        var index = $("[type='radio']").index(this);
        if (index === 3) {
            $("#PrimaryName").html("Organization:");
            $("#SecondaryName").html("<span class='optional'>(opt)</span>Position:");
        } else {
            $("#PrimaryName").html("First Name:");
            $("#SecondaryName").html("Last Name:");
        }
    });

    $("#EmailTextbox").focusout(function () {
        var results = validateEmail($(this).val());
        $("#ErrorLabel").html(results);
    });

    $("#PrimaryNameTextbox").focusout(function () {
        var organization = $("#TypeRadioButtonList").find(":checked").val() === 3;
        var results = validatePrimaryName($(this).val(), organization);
        $("#ErrorLabel").html(results);
    });

    $("#SecondaryNameTextbox").focusout(function () {
        var organization = $("#TypeRadioButtonList").find(":checked").val() === 3;
        var results = validateSecondaryName($(this).val(), organization);
        $("#ErrorLabel").html(results);
    });

    $("#PasswordTextbox").focusout(function () {
        var results = validatePassword($(this).val());
        $("#ErrorLabel").html(results);
    });

    $("#ConfirmPasswordTextbox").focusout(function () {
        var results = validateConfirm($("#PasswordTextbox").val(), $(this).val());
        $("#ErrorLabel").html(results);
    });
});