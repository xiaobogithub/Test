var sessionsPosition = 'collapsed';

//When resizing the window it waits 300ms after the resizing is performed before it executes
$(window).resize(function() {
        if(this.resizeTO) clearTimeout(this.resizeTO);
        this.resizeTO = setTimeout(function() {
            $(this).trigger('resizeEnd');
        }, 300);
});

$(function() {
  $(document).ready(function(){
	  
  
	/* Expanding and collapsing the sessions list - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
	$(".button-expand").click(function(){
	
		$(".wrapper-sessionslist").css('left', '0%');
		$("body").css('overflow-y', 'hidden');
		sessionsPosition = 'expanded'
		return false
	
  });
  
	$(".button-collapse").click(function(){
	
		$(".wrapper-sessionslist").css('left', '100%');
		$("body").css('overflow-y', 'auto');
		sessionsPosition = 'collapsed'
		return false
	
  });
  
});


	/* Checking the state of the sessionslist after a window resize - - - - - - - - - - - - - - - */
	
	$(window).bind('resizeEnd', function() {
    
	// Making sure the sessions list stays in the main area
	if ($(window).width() > 768) {
	
		$(".wrapper-sessionslist").css('position', 'relative');
		$(".wrapper-sessionslist").css('left', '0%');
		$("body").css('overflow-y', 'auto');
		sessionsPosition = 'collapsed';
	
	}
	
	// Making sure the list stays collapsed
	else if (sessionsPosition == 'collapsed' && $(window).width() < 767){
	  
		$(".wrapper-sessionslist").css('position', 'fixed');
		$(".wrapper-sessionslist").css('left', '100%');
		sessionsPosition = 'collapsed';

	}
	
});



	/* Toggles the sessions in sessionslist to expand/collapse - - - - - - - - - - - - - - - - */
    
	// Resetting
	$('.session-content').slideUp(0);
	
	// Opening active session
	$('.active .session-content').slideDown(0);
	$('.session-container.active').css('background' , '#e4f0fb');
	$('.active .plusminus').css('backgroundPosition' , '0 -40px');
	
	// Actual function
    $('.session-container').click(function() {
        
		$('.session-content').slideUp(200);
		$('.plusminus').css('backgroundPosition' , '0 0px');
		$(this).children('.plusminus').css('backgroundPosition' , '0 -40px');
		$(this).children('.session-content').slideDown(400);
		
    });
	
	
	
	/* Toggles the password box on login page to expand/collapse - - - - - - - - - - - - - - - - */
	
	// Resetting
	$('.forgotpassword').slideUp(0);
	
	// Actual function
    $('.button-forgotpassword').click(function() {
        
		$('.forgotpassword').slideToggle(500);
		return false
		
    });

});


/* Scales the program header text according to available width - - - - - - - - - - - - - - - - */
// FitText.js 1.1 Copyright 2011, Dave Rupert http://daverupert.com

(function( $ ){

  $.fn.fitText = function( kompressor, options ) {

    // Setup options
    var compressor = kompressor || 1,
        settings = $.extend({
          'minFontSize' : Number.NEGATIVE_INFINITY,
          'maxFontSize' : Number.POSITIVE_INFINITY
        }, options);

    return this.each(function(){

      // Store the object
      var $this = $(this);

      // Resizer() resizes items based on the object width divided by the compressor * 10
      var resizer = function () {
        $this.css('font-size', Math.max(Math.min($this.width() / (compressor*10), parseFloat(settings.maxFontSize)), parseFloat(settings.minFontSize)));
      };

      // Call once to set.
      resizer();

      // Call on resize. Opera debounces their resize by default.
      $(window).on('resize.fittext orientationchange.fittext', resizer);

    });

  };

})( jQuery );

