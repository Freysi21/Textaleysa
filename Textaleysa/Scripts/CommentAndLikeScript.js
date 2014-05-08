/// <reference path="CommentScript.js" />

$(document).ready(function () { // function loads then the document is loaded

    getAllComments();
    /*
    $("#postbutton").click(function () { // function runs when the post button is clicked

        $(".comment-item").remove(); // first we remove all the comments 

        var new_comment = { "CommentText": $("#CommentText").val() }; // get text from input box and make a json object

        if (new_comment.CommentText != null && new_comment.CommentText.trim() != "") { // if the input field is nonempty 
            $.post("/CommentAndLike/PostComment/", new_comment, function (comments) { // post the comment 
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
    }); */
});

function getAllComments() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/CommentAndLike/GetComments/",
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