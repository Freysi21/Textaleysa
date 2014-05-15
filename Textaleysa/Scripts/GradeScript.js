$(document).ready(function () {
<<<<<<< HEAD
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
=======
    jQuery(".vote-request").click(function () {
        var new_vote = { requestID: this.id }
        jQuery.post("/Request/postVotes", new_vote, function (data) {
            getVote(data);
>>>>>>> 30dcd4a252c8c39737735768fc4e5f8a207fdd79
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
