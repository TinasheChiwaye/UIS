function ShowSaveMessage(messageContainerDivId, displayMessage) {
    setTimeout(function () {
        jQuery(messageContainerDivId).append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + displayMessage + '</strong> </div>');
        jQuery(".alert").fadeTo(7000, 0).slideUp(1000, function () {
            jQuery(this).remove();
        });
    }, 100);
}