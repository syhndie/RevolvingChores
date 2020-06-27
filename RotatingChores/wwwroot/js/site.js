//changes star icon on high-priority checkbox from outline to solid and back again
function changestarcheckbox() {
    $("#star-checkbox").change(function () {
        var $ec = $("#star-checkbox");
        var $icon = $("#priority-icon");

        $icon.removeClass("far fas");
      
        if ($ec.is(":checked")) {
            $icon.addClass("fas");
        } else {
            $icon.addClass("far");
        }
    });    
};

function changeremembercheckbox() {
    $("#remember-checkbox").change(function () {
        var $ec = $("#remember-checkbox");
        var $icon = $("#remember-icon");

        $icon.removeClass("far fas fa-circle fa-check-circle");

        if ($ec.is(":checked")) {
            $icon.addClass("fas fa-check-circle");
        } else {
            $icon.addClass("far fa-circle");
        }
    });
};
