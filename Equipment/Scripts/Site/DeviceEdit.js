$(document).ready(function () {
    $('.datepicker').datepicker({ autoclose: true, todayHighlight: true, language: "pl" });
    $(function () {
        $("#UserClick").on("click", function () {
            $("#DeviceUser").val($("#UserName").val());
        });
    });
});