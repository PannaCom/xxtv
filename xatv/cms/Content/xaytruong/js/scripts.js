$(function () {
    $('.nav_menu').hover(function () {
        $('.dropdown-toggle', this).trigger('click');
    });


})

$(document).ready(function () {
    $('[data-toggle=offcanvas]').click(function () {
        $('.row-offcanvas').toggleClass('active');
        $('.showhide').toggle();
    });
});