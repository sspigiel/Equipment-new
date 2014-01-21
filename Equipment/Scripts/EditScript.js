$(document).ready(function () {
    $(function () {
        $("#Start").datepicker();
    });
    $(function () {
        $("#End").datepicker();
    });
    $(function () {
        $("#UserClick").on("click", function () {
            $("#DeviceUser").val($("#UserName").val());
        })
    });
});