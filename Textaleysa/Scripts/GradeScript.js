$(document).ready(function () {
    jQuery(".submit-grade").click(function () {
        var give_grade = $("#input-" + this.id).val();
        if (give_grade > 10 || give_grade < 0) {
            $("#error-message").html("Einkunn verður að vera á bilinu 0-10")
            //alert("Einkunn verður að vera á bilinu 0-10");
        }
<<<<<<< HEAD
        else{
            var new_grade = { fileID: this.id, mediaGrade: give_grade }

            jQuery.post("/SubtitleFile/postGrade", new_grade, function (data) {
                if (data == "") {
                    $("#error-message").html("Ekki er hægt að gefa sömu skrá einkunn tvisvar.")
                }
                else {
                    getVote(data);
                }
            });
        }
=======
        var new_grade = { fileID: this.id, mediaGrade: give_grade  }
        jQuery.post("/SubtitleFile/postGrade", new_grade, function (data) {
            if (data == "") {
                //alert("BÍDDU HALLÓ, ÆTLAR ÞÚ GEFA EINKUNNIR EINS OG KENNARI Í HR Í LOK ANNAR");
            }
            else {
                getVote(data);
            }
        });
>>>>>>> 6e59bdd26cb6c4aad4b01ec24abf7bd7d431992b
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
        
