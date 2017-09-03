var GigsController = function() {
    var init = function() {
        $(".js-toggle-attendance").click(toggleAttendance);

    };


    var button;

    var toggleAttendance = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) {
            $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                .done(done)
                .fail(fail);
        } else {
            $.ajax({
                    url: "/api/attendances/" + button.attr("data-gig-id"),
                    method: "DELETE",
                    contentType: "application/json"
                })
                .done(done)
                .fail(fail);
        }

    };

    var fail = function () {
        alert("Something failed...");
    };

    var done = function() {
        var text = (button.text() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    //Where we make it public
    return{
        init: init
    }
}();

 