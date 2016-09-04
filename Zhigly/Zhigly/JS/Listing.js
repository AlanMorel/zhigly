$(function () {

    $(".report-label").on("click", function (e) {
        $(".report-container").css("display", "block");
        $(".report-label").css("display", "none");
    });

    $("#ReportButton").on("click", function (e) {
        e.preventDefault();

        var listing = $("#ListingId").val();
        var reason = $("#Reason").val();

        $.ajax({
            type: "POST",
            url: "/WebService.asmx/report?listing=" + listing + "&reason=" + reason,
            success: onSuccess,
            error: onErrorCall
        });

        function onSuccess(response) {
            $(".report-container").addClass("success-result");
            displayResultMessage("Your report has been received successfully!");
        }

        function onErrorCall(response) {
            $(".report-container").addClass("error-result");
            displayResultMessage("An error has occurred. Please try again. :(");
        }

        function displayResultMessage(result) {
            $("#ReportContainer").css("display", "none");
            $(".report-result").css("display", "block");
            $(".report-result").html(result);
            setTimeout(
                 function () {
                     $(".report-container").css("display", "none");
                 }, 5000);
        }
    });
});