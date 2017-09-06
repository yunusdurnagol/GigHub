var FollowingController = function () {
    var init = function () {
        
    };


    var toggleFollowing = function (e) {
        button = $(e.target);
        var artistId = button.attr("data-artist-id");
        if (button.hasClass("btn-default"))
            attendanceService.createFollowing(artistId, done, fail);
        else
            attendanceService.deleteFollowing(artistId, done, fail);
    };





    var done = function () {
            button.removeClass("btn-default")
            .addClass("btn-info")
            .text("Following");
    };

    var fail = function () {
        alert("Something failed");
    };
    return {
        init: init
    }
}();

<script>
    $(document).ready(function () {



        $(".js-toggle-follow").click(function (e) {
            var button = $(e.target);
            $.post("/api/following", { followeeId: button.attr("data-artist-id") })
                .done(function () {
                    
                })
                .fail(function () {
                    alert("Something Failed");
                });
        });
    //button click
    return {
        init:init;
    }
    });
</script>