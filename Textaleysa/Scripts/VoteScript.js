jQuery(document).ready(function () {

    getRequests();

    jQuery("#postbutton").click(function () {

        jQuery(".request-item").remove();

        var new_request = { "mediaTitle": jQuery("#mediaTitle").val()};

        if (new_request.mediaTitle != null && new_request.mediaTitle.trim() != "") {

            jQuery.post("/Request/CreateRequest/", new_request, function (requests) {

                getRequests();

            });
            jQuery("#mediaTitle").val("");
            jQuery("#mediaTitle").attr("placeholder", "Enter title");
        }
        else {
            getRequests();
            jQuery("#mediaTitle").attr("placeholder", "Title needed");
        }
        alert("Hallo!");
    });
    //jQuery(".list-group-item").on("click", "#vote-request", function () {
    jQuery("#vote-request").click(function () {
        alert("Hallo!");
        var new_vote = { requestID: jQuery(this).closest("tr.request-item").attr("id") }
        jQuery.post("/Request/postVotes", new_vote, function (data) {
            getVote();
        });
    });
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
function getRequests() {
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
}

function getVote(requests) {
    jQuery.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/Request/getVotes/",
        data: "{}",
        dataType: "json",
        success: function (votes, requests) {
            var requestID = 1;
            for (var i = 0; i < requests.length; i++) {
                var counter = 0;
                for (var j = 0; j < votes.length; j++) {
                    if (votes[j].requestID == requestID) {
                        counter++;
                    }
                    //document.getElementById(ID_Dealer).innerHTML = counter;
                    //var Vote = '#Vote' + i
                    //document.getElementById(Vote) = counter.toString();
                }
                $('#Vote' + i).html(counter);
                requestID++;
            }
        },
        error: function (xhr, err) {
            // Note: just for debugging purposes!
            alert("readyState: " + xhr.readyState +
            "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
}



