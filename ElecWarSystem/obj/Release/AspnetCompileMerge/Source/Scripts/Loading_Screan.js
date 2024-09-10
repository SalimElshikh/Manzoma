$(document).ready(function () {
    // Show loading spinner before page content loads
    $("#loading").show();

    // Hide the loading spinner once content is loaded
    $(window).on("load", function () {
        $("#loading").hide();
    });

    // Show loading spinner during AJAX requests
    $(document).ajaxStart(function () {
        $("#loading").show();
    }).ajaxStop(function () {
        $("#loading").hide();
    });
});
