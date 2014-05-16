$(document).ready(function () {
    jQuery(".submit-grade").click(function () {
        var give_grade = $("#input-" + this.id).val();
        if (give_grade > 10 || give_grade < 0) {
<<<<<<< HEAD
            console.log("Einkunn verður að vera á bilinu 0-10");
            $("#error-message").html("Einkunn verður að vera á bilinu 0-10")
            //alert("Einkunn verður að vera á bilinu 0-10");
=======
            $("#error-message").html("Einkunn verður að vera á bilinu 0-10")
>>>>>>> f036f0441b56c7602e9c5ef51a6280f875424a0d
        }
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
        
