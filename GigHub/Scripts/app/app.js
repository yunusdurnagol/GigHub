var AttendanceService = function () {
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);

    };

    var deleteAttendance = function (gigId,done,fail) {

        $.ajax({
            url: "/api/attendances/" + gigId ,
            method: "DELETE",
            contentType: "application/json"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();


var GigsController = function (attendanceService) {
    var init = function(container) {
        //$(".js-toggle-attendance").click(toggleAttendance);
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);

    };


    var button;

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done,fail);
    };

    //var createAttendance = function () {
    //    $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
    //        .done(done)
    //        .fail(fail);

    //};

    //var deleteAttendance = function () {

    //    $.ajax({
    //        url: "/api/attendances/" + button.attr("data-gig-id") ,
    //        method: "DELETE",
    //        contentType: "application/json"
    //    })
    //        .done(done)
    //        .fail(fail);
    //};


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
}(AttendanceService);

 