$(function() {
    $('.sortable').click(function(e) {
        // when the link is clicked
        // perform an ajax request:
        $.ajax({
            url: this.href,
            success: function(result) {
                // when the AJAX call succeed do something with the result
                // for example if the controller action returned a partial
                // then you could show this partial in some div
				$('#tableDiv').html(result);
            }
        });

        // don't forget to cancel the default action by returning false
        return false;
    });
});