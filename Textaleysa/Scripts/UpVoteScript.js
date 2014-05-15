$(document).ready(function () {
    alert("hallo");
    //jQuery(".list-group-item").on("click", "#vote-request", function () {
    $(".vote-request").click(function () {
        alert("Your IN");
        var new_vote = { requestID: this.id }
        $.post("/Request/postVotes", new_vote, function (request) {
            getVote(request);
        });
    });
    function getVote(request) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf­8",
            url: "/Request/getVotes/",
            data: "{}",
            dataType: "json",
            success: function (votes) {
                var counter = $("#" + votes[0].requestID + ".vote-request").closest("#hiddenVotes").val() + 1;
                    $('#vote' + votes[0].requestID).html(counter);
                },
            error: function (xhr, err) {
                // Note: just for debugging purposes!
                alert("readyState: " + xhr.readyState +
                "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            }
        });
    }
});
//var new_vote = { requestID: jQuery(this).closes("li").attr("id") }

//jQuery.post("/Home/postVotes", new_vote, function (data) {

//  if (data.userName != "") {
//    jQuery(".vote-item").remove();
//  getVote();
//         }
//       else {
//         console.log("User already voted this request");
//       jQuery(".vote-item").remove();
//     getVote();
//            }
//        });
//    });
//});
/*function getRequests() {
    jQuery.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/Request/getRequests/",
        data: "{}",
        dataType: "json",
        success: function (requests) {
            for(var i = 0; i < requests.length; i++){
                jQuery("#request-list").loadTemplate(jQuery("#template"), requests[i], { "append": true });

                jQuery("tr.request-item").each(function (i) {
                    jQuery(this).attr("id", i)
                });

                jQuery("span.qty").each(function (i) {
                    jQuery(this).attr("id", "Vote" + i)
                });

            }
            getVote();
        },
        error: function (xhr, err) {
            // Note: just for debugging purposes!
            alert("readyState: " + xhr.readyState +
            "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
}*/




