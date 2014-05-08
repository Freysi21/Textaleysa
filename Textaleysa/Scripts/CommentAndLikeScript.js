/// <reference path="CommentScript.js" />

$(document).ready(function () { // function loads then the document is loaded

    getAllComments();

    $("#postbutton").click(function () { // function runs when the post button is clicked

        $(".comment-item").remove(); // first we remove all the comments 

        var new_comment = { "CommentText": $("#CommentText").val() }; // get text from input box and make a json object

        if (new_comment.CommentText != null && new_comment.CommentText.trim() != "") { // if the input field is nonempty 

            $.post("/Home/Create/", new_comment, function (comments) { // post the comment 

                getAllComments();
            });
            $("#CommentText").val(""); // resets the input field
            $("#CommentText").attr("placeholder", "Enter a comment."); // resets the placeholder
        }
        else {
            getAllComments();
            // comment field was empty and we display another placeholder
            $("#CommentText").attr("placeholder", "Empty comments are not allowed.");
        }
    });

    $(".list-group").on("click", ".like-comment", function () { // function runs when a comment is liked 

        var new_like = { CommentID: $(this).closest("li").attr("id") }  // get the id from the element

        $.post("/Home/postLikes", new_like, function (data) { // post the like

            if (data.Username != "") {
                $(".like-item").remove();
                getLike();
            }
            else {
                console.log("User already liked this comment."); // logs to the console window.
                $(".like-item").remove();
                getLike();
            }
        });
    });
});

function getAllComments() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/Home/getComments/",
        data: "{}",
        dataType: "json",
        success: function (comments) {
            // go through the comments
            for (var i = 0; i < comments.length; i++) {
                // loads the comments list 
                $("#comment-list").loadTemplate($("#template"), comments[i], { "append": true });

                $("li.comment-item").each(function (i) { // sets id to each comment 
                    $(this).attr("id", i)
                });

                $("ul.like-list").each(function (i) { // sets id to each like list
                    $(this).attr("id", "like" + i)
                });
            }
            getLike();
        },
        error: function (xhr, err) {
            // Note: just for debugging purposes!
            alert("readyState: " + xhr.readyState +
            "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
}

function getLike(comments) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/Home/getLikes/",
        data: "{}",
        dataType: "json",
        success: function (likes, comments) {
            var commentID = 1;
            // go through all the comments and the likes 
            for (var i = 0; i < comments.length; i++) {
                for (var j = 0; j < likes.length; j++) {
                    // we display each like from the user
                    if (likes[j].CommentID == commentID) {
                        var item = $("<li/>").text(likes[j].Username + " liked this at " + likes[j].LikeDate).addClass("like-item");
                        var ID_Dealer = "#like" + i;
                        $(ID_Dealer).append(item);
                    }
                }
                commentID++;
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