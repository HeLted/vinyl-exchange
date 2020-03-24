(function($) {
  "use strict";

  // PRE LOADER
  $(window).on("load", function() {
    $(".preloader")
      .delay(500)
      .slideUp("slow"); // set duration in brackets
  });

  $(window).scroll(function() {
    if ($(".navbar").offset().top > 50) {
      $(".navbar-fixed-top").addClass("top-nav-collapse");
    } else {
      $(".navbar-fixed-top").removeClass("top-nav-collapse");
    }
  });
})(jQuery);
