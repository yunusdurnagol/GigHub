﻿var GigsController = function() {
    var init = function() {
        $(".js-toggle-attendance").click(function (e) {
            var button = $(e.target);
            if (button.hasClass("btn-default")) {
                $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                    .done(function () {
                        button.removeClass("btn-default")
                            .addClass("btn-info")
                            .text("Going");
                    })
                    .fail(function () {
                        alert("Something Failed");
                    });
            } else {
                $.ajax({
                        url: "/api/attendances/" + button.attr("data-gig-id"),
                        method: "DELETE",
                        contentType: "application/json"
                    })
                    .done(function () {
                        button.removeClass("btn-info")
                            .addClass("btn-default")
                            .text("Going?");

                    })
                    .fail(function () {
                        alert("Something failed??");
                    });
            }

        });
    };
    //Where we make it public
    return{
        init: init
    }
}();

 