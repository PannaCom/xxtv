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

function getDayLeft(s, d) {
    $(s).countdown(d)
    .on('update.countdown', function (event) {
        var format = '';
        if (event.offset.totalDays > 0) {
            format = 'Còn lại %-d ngày';
        }
        //if (event.offset.weeks > 0) {
        //    format = 'Còn lại %-w tuần %!w ' + format;
        //}
        if (event.offset.totalDays = 0) {
            format = 'Hoàn tất';
        }
        $(this).html(event.strftime(format));
    })
    .on('finish.countdown', function (event) {
        $(this).html(event.strftime(format));

    });
}

$(function() {
    var url = window.location;
    // var element = $('ul.nav a').filter(function() {
    //     return this.href == url;
    // }).addClass('active').parent().parent().addClass('in').parent();
    var element = $('.categoryList a').filter(function () {
        return this.href == url;
        
    }).addClass('cat_active').parent();

    while (true) {
        if (element.is('li')) {

            element = element.addClass('cat_active').parent();
        } else {
            break;
        }
    }

    var element2 = $('#select_orderby a').filter(function () {
        return this.href == url;
    }).addClass('tab_active').parent();
    
    while (true) {
        if (element2.is('li')) {
            element2 = element2.addClass('tab_active').parent();            
        } else {
            break;
        }
    }

    //$('.categoryList > ol > li').find('a').on('click', function () {
    //    var e1 = $(this).attr('href');
    //    console.log(e1);
    //    console.log(url.pathname);
    //    if (e1 == url.pathname) {
    //        $('.categoryList > ol > li').find('a[href^="' + e1 + '"]').addClass('cat_active').parent();
    //    }
    //})

   
    

});



var addUrlParam = function (search, key, val) {
    var newParam = key + '=' + val,
        params = '?' + newParam;

    // If the "search" string exists, then build params from it
    if (search) {
        // Try to replace an existance instance
        params = search.replace(new RegExp('([?&])' + key + '[^&]*'), '$1' + newParam);

        // If nothing was replaced, then add the new param to the end
        if (params === search) {
            params += '&' + newParam;
        }
    }
    return params;
};