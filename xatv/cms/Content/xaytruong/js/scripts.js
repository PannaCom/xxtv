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

    $('.footer_album').flexslider({
        animation: "slide",
        controlNav: true,
        directionNav: false,
        draggable: false,
    });


   
});

var wow = new WOW(
  {
      boxClass: 'wowload',      // animated element css class (default is wow)
      animateClass: 'animated', // animation css class (default is animated)
      offset: 0,          // distance to the element when triggering the animation (default is 0)
      mobile: true,       // trigger animations on mobile devices (default is true)
      live: true        // act on asynchronously loaded content (default is true)
  }
);
wow.init();

// scroll top
//==========================================    
$(window).scroll(function () {
    $(this).scrollTop() > 500 ? $('.totop').fadeIn() : $('.totop').fadeOut();
});
if ($('.totop').length) {
    $('.totop').click(function () {
        $('html,body').animate({ scrollTop: 0 }, 500);
        return false;
    })
}

//$(function() {
//    var url = window.location;
//    // var element = $('ul.nav a').filter(function() {
//    //     return this.href == url;
//    // }).addClass('active').parent().parent().addClass('in').parent();
//    var element = $('ul.nav a').filter(function () {
//        return this.href == url;
//    }).addClass('active').parent();

//    while (true) {
//        if (element.is('li')) {
//            element = element.parent().addClass('in').parent();
//        } else {
//            break;
//        }
//    }
//});