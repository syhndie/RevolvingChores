function changestarcheckbox() {
    $("#example-checkbox").change(function () {
        var $ec = $("#example-checkbox");
        var $icon = $("#priority-icon");

        $icon.removeClass("far fas");
      
        if ($ec.is(":checked")) {
            $icon.addClass("fas");
        } else {
            $icon.addClass("far");
        }
    });    
};
