$(function () {

    $(".button.add").click(function () {
        var uploads = $("[type='file']");
        var length = uploads.length;
        for (i = 0; i < length; i++) {
            var upload = $(uploads[i]);
            if (upload.css("display") === "none") {
                upload.show();
                return;
            }
        }
        $(".error-label").html("Zhigly supports up to " + length + " images per listing.");
        $(".error-label").show();
        $("html, body").animate({ scrollTop: 0 }, "medium");
    });

    $("#DurationDropDownList").change(function () {
        calculateTotal();
    });

    $(".visibility").click(function () {

        var checkbox = $("[type='checkbox']");
        checkbox.prop("checked", !checkbox.prop("checked"));

        alert(checkbox.prop("checked"));

        var div = $(".visibility");
        div.toggleClass("visible");
        div.toggleClass("invisible");

        var spy = $(".spy");
        spy.toggleClass("black");
        spy.toggleClass("white");

        var text = $(".visibility-text");

        if (spy.hasClass("black")) {
            text.html("This listing is not being posted anonymously.");
        } else {
            text.html("This listing is being posted anonymously.");
        }
    });

    $("#anonymity").click(function () {

        var checkbox = $("#AnonymityCheckbox");
        checkbox.prop("checked", !checkbox.prop("checked"));

        var div = $("#anonymity > .option-icon");
        div.toggleClass("enabled-anonymity");
        div.toggleClass("disabled-anonymity");

        var text = $("#anonymity .option-title");

        if (div.hasClass("disabled-anonymity")) {
            text.html("This listing is not being posted anonymously.");
        } else {
            text.html("This listing is being posted anonymously.");
        }

        calculateTotal();
    });

    $("#boost").click(function () {

        var checkbox = $("#BoostCheckbox");
        checkbox.prop("checked", !checkbox.prop("checked"));

        var div = $("#boost > .option-icon");
        div.toggleClass("enabled-boost");
        div.toggleClass("disabled-boost");

        var text = $("#boost .option-title");

        if (div.hasClass("disabled-boost")) {
            text.html("This listing is not being boosted.");
        } else {
            text.html("This listing is being boosted.");
        }

        calculateTotal();
    });

    $("#TitleTextBox").focusout(function () {
        var results = validateTitle($(this).val());
        $("#ErrorLabel").html(results);
    });

    $("#DescriptionTextBox").focusout(function () {
        var results = validateDescription($(this).val());
        $("#ErrorLabel").html(results);
    });

    function calculateTotal() {
        var anonymity = $("#AnonymityCheckbox").prop("checked");
        var boost = $("#BoostCheckbox").prop("checked");
        var duration = $("#DurationDropDownList").val();

        var anonymityCost = anonymity ? duration * 5 : 0;
        var boostCost = boost ? duration * 10 : 0;

        var total = anonymityCost + boostCost;

        $("#AnonymityCost").html(anonymityCost + " Credits");
        $("#BoostCost").html(boostCost + " Credits");

        if (total === 0) {
            $("#TotalCost").html("Free");
        } else {
            $("#TotalCost").html(total + " Credits");
        }
    }

    function setStates() {

        if ($("#AnonymityCheckbox").prop("checked")) {
            $("#anonymity > .option-icon").attr("class", "option-icon enabled-anonymity");
            $("#anonymity .option-title").html("This listing is being posted anonymously.");
        } else {
            $("#anonymity > .option-icon").attr("class", "option-icon disabled-anonymity");
            $("#anonymity .option-title").html("This listing is not being posted anonymously.");
        }

        if ($("#BoostCheckbox").prop("checked")) {
            $("#boost > .option-icon").attr("class", "option-icon enabled-boost");
            $("#boost .option-title").html("This listing is being boosted.");
        } else {
            $("#boost > .option-icon").attr("class", "option-icon disabled-boost");
            $("#boost .option-title").html("This listing is not being boosted.");
        }

        calculateTotal();
    }

    setStates();
});