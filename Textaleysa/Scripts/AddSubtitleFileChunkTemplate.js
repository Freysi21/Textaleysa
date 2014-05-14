$(document).ready(function () {
    $(".edit-chunk").click(function () {
        $(".form-for-chunk").loadTemplate($("#template-for-chunk"), { "append": true });
    });
});