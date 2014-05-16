/// <reference path="CommentScript.js" />

$(document).ready(function () { // function loads then the document is loaded
    var fileIDElem = document.getElementsByClassName("submit-grade");
    var fileID = $(fileIDElem[0].getAttribute('id'));
    var new_comment = { fileID: fileID.selector, content: $("#comment").val() }; // get text from input box and make a json object
    getAllComments(new_comment);
    jQuery("#postbutton").click(function () { // function runs when the post button is clicked
        var new_comment = { fileID: fileID.selector, content: $("#comment").val() };
        $(".comment-item").remove(); // first we remove all the comments 
        if (new_comment.content != null && new_comment.content.trim() != "") { // if the input field is nonempty 
            jQuery.post("/CommentAndLike/PostComment/", new_comment, function (data) { // post the comment 
                getAllComments(data);
            });
            $("#comment").val(""); // resets the input field
            $("#comment").attr("placeholder", "Enter a comment."); // resets the placeholder
        }
        else {
            getAllComments();
            // comment field was empty and we display another placeholder
            $("#comment").attr("placeholder", "Empty comments are not allowed.");
        }
    });

});

function getAllComments(new_comment) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/CommentAndLike/GetComments/",
        data: new_comment,
        dataType: "json",
        success: function (comments) {
            // go through the comments
            for (var i = 0; i < comments.length; i++) {
                // loads the comments list 
                $("#comment-list").loadTemplate($("#template"), comments[i], { "append": true });
                $("li.comment-item").each(function (i) { // sets id to each comment 
                    $(this).attr("id", "comment" + i)
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