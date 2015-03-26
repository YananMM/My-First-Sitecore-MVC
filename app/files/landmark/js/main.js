function getScrollBarWidth () {
  var inner = document.createElement('p');
  inner.style.width = "100%";
  inner.style.height = "200px";

  var outer = document.createElement('div');
  outer.style.position = "absolute";
  outer.style.top = "0px";
  outer.style.left = "0px";
  outer.style.visibility = "hidden";
  outer.style.width = "200px";
  outer.style.height = "150px";
  outer.style.overflow = "hidden";
  outer.appendChild (inner);

  document.body.appendChild (outer);
  var w1 = inner.offsetWidth;
  outer.style.overflow = 'scroll';
  var w2 = inner.offsetWidth;
  if (w1 == w2) w2 = outer.clientWidth;

  document.body.removeChild (outer);

  return (w1 - w2);
};

var 
scrollBarWidth = getScrollBarWidth(),
mq = {
	'phone': function(){return $(window).width() + scrollBarWidth < 768},                          	// phone
	'pad':  function(){return $(window).width() + scrollBarWidth >= 768 && $(window).width() + scrollBarWidth < 1000},// pad
	'desktop': function(){return $(window).width() + scrollBarWidth >= 1000}                        	// desktop
},
isScreenWidth = function(device) {
	return mq[device]();    
},
isPhone = function() {
	return !isIE8() && isScreenWidth('phone');
},
isPad = function() {
	return !isIE8() && isScreenWidth('pad');
},
isDesktop = function(){
	return isIE8() || isScreenWidth('desktop');
},
isIE8 = function(){
	return /MSIE 8.0/.test(navigator.userAgent);
},
isAndroid = function(){
	return /Android/.test(navigator.userAgent);
},
isiPhone = function(){
	return /iPod|iPhone/.test(navigator.userAgent);
},
isiPad = function(){
	return /iPad/.test(navigator.userAgent);
},
isiOS = function(){
	return /iPod|iPhone|iPad/.test(navigator.userAgent);
},
getResponseEnv = function(){
	return isDesktop() 
		? 'desktop'
		: isPad()
			? 'pad'
			: 'phone';
};

jQuery(function($){
$(document).ready(function() {    
	/**********************************************************************************************************
	 * Navbar
	 **********************************************************************************************************/
	var responseEnv = getResponseEnv(),
		$navbar = $('.site-header'),
		$footer = $('.site-footer'),
		$mega = $('nav.mega-menu'),
		$mobileMainMenuHandle = $('.container.visible-xs-block .lines-button', $navbar),
		$mobileMainMenu = $('.container.visible-xs-block .mobile-menu', $navbar),
		$backToTopBar = $('.back-top'),
		$backToTopBtn = $('.back-to-top'),
		navHL = 165,
		navHS = 70,
		navHM = 45,
		isScrolling = false,
		origScrollPos = 0,
		backToTopFadeInAnimation = new TimelineMax()
		.add(
			TweenMax.fromTo($backToTopBtn, .5, {autoAlpha: 0}, {autoAlpha: 1}),
			0
		)
		.pause(); 

	/**********************************************************************************************************
	 * Response Checkpoint
	 **********************************************************************************************************/
	function handleResponsive(){
		if(getResponseEnv() != responseEnv){
			window.location.reload(true);
		}
	}
	$(window).on('resize', handleResponsive);

	/**********************************************************************************************************
	 * Back To Top Visibility
	 **********************************************************************************************************/
	$(window).on('scroll.backToTopBar touchmove.backToTopBar', function(){
		if(isScrolling || !$backToTopBar.length) 
			return;

		if ($('body').height() > $(window).height() 
			&& $(window).scrollTop() + $(window).height() > $backToTopBar.offset().top
			)
		{
			backToTopFadeInAnimation.play();
		} else {
			backToTopFadeInAnimation.reverse();
		}
	});	

	/**********************************************************************************************************
	 * Portrait Only on Phone
	 **********************************************************************************************************/
	$.scrollTo(window.location.hash || 0, 400);
	$(window).on('orientationchange', function(){
		window.location.reload(true);
	});

	if(isPhone() && $(window).height() < $(window).width()){
		$('.portrait_only').removeClass('hidden');
	}

	/**********************************************************************************************************
	 * Menu
	 **********************************************************************************************************/
	if(isDesktop()){
		var mainNavShrinkAnimation = new TimelineMax()
			.addCallback(function(){
				$navbar.removeClass('clover');
			})
			.add(
				TweenMax.fromTo($navbar, .5, {height: navHL}, {height: navHS}),
				0
			)
			.add(
				TweenMax.to($('.menu-left, .menu-right', $navbar), .5, {marginTop: "-=105px" }),
				0
			)
			.add(
				TweenMax.to($('nav h1', $navbar), .5, {marginTop: "-=12px"}),
				0
			)
			.add(
				TweenMax.to($('.responsive-logo', $navbar), .5, {paddingTop: "0" }),
				0
			)
			.add(
				TweenMax.to($('.img-logo', $navbar), .5, {y: "-=81px", autoAlpha: "-=1" }),
				0
			)			
			.add(
				TweenMax.fromTo($mega, .5, {top: navHL}, {top: navHS}),
				0
			)
			.add(
				TweenMax.to('.panel-nav', .5, {top: "-=95px"}),
				0
			)
			.add(
				TweenMax.to($('.language', $navbar), .5, {top: "-=95px"}),
				0
			)
			.addCallback(function(){
				$navbar.addClass('clover');
			})
			.pause();
	
		$(window).on('scroll.mainNavShrinkAnimation touchmove.mainNavShrinkAnimation', function(){
			if(isScrolling) 
				return;

			if( $(window).scrollTop() > navHS){
				mainNavShrinkAnimation.play();
			} else {
				mainNavShrinkAnimation.reverse();
			}
		});	

		// Back To Top
		$backToTopBtn.on('click', function(e){
			e.preventDefault();
			isScrolling = true;
			mainNavShrinkAnimation.reverse();
			$.scrollTo(0, 2000, {
				onAfter: function(){							
					isScrolling = false;
				}
			});
		});

		// mega menu
		function handleMegaMenu(){	// wait for IE8 ready
			$('.container.hidden-xs nav ul li', $navbar).each(function(){
				var $handle = $(this),
					$mega = $('a + nav.mega-menu', $handle);
				if ($mega.length){
					$mega
					.css({
						'width': $(window).width(),
						'top': $navbar.height()
					});

					function showMega(){
						var $visibleMega = $('nav.mega-menu:visible').not($mega);
						if ($visibleMega.length){
							$mega.stop().slideDown(0);
							$visibleMega.stop().slideUp(0);
						} else {
							$mega.stop().slideDown(1000);
						}
					}

					function hideMega(){
						$mega.stop().slideUp(1000);
					}

					$handle
					.off('mouseenter').on('mouseenter', showMega)
					.off('mouseleave').on('mouseleave', hideMega)
					.off('click', '> a').on('click', '> a', function(e){	// for touch devices
						e.preventDefault();
						if($mega.is(':visible')){
							hideMega();
						} else {
							showMega();
						}
					});
				}
			});
		}
		$(window).on('resize', handleMegaMenu);
	} else {
		// mobile menu
		if (!isIE8()){
			var menuScroll = new IScroll($mobileMainMenu[0], {
			    mouseWheel: true,
			    click: true
			});
		}
		var mobileMainMenuAnimation = new TimelineMax()
			.add(
				TweenMax.fromTo($mobileMainMenu, .5, {left: "-100%"}, {left: "0%"}),
				0
			)
			.add(TweenMax.fromTo($('body > *').not('.site-header'), .5, {autoAlpha: 1}, {autoAlpha: .3}),
				0
			)
			.pause();
		function handleMobileMenuHeight(){
			$mobileMainMenu.css('height', $(window).height() - navHM);
			if (!isIE8()){
				menuScroll.refresh();
			}
		}
		$mobileMainMenuHandle.on('click', function(e) {
			e.preventDefault();
			if ($(this).is('.button-close')){
				mobileMainMenuAnimation.reverse();
				$('html, body')
				.css({
					'height': '',
					'overflow-y': ''
				});
			} else {
				mobileMainMenuAnimation.play();	
				$('html, body')
				.css({
					'height': '100%',
					'overflow-y': 'hidden'
				});
			}
			$mobileMainMenuHandle.toggleClass('button-close');
		});

		$('li > a:has(+ ul)', $mobileMainMenu).on('click', function(e){
			e.preventDefault();
			$(this).parent().toggleClass('active');
			$(this).next('ul').slideToggle(handleMobileMenuHeight);
		});

		$(window).on('resize', handleMobileMenuHeight);
	}

	// Search Box
	$('.sb-search').each(function(){
		var $searchIcon = $('.icomoon-search', this),
			$searchBox = $(this),
			$searchInput = $('input[type=search]:first', this);
		$searchIcon.click(function(e){
			e.preventDefault();
			if($searchBox.is('.sb-search-open')){
				$searchBox.removeClass('sb-search-open');
			} else {
				$searchBox.addClass('sb-search-open');
				origScrollPos = $(window).scrollTop();
				if(isiOS()){
					isScrolling = true;
					$fades = $('body > *:visible').not($navbar);
					TweenMax.to($fades, 0, {autoAlpha: 0});

					setTimeout(function(){
						isScrolling = false;
						TweenMax.to($fades, .4, {autoAlpha: 1});

						// iOS focus trigger scroll issue
						$.scrollTo(origScrollPos, 0);

						$navbar.css('top', origScrollPos);
					}, 400);
				}
				$searchInput.focus();
			}
		});
		$('body').on('click touchstart', function(event){
			if ($(event.target).parents('.sb-search').length < 1 && $searchBox.hasClass('sb-search-open')){
				$searchBox.removeClass('sb-search-open');
				$searchInput.blur();
				$.scrollTo(origScrollPos, 0);
				if (isiOS){
					$navbar.css('top', '');
				}
			}
		});
	});
	

	/**********************************************************************************************************
	 * popup handling
	 * ref: http://www.vagabond.com/cn/Men/Autumn-Collection/
	 **********************************************************************************************************/
	function bindPopups() {

		$('.popup-overlay').each(function() {
			if(!$(this).parent().hasClass("wrapper")) {
				$(this).detach().appendTo("body");
			}
		});
		
		$('.popup-overlay a.close-popup').click(function () {
			var $popup = $(this).closest('.popup-overlay');
			$popup.find('.popup-content').fadeTo(100, 0, function() {
				$popup.addClass("hide").fadeOut(100);
				$('body').removeClass("popup-show").css("height", "auto");
			});

			$('html, body').animate({ scrollTop: origScrollPos }, 100).css(
				(isAndroid() ? 'overflow-y': 'overflow'), '');
	        return false;
	    });
	    
	    $('.popup-trigger').click(function(e) {
	    	e.preventDefault();
		    var sel = $(this).data("popup");
		    if(sel != "") {
		        showPopup(sel);
		    }
		    return false;
	    });
	    
	    $(window).resize(function() {
		    popupResize();
	    });
	}


	function showPopup(sel) {

		var winH = $(window).height();
		origScrollPos = $(window).scrollTop();
		
		$('html, body').animate({ scrollTop: 0 }, 100);
		$(sel).removeClass("hide").fadeIn(100, function() {
			popupResize();
			$(this).find('.popup-content').fadeTo(100, 1);
		});
		
	}

	function popupResize() {
		
		var winH = $(window).height(), 
			navH = $navbar.outerHeight(true), 
			mainH = winH - navH;
		
		$('.popup-overlay').each(function() {
			if(!$(this).is(":visible")) {
				return true;
			}
			$(this).css('top', navH)
			var popupContentH = $(this).find('.popup-content').outerHeight(true);
			if(popupContentH > mainH) {
				$(this).css('height', popupContentH + 'px').css('overflow', 'auto');
				var style = isAndroid() 
					? {'overflow-y': 'scroll'}
					: {'overflow': 'hidden'};			
				$('html, body').css('height', (popupContentH + navH) + 'px')
				.css(style);
			} else {
				$(this).css('height', mainH + 'px');
				$('html, body').css('height', winH + 'px');
			}
		});
	}


	/**********************************************************************************************************
	 * Home
	 **********************************************************************************************************/
    $('.t1').each(function(){
    	var	$layout = $('.panel-layout'),
			$panelNav = $('.panel-nav'),
			currentSlide = 0,
			totalSlides = $('.panel-area', $layout).length,
			panelHs = [],
			scrollPositions = [],
			inAnimations = [],
			outAnimations = [],
			panelDuration = 1.5,
			footerDuration = 1,
			easing = Quad.easeInOut;

		// turn off default animation handling
		$(window).off('scroll.mainNavShrinkAnimation touchmove.mainNavShrinkAnimation');
		$(window).off('scroll.backToTopBar touchmove.backToTopBar');

    	function handelPanelLayout(){   
    		// parallax panel layout 
    		var winH = $(window).height();

            $('li:eq(' + currentSlide + ')', $panelNav).addClass('active');

			// Setup Panel Animations
	    	$('.panel-area', $layout).each(function(i){
    			var $panel = $(this),
    				$slider = $('.slider', this),
	    			navH = isDesktop() ? i == 0 ? navHL : navHS : navHM,	    						
	    			panelH = panelHs[i] = winH - navH,
	    			scrollPos = scrollPositions[i] = i == 0 ? 0 : (scrollPositions[i-1] + panelH);

	    		// store and set heights
				$panel.add($slider).add('li', $slider).add($slider.parents('.bx-wrapper, .bx-viewport')).css({
	    			'height': panelH
	    		});
	    		$panel.css('z-index', i*10)

		    	// animation
	    		function setAnimation() {
		    		var	$text = $('.panel-text', $panel),
		    			inAnimation = inAnimations[i] = new TimelineMax({paused: true}),
		    			outAnimation = outAnimations[i] = new TimelineMax({paused: true});
					if(!isIE8()){
						if (i > 0 && isDesktop()){
			    			inAnimation.fromTo($text, .4, 			// tween text in	
								{y: 100, autoAlpha: 0},
								{y: 0, autoAlpha: 1, ease: easing},
								panelDuration
							);
			    		}
					
						// tween panel out
						if (i < totalSlides - 1){
							outAnimation.fromTo($panel, panelDuration,
								{y: 0},
								{y: panelH / 2, ease: easing}
							);
						}
					}
	    		}

	    		if ($slider.length && !$slider.data('slider')){
		    		var slider = $slider.bxSlider({
		    			speed: 1000,
		    			auto: true,
		    			pause: 5000,
		    			pager: false,
		    			controls: false,
		    			oneToOneTouch: false,
		    			useCSS: false,
		    			onSliderLoad: function(){
		    				setAnimation();
		    				$(window).lazyLoadXT();
		    				setTimeout(function(){
			    				$(window).trigger('resize')
		    				}, 100)
		    			},
		    			onSlideAfter: function(){
		    				$(window).trigger('lazyupdate')
		    			}
		    		});
		    		$slider.data('slider', slider);

		    		$('.prev', $panel).click(function(e){
		    			e.preventDefault();
		    			slider.goToPrevSlide();
		    		});
		    		$('.next', $panel).click(function(e){
		    			e.preventDefault();
		    			slider.goToNextSlide();
		    		});
	    		} else {
	    			setAnimation();
	    		}
	    	});

			// add footer position			
			scrollPositions[totalSlides] = scrollPositions[totalSlides - 1] + $backToTopBar.outerHeight(true) + $footer.outerHeight(true);
			var footerInAnimation = inAnimations[totalSlides] = new TimelineMax({paused: true}).to($backToTopBtn, .5, {autoAlpha: 1}),
				footerOutAnimation = outAnimations[totalSlides] = new TimelineMax({paused: true}).to($backToTopBtn, .5, {autoAlpha: 0});

			if (!isIE8() && $(window).scrollTop() != scrollPositions[currentSlide]){
				goToSlide(currentSlide);
			}
    	}

        // Methods
		function goToSlide(slideIndex){
			if (scrollPositions[slideIndex] !== undefined){
				isScrolling = true;
				var previousSlide = currentSlide; // used for callback, because when animation stops, currentSlide will change
				var direction = slideIndex == currentSlide ? 'stay' : slideIndex < currentSlide ? 'prev' : 'next';
				var slider = $('.panel-area:eq('+currentSlide+') .slider', $layout).data('slider');
				if (slider){
					slider.stopAuto();
				}
				if (isDesktop()){
					if(slideIndex == 0) {
						mainNavShrinkAnimation.reverse();
					} else {						
						mainNavShrinkAnimation.play();
					}
				}
				switch(direction){
					case 'stay':
						if (slideIndex > 0){
							inAnimations [slideIndex -1].progress(1);
							outAnimations[slideIndex -1].progress(1);
						}
						inAnimations [slideIndex  ].progress(1);
						break;
					case 'prev':
						outAnimations[slideIndex  ].reverse();
						inAnimations [slideIndex  ].progress(0).play();
						setTimeout(function(){
							inAnimations [previousSlide].progress(0).reverse();
						}, panelDuration * 1000);
						break;
					case 'next':
						outAnimations[slideIndex  ].reverse();
						inAnimations [slideIndex  ].progress(0).play();
						outAnimations[currentSlide].play();
						setTimeout(function(){
							inAnimations [previousSlide].progress(0).reverse();
						}, panelDuration * 1000);
						break;
				}
				var animElements = isIE8() 
					? $layout 
					: $layout.add($backToTopBar).add($footer),
					animProps = isIE8()
					? {
						marginTop: -scrollPositions[slideIndex],
						ease: easing
					}
					: {
						y: -scrollPositions[slideIndex],
						ease: easing
					};

				TweenLite.to(
					animElements, 
					slideIndex != 0 && (currentSlide == totalSlides || slideIndex == totalSlides)
						? footerDuration
						: panelDuration,
					animProps
				)
				.eventCallback('onComplete', function(){			
					isScrolling = false;

	    			currentSlide = slideIndex;
	    			$panelNav.find('li.active').removeClass('active');
					$('li:eq('+currentSlide+')', $panelNav).addClass('active');
		    		
					$(window).trigger('scroll');
					$(window).lazyLoadXT();
					var slider = $('.panel-area:eq('+slideIndex+') .slider', $layout).data('slider');
					if (slider){
						slider.startAuto();
					}
				});
			}
		}

		// Events
		// Nav Click
		$('li a', $panelNav).off('click').on('click.panelNav', function(e){
			e.preventDefault();
			goToSlide($('li', $panelNav).index($(this).parent('li')));
		});

		// Scroll To Begin
		$('.scroll-area', $layout).off('click').on('click.panelScrollBegin', function(e){
			e.preventDefault();
			goToSlide(1);
		});

		// Back To Top
		$backToTopBtn.off('click').on('click.panelScroll', function(e){
			e.preventDefault();
			goToSlide(0);
		});

        // Mousewheel
		$(document).off('mousewheel.panel').on('mousewheel.panel', function(e){
			e.preventDefault();
			if(isScrolling)
				return;

			if(e.deltaY < 0){
				goToSlide(currentSlide + 1);
			} else {
				goToSlide(currentSlide - 1)
			}
		});

		// Swipe
		$layout.swipe({
			allowPageScroll: 'none',
			excludedElements: null,
			fallbackToMouseEvents: false,
			tap: function(e){
				/*if($searchBox.is(':visible')){
					$searchIcon.focus().click();
				}*/
			},
			swipeUp: function(e){
				if(!isScrolling)
					goToSlide(currentSlide + 1);
			},
			swipeDown: function(e){
				if(!isScrolling)
					goToSlide(currentSlide - 1);
			}
		});
		
		// don't handle resize on mobile devices, in case of soft keyboard poping up
		if (isAndroid() || isiOS()) {
			handelPanelLayout();
		} else {
	    	$(window)
	    		.on('resize', handelPanelLayout);
    	}
    });

	/**********************************************************************************************************
	 * Men
	 **********************************************************************************************************/
	$('.t11').each(function(){
		if(!isPhone()){
			// key men box equal height, adjust variation 2 height according to variation 1 img height
			var $box2 = $('.menbox-2'),
			    $img1 = $('.menbox-1 .menbox-img img');
			if($box2.length && $img1.length){
				function adjustKeyMenBox(){
					origScrollPos = $(window).scrollTop();
					$box2.css({
						'height': $img1.outerHeight(true) + 'px'
					});
					$.scrollTo(origScrollPos, 0);
				}
				$(window).on('resize', adjustKeyMenBox);
				$img1.on('lazyload', adjustKeyMenBox);
			}

			// bottom men box equal height, adjust variation 4 height according to variation 3 img height
			var $box3 = $('.menbox-3'),
			    $img4 = $('.menbox-4 .menbox-img img');
			if($box3.length && $img4.length){
				function adjustBottomMenBox(){
					origScrollPos = $(window).scrollTop();
					$box3.parent('.menbox').css({
						'height': $img4.outerHeight(true) + 'px'
					});
					$.scrollTo(origScrollPos, 0);
				}
				$(window).on('resize', adjustBottomMenBox);
				$img4.on('lazyload', adjustBottomMenBox);
			}
		}
	});

	/**********************************************************************************************************
	 * Brands
	 **********************************************************************************************************/
	$('.t13').each(function(){
		$('.masonry-area .brand-box-area').masonry({
			itemSelector: '.brand-box',
			isFitWidth: true
		});

		$('select.selectpicker').selectpicker({
			size: false
		});


		// brands link scroll
		$('.brand-menu-area a[href^=#]').on('click', function(e){
			e.preventDefault();
			if($(this).parents('.popup-overlay').length){
				$(this).parents('.popup-overlay').find('.close-popup').trigger('click')
			}
			$.scrollTo($(this).attr('href'), 1000, {axis: 'y', offset: -$('.brand-list').offset().top + (isDesktop()? 160 : 70)})
		});

		if(!isPhone()){
			/*var $pageTitle = $('.page-title'),
				$sidebar = $('.brand-menu.hidden-xs'),
				titleH = $pageTitle.outerHeight() + $('.page-header').height();
			if (!isIE8()){
				var sidebarScroll = new IScroll($('.brand-menu-area', $sidebar)[0], {
				    mouseWheel: true,
				    scrollbar: true,
				    click: true
				});
			}
				
			function handleSidebar(){
				var scrollTop = $(window).scrollTop(), 
					winH = $(window).height(),
					targetY = scrollTop < titleH
						? 0
						: isDesktop()
							? scrollTop < navHS 
								? scrollTop - titleH
								: scrollTop - (titleH + (navHL - navHS))
							: scrollTop - titleH + 30;

				// overscroll bug occurs when screen size near 1920
				if (!isiOS()){
					setTimeout(function(){
						if (scrollTop >  $(document).height() - $(window).height()) {
							$.scrollTo($(document).height() - $(window).height(), 0)
						}
					}, 100);
				}

				$sidebar.css('top', targetY)
				var targetH = scrollTop < titleH 
					? winH + scrollTop - titleH - $navbar.outerHeight(true) - 30
					: winH - (scrollTop - targetY) + 30;
				// is footer visible?
				if (scrollTop + winH > $backToTopBar.offset().top) {
					targetH -= (scrollTop + winH - $backToTopBar.offset().top)
				}
				$('.brand-menu-area', $sidebar).css('height', targetH) 
				if (!isIE8()){
					sidebarScroll.refresh();
				}
				$('.selectpicker').selectpicker('refresh');
			}

			$(window)
			.on('scroll.handleSidebar touchmove.handleSidebar', handleSidebar)
			.on('resize', handleSidebar);*/
		} else{
			bindPopups();
		}

		// "GO TO" form
		var urlGetCategories = $('form.form-goto').data('handler-get-catetories'),
			categories = [];

		$.getJSON(urlGetCategories)
		.done(function(json){
			categories = json;
		})
		/*.fail(function(){
			console.log('fail', arguments)
		})*/;
		
		// handle each parent category select
		$('form.form-goto').each(function(){
			var form = this,
				$form = $(this),
				$category = $('select[name=category]', form),
				$childcategory = $('select[name=childcategory]', form);

			$category.on('change', function(){
				var categoryval = $category.val(),
					children = [];

				for(var i = 0; i < categories.length; i++){
					var category = categories[i];
					if(category.value == categoryval){
						children = category.children;
					}
				}

				$childcategory.html('');
				if(children.length) {
					$childcategory.removeAttr('disabled');
				} else {
					$childcategory.attr('disabled', 'disabled');
				}
				for(var j = 0; j < children.length; j++){
					var child = children[j];
					$('<option value="' + child.value + '">' + child.text.toUpperCase() +'</option>').appendTo($childcategory);
				}
				$childcategory.selectpicker('refresh')
			})
		})

	})

	//IE8 Header scroll fix
	if (isIE8){
		$(window).on('scroll', function(){
			$navbar.css({'left': '-' + $(this).scrollLeft() + 'px'});
		});

		var startWidth = $(window).width();
		$(window).on('resize', function(){
			if ($(window).width() - startWidth > 200){
				window.location.reload(true);
			}
		});
	}

	// trigger resize once to fire all the adjustments
	$(window).resize();
});
});