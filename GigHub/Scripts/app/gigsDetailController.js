
var FollowingService = function () {
    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/following", { followeeId: followeeId })
            .done(done)
            .fail(fail);
    };

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/following/" + followeeId,
            method: "DELETE",
            contentType: "application/json"
        })
            .done(done)
            .fail(fail);
    };
    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }

}();

var gigsDetailController = function (followingService) {
    var followButton;


    var init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };

    var toggleFollowing = function (e) {
        followButton = $(e.target);
        var followeeId = followButton.attr("data-artist-id");

        if (followButton.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);
            //createFollowing();
        else
             followingService.deleteFollowing(followeeId, done, fail);
           // deleteFollowing();
    };
    //var createFollowing = function () {
    //    $.post("/api/following", { followeeId: followButton.attr("data-artist-id") })
    //        .done(done)
    //        .fail(fail);
    //};

    //var deleteFollowing = function () {
    //    $.ajax({
    //        url: "/api/following/" + followButton.attr("data-artist-id"),
    //        method: "DELETE",
    //        contentType: "application/json"
    //    })
    //        .done(done)
    //        .fail(fail);
    //};

    var done = function () {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something is wrong");
    };

    return {
        init: init
    }
}(FollowingService);

