$(document).ready(function () {
    jQuery(".submit-grade").click(function () {
        var give_grade = $("input#" + this.id).val();
        alert(give_grade);
        var new_grade = { fileID: this.id, mediaGrade: give_grade  }
        jQuery.post("/SubtitleFile/postGrade", new_grade, function (data) {
            getVote(data);
        });
    });
});
function getVote(new_grade) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/SubtitleFile/getGrades/",
        data: new_grade,
        dataType: "json",
        success: function (avg) {

            $('#grade-' + avg.ID).html(avg.avrage);
        },
        error: function (xhr, err) {
            // Note: just for debugging purposes!
            alert("readyState: " + xhr.readyState +
            "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
}
