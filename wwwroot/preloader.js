/* preloader */
var $window = $(window);

$window.on('load', function () {
    $('#preloader').fadeOut('slow', function () {
        $(this).remove();
    });
});