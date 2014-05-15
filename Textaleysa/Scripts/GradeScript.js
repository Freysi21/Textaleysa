$(document).ready(function () {
    jQuery(".submit-grade").click(function () {
        var give_grade = $("input#" + this.id).val();
        if (give_grade > 10 || give_grade < 0) {
            //alert("Einkunn verður að vera á bilinu 0-10");
        }
        var new_grade = { fileID: this.id, mediaGrade: give_grade  }
        jQuery.post("/SubtitleFile/postGrade", new_grade, function (data) {
            if (data == "") {
                //alert("BÍDDU HALLÓ, ÆTLAR ÞÚ GEFA EINKUNNIR EINS OG KENNARI Í HR Í LOK ANNAR");
            }
            else {
                getVote(data);
            }
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
