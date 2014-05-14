$(document).ready(function () {
    $("#save-chunk").click(function () {
        var newChunk = {
            "ID" : $("#item_ID").val(),
            "lineID" : $("#item_lineID").val(),
            "startTime": $("#item_startTime").val(),
            "stopTime" : $("#item_stopTime").val(),
            "line1": $("#item_line1").val(),
            "line2": $("#item_line2").val(),
            "line3": $("#item_line3").val()
        };
        $.post("/SubtitleFile/EditChunk/", newChunk, function () {
            $(".form-for-chunk").empty();
        })
    });
});