/*
 *
 *   INSPINIA - Responsive Admin Theme
 *   version 2.0
 *
 */

jQuery(document).ready(function () {

    // Add body-small class if window less than 768px
    if (jQuery(this).width() < 769) {
        jQuery('body').addClass('body-small');
    } else {
        jQuery('body').removeClass('body-small');
    }

    // MetsiMenu
    jQuery('#side-menu').metisMenu();

    // Collapse ibox function
    jQuery('.collapse-link').click(function () {
        var ibox = jQuery(this).closest('div.ibox');
        var button = jQuery(this).find('i');
        var content = ibox.find('div.ibox-content');
        content.slideToggle(200);
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        ibox.toggleClass('').toggleClass('border-bottom');
        setTimeout(function () {
            ibox.resize();
            ibox.find('[id^=map-]').resize();
        }, 50);
    });

    // Close ibox function
    jQuery('.close-link').click(function () {
        var content = jQuery(this).closest('div.ibox');
        content.remove();
    });

    // Close menu in canvas mode
    jQuery('.close-canvas-menu').click(function () {
        jQuery("body").toggleClass("mini-navbar");
        SmoothlyMenu();
    });

    // Open close right sidebar
    jQuery('.right-sidebar-toggle').click(function () {
        jQuery('#right-sidebar').toggleClass('sidebar-open');
    });

    // Initialize slimscroll for right sidebar
    jQuery('.sidebar-container').slimScroll({
        height: '100%',
        railOpacity: 0.4,
        wheelStep: 10
    });

    // Open close small chat
    jQuery('.open-small-chat').click(function () {
        jQuery(this).children().toggleClass('fa-comments').toggleClass('fa-remove');
        jQuery('.small-chat-box').toggleClass('active');
    });

    // Initialize slimscroll for small chat
    jQuery('.small-chat-box .content').slimScroll({
        height: '234px',
        railOpacity: 0.4
    });

    // Small todo handler
    jQuery('.check-link').click(function () {
        var button = jQuery(this).find('i');
        var label = jQuery(this).next('span');
        button.toggleClass('fa-check-square').toggleClass('fa-square-o');
        label.toggleClass('todo-completed');
        return false;
    });

    // Minimalize menu
    jQuery('.navbar-minimalize').click(function () {
        jQuery("body").toggleClass("mini-navbar");
        SmoothlyMenu();

    });

    // Tooltips demo
    jQuery('.tooltip-demo').tooltip({
        selector: "[data-toggle=tooltip]",
        container: "body"
    });

    // Move modal to body
    // Fix Bootstrap backdrop issu with animation.css
    //jQuery('.modal').appendTo("body");
    jQuery('.modal').appendTo("from");

    // Full height of sidebar
    function fix_height() {
        var heightWithoutNavbar = jQuery("body > #wrapper").height() - 61;
        jQuery(".sidebard-panel").css("min-height", heightWithoutNavbar + "px");

        var navbarHeigh = jQuery('nav.navbar-default').height();
        var wrapperHeigh = jQuery('#page-wrapper').height();

        if (navbarHeigh > wrapperHeigh) {
            jQuery('#page-wrapper').css("min-height", navbarHeigh + "px");
        }

        if (navbarHeigh < wrapperHeigh) {
            jQuery('#page-wrapper').css("min-height", jQuery(window).height() + "px");
        }

    }
    fix_height();

    // Fixed Sidebar
    jQuery(window).bind("load", function () {
        if (jQuery("body").hasClass('fixed-sidebar')) {
            jQuery('.sidebar-collapse').slimScroll({
                height: '100%',
                railOpacity: 0.9
            });
        }
    })

    // Move right sidebar top after scroll
    jQuery(window).scroll(function () {
        if (jQuery(window).scrollTop() > 0 && !jQuery('body').hasClass('fixed-nav')) {
            jQuery('#right-sidebar').addClass('sidebar-top');
        } else {
            jQuery('#right-sidebar').removeClass('sidebar-top');
        }
    });

    jQuery(document).bind("load resize scroll", function () {
        if (!jQuery("body").hasClass('body-small')) {
            fix_height();
        }
    });

    jQuery("[data-toggle=popover]")
        .popover();

    // Add slimscroll to element
    jQuery('.full-height-scroll').slimscroll({
        height: '100%'
    })
});


// Minimalize menu when screen is less than 768px
jQuery(window).bind("resize", function () {
    if (jQuery(this).width() < 769) {
        jQuery('body').addClass('body-small')
    } else {
        jQuery('body').removeClass('body-small')
    }
});


function SmoothlyMenu() {
    if (!jQuery('body').hasClass('mini-navbar') || jQuery('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        jQuery('#side-menu').hide();
        // For smoothly turn on menu
        setTimeout(
            function () {
                jQuery('#side-menu').fadeIn(500);
            }, 100);
    } else if (jQuery('body').hasClass('fixed-sidebar')) {
        jQuery('#side-menu').hide();
        setTimeout(
            function () {
                jQuery('#side-menu').fadeIn(500);
            }, 300);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        jQuery('#side-menu').removeAttr('style');
    }
}

