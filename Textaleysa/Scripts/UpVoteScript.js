$(document).ready(function () {
    jQuery(".vote-request").click(function () {
        var new_vote = { requestID: this.id }
        jQuery.post("/Request/postVotes", new_vote, function (data) {
            getVote(data);
        });
    });
});
function getVote(new_vote) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf­8",
        url: "/Request/getVotes/",
        data: new_vote,
        dataType: "json",
        success: function (votes) {

            $('#vote-' + votes[0].requestID).html(votes.length);
        },
        error: function (xhr, err) {
        }
    });
}
