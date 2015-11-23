jQuery.noConflict();
function getScrollBarWidth() {
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
  'phone': function(){return jQuery(window).width() + scrollBarWidth < 768},                           // phone
  'pad':  function(){return jQuery(window).width() + scrollBarWidth >= 768 && jQuery(window).width() + scrollBarWidth < 1000},// pad
  'desktop': function(){return jQuery(window).width() + scrollBarWidth >= 1000}                          // desktop
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
isIE9 = function(){
  return /MSIE 9.0/.test(navigator.userAgent);
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
},
getScMode =  function (){
  if (window.Sitecore) { 
    if (Sitecore.PageModes.PageEditor) {
     return 'pageeditor'; 
    }
    return 'preview'; 
  } 
  return 'visitor'; 
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
    origScrollPos = 0;

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
   * Portrait Only on Phone
   **********************************************************************************************************/
  $.scrollTo(window.location.hash || 0, 400);
  $(window).on('orientationchange', function(){
    window.location.reload(true);
  });

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
        TweenMax.to($('nav .logo-area', $navbar), .5, {marginTop: "-=12px"}),
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
    function handleMegaMenu(){  // wait for IE8 ready
      $('.container.hidden-xs nav ul li', $navbar).each(function(){
        var $handle = $(this),
          $mega = $('a + nav.mega-menu', $handle),
          megaWidth;
        if ($mega.length){
          $mega
          .css({
              'width': $('body').width(),
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
          .off('click', '> a').on('click', '> a', function(e){  // for touch devices
            var href = $(this).attr('href');
            if(href != '#' && href != '')
              return true;
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
    // if (!isIE8()){
    //   var menuScroll = new IScroll($mobileMainMenu[0], {
    //     mouseWheel: true,
    //     click: true
    //   });
    // }
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
      // if (!isIE8()){
      //   menuScroll.refresh();
      // }
    }
    $mobileMainMenuHandle.on('click', function(e) {
      e.preventDefault();
      if ($(this).is('.button-close')){
        mobileMainMenuAnimation.reverse();
        if (!$('body').hasClass('t1')){
          $('html, body')
          .css({
            'height': '',
            'overflow-y': ''
          });
        }
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
    
    // Scroll top on mobile devices
    var gdJumpTop = function() {
      var gdJumpHeight = $(window).scrollTop();
      if (gdJumpHeight > 0) {
        $(window).scrollTop(gdJumpHeight - gdJumpHeight / 5);
        setTimeout(function() {
          gdJumpTop();
        }, 30);
      }
    };
    $backToTopBtn.on('click', function() {
      gdJumpTop();
    });
  }


  // Search Box
  $('.sb-search').each(function(){
    var $searchIcon  = $('.icomoon-search', this),
        $searchBox   = $(this),
        $searchInput = $('input[type=search]:first', this),
        $searchClear = $('.sb-search-clear', this),
        pageScroll   = 0;
    $searchIcon.click(function(e){
      e.preventDefault();
      if($searchBox.is('.sb-search-open')){
        $searchInput.blur();
        $searchBox.removeClass('sb-search-open');
        if (isiOS()){
          $(window).scrollTop(pageScroll);
        }
      } else {
        pageScroll = $(window).scrollTop();
        $searchBox.addClass('sb-search-open');
        $searchInput.focus();
      }
    });
    $searchClear.click(function(){
      $(this).removeClass('active').parent().find('.sb-search-input').val('').focus();
    });
    $searchInput.on('keyup', function(){
      if ($(this).val().length > 0){
        $(this).parent().find('.sb-search-clear').addClass('active');
      }else{
        $(this).parent().find('.sb-search-clear').removeClass('active');
      }
    });
    $('body').on('touchend click', function(event){
      if ($(event.target).parents('.sb-search').length < 1 && $searchBox.hasClass('sb-search-open')){
        $searchInput.blur();
        $searchBox.removeClass('sb-search-open');
        if (isiOS()){
          $(window).scrollTop(pageScroll);
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

  // **********************************************************************************
  // Social Sharing
  var socialShareUrl = $('head meta[property="og:url"]').attr('content') || window.location.href,
    socialShareTitle = $('head meta[property="og:title"]').attr('content') || $('head meta[name="title"]').attr('content') || "",
    socialShareDescription = $('head meta[property="og:description"]').attr('content') || $('head meta[name="Description"]').attr('content') || "",
    socialShareImages = [];

  $('head meta[property="og:image"]').each(function(){
    socialShareImages.push($(this).attr('content'));
  });

  // FB Share
  Modernizr.load({
    load: '//connect.facebook.net/en_US/sdk.js',
    callback: function(){
      window.fbAsyncInit = function() {
            FB.init({
              appId      : gdSettings.fbAppId,
              xfbml      : true,
              version    : 'v2.4'
            });
         };

      $('.header-social .icomoon-facebook, .gd-promo-sharebox .icomoon-facebook').click(function(e){
        e.preventDefault();
        var url;
        if($(this).parent().is('.gd-promo-sharebox')){
          url = $(this).parent().parent().prev('.gd-button').attr('href');
        } else {
          url = socialShareUrl
        }
        FB.ui(
        {
          method: 'share',
          href: url
        });
      });
    }
  });

  // Twitter Share
  Modernizr.load({
    load: 'https://platform.twitter.com/widgets.js',
    callback: function(){
      $('.header-social .icomoon-twitter, .gd-promo-sharebox .icomoon-twitter').each(function(){
        var url;
        if($(this).parent().is('.gd-promo-sharebox')){
          url = $(this).parent().parent().prev('.gd-button').attr('href');
        } else {
          url = socialShareUrl;
        }
        $(this).attr('href', 'https://twitter.com/intent/tweet?url='+ encodeURIComponent(url));
      });
    }
  });

  // Weibo Share
  Modernizr.load({
    load: 'http://tjs.sjs.sinajs.cn/open/api/js/wb.js?appkey=' + gdSettings.wbAppKey,
    callback: function(){
      WB2.anyWhere(function(W){
        $('.header-social .icomoon-sina-weibo, .gd-promo-sharebox .icomoon-sina-weibo').each(function(index){
          $(this).attr('id', 'wb_publish_'+index)
          var url, description, default_imagesArr = [];
          if($(this).parent().is('.gd-promo-sharebox')){
            url = $(this).parent().parent().prev('.gd-button').attr('href');
            var $promo = $(this).parents('.gd-promo-box2');
            description = $promo.find('meta[property="og:description"]').attr('content');
            $promo.find('meta[property="og:image"]').each(function(){
              default_imagesArr.push(encodeURIComponent($(this).attr('content')));
            });
          } else {
            url = socialShareUrl;
            description = socialShareDescription;
            for(var i=0; i<socialShareImages.length; i++){
              default_imagesArr.push(encodeURIComponent(socialShareImages[i]));
            }
          }
          
          W.widget.publish({
            id : 'wb_publish_'+index,
            action: "publish",
            type: isPhone() ? "mobile" : "web",
            language: $('body').hasClass('sc') ? "zh_cn" : "zh_tw",
            default_text : encodeURIComponent(description + url),
            default_image: default_imagesArr.join(','),
            callback : function(data) {
              // do something...
            }
          });
        });
      });
    }
  });

  // share by email
  $('.header-social .icomoon-envelop').attr('href', 'mailto:?subject='+ encodeURIComponent(socialShareTitle)+'&body='+encodeURIComponent(socialShareDescription));

  /**********************************************************************************************************
   * Home
   **********************************************************************************************************/
  $('.t1').each(function(){
    var $layout = $('.panel-layout'),
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

    if(isPhone() && $(window).height() < $(window).width()){
      $('.portrait_only').removeClass('hidden');
    }

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
          var $text = $('.panel-text', $panel),
            inAnimation = inAnimations[i] = new TimelineMax({paused: true}),
            outAnimation = outAnimations[i] = new TimelineMax({paused: true});
          if(!isIE8()){
            if (i > 0 && isDesktop()){
              inAnimation.fromTo($text, .4,       // tween text in  
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
            autoStart: getScMode() != 'pageeditor',
            pause: 5000,
            pager: false,
            controls: false,
            oneToOneTouch: false,
            useCSS: false,
            onSliderLoad: function(){
              setAnimation();
              $slider.find('*[data-bg]').each(function(){
                $(this).css('background-image', 'url(' +$(this).data('bg') + ')')
              });
              $(window).lazyLoadXT();
              setTimeout(function(){
                $(window).trigger('resize')
              }, 100)
            },
            onSlideAfter: function($slide){
              $(window).trigger('scroll');
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
            scPanelView('panel'+slideIndex);  // track panel view
            outAnimations[slideIndex  ].reverse();
            inAnimations [slideIndex  ].progress(0).play();
            setTimeout(function(){
              inAnimations [previousSlide].progress(0).reverse();
            }, panelDuration * 1000);
            break;
          case 'next':
            scPanelView('panel'+slideIndex);  // track panel view
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
          if (slider && getScMode() != 'pageeditor'){
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
    $layout.add($footer).swipe({
      allowPageScroll: 'none',
      excludedElements: null,
      fallbackToMouseEvents: false,
      tap: function(e){
        /*if($searchBox.is(':visible')){
          $searchIcon.focus().click();
        }*/
      },
      swipeUp: function(e){
        if(!isScrolling && currentSlide < totalSlides) {
          goToSlide(currentSlide + 1);
        }     },
      swipeDown: function(e){
        if(!isScrolling && currentSlide > 0) {
          goToSlide(currentSlide - 1);
        }
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
        $img1 = $('.menbox-1 .menbox-img img'),
        $img2 = $('.menbox-2 .menbox-img img');
      if($box2.length && $img1.length){
        function adjustKeyMenBox(){
          var box2ImageWrapHeight = $('.menbox-img', $box2).height();
          var box2ImageWrapWidth  = $('.menbox-img', $box2).width();
          var box2ImageHeight     = $('img', $box2).height();
          var box2ImageWidth      = $('img', $box2).width();
          
          origScrollPos = $(window).scrollTop();
          $box2.css({
            'height': $img1.outerHeight(true) + 'px'
          });
          $('img', $box2).css({
            'top': (box2ImageWrapHeight - box2ImageHeight) / 2
          });
          $('.menbox-img-mask', $box2).css({
            'height': box2ImageHeight,
            'width':  box2ImageWidth,
            'top':    (box2ImageWrapHeight - box2ImageHeight) / 2,
            'left':   (box2ImageWrapWidth - box2ImageWidth) /2
          });
          
          $.scrollTo(origScrollPos, 0);
        }
        $(window).on('resize', adjustKeyMenBox);
        $img1.on('lazyload', adjustKeyMenBox);
        $img2.on('lazyload', adjustKeyMenBox);
      }

      // bottom men box equal height, adjust variation 4 height according to variation 3 img height
      var $box3 = $('.menbox-3'),
        $img4 = $('.menbox-4 .menbox-img img');
      if($box3.length && $img4.length){
        function adjustBottomMenBox(){
          origScrollPos = $(window).scrollTop();
          // $box3.parent('.menbox').css({
          //   'height': $img4.outerHeight(true) + 'px'
          // });
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
    $('.brand-menu-area .brands-gap>li>a').on('click', function(e){
      
      var gdCurrentLoc = location.pathname;
          gdCurrentLoc = gdCurrentLoc.slice( gdCurrentLoc.lastIndexOf('/') );
      var gdAnchorLoc  = $(this).attr('href');
      var gdAnchor     = gdAnchorLoc.slice( gdAnchorLoc.indexOf('#') );
      
      if ( gdAnchorLoc.indexOf('/') > -1 ) {
        gdAnchorLoc = gdAnchorLoc.slice( gdAnchorLoc.lastIndexOf('/'), gdAnchorLoc.lastIndexOf('#') );
      } else if ( gdAnchorLoc.slice( 0, 1) === '#' ) {
        gdAnchorLoc = '';
      } else {
        gdAnchorLoc = gdAnchorLoc.slice( 0, gdAnchorLoc.indexOf('#') )
      }

      if ( gdAnchorLoc === gdCurrentLoc || gdAnchorLoc === '' ) {
        e.preventDefault();
        if($(this).parents('.popup-overlay').length){
          $(this).parents('.popup-overlay').find('.close-popup').trigger('click')
        }
        $.scrollTo(gdAnchor, 1000, {axis: 'y', offset: -$('.brand-list').offset().top + (isDesktop()? 160 : 70)})
      }
    });

    // T13 Anchor Link
    var gdAnchorLink = function() {
      var gdUrl = location.href;
      var gdTag;
      if (gdUrl.indexOf('#') > 0) {
        gdTag = gdUrl.slice(gdUrl.indexOf('#'));
        $.scrollTo(gdTag, 1000, {axis: 'y', offset: -$('.brand-list').offset().top + (isDesktop()? 160 : 70)})
      }
    }
    gdAnchorLink();

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
         $parentTemplateId = $('input[name=parentTemplateId]', form),
         $pageTemplateId = $('input[name=pageTemplateId]', form),
        $category = $('select[name=category]', form),
        $childcategory = $('select[name=childcategory]', form);

      if ($parentTemplateId.val() === $pageTemplateId.val()) {
          $childcategory.selectpicker('hide');
          $childcategory.attr('disabled', 'disabled');
      }

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
          $childcategory.selectpicker('show');
          $childcategory.removeAttr('disabled');
        } else {
          $childcategory.selectpicker('hide');
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
  if (isIE8()){
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

  //T1.html scroll problem fix
  if ($('body').hasClass('t1')){
    $('html, body').css({'height':'100%', 'overflow': 'hidden'});
    
    //iPad browser scroll problem fix
    $('.site-footer, .back-top').on('touchmove', function(){
      return false;
    });
    $('.site-footer, .back-top').on('touchstart', function(event){
      if ($(event.target).prop('tagName').toLowerCase() !== 'a'){
        return false;
      }
    });
    //iPad browser double tap problem fix
    $('body').on('touchend', function(){
      var curTime = new Date().getTime();
      var tapLast = $(this).data('tapLast') || curTime;
      var tapGap  = curTime - tapLast;
      if (tapGap < 500 && tapGap > 0){
        return false;
      }
      $(this).data('tapLast', curTime);
    });
  }
  if (/MSIE/.test(navigator.userAgent) && $('body').hasClass('t1')){
    $('.t1').on('selectstart', function(){
      return false;
    });
  }

  // trigger resize once to fire all the adjustments
  $(window).resize();


  /**********************************************************************************************************
   * T7 SVG Floorplan
   **********************************************************************************************************/
  if ($('body').hasClass('t7')) {
    var gdIE8 = isIE8();
    var gdFloorPlanJson = $('.gd-pagetitle-mapmenu ul li.active a').data("src");
    var gdFloorPlanIE8  = $('.gd-pagetitle-mapmenu ul li.active a').data("src-ie8");
    
    function gdShopInit() {
      if ( gdIE8 ) {
        gdFloorPlanJson = gdFloorPlanIE8;
      }
      
      $('#mapplic').mapplic({
        source: gdFloorPlanJson,
        selector: '#floorplan .mapplic-clickable',
        mapfill: false,
        height: 525,
        animate: true,
        sidebar: false,
        minimap: false,
        locations: true,
        deeplinking: false,
        fullscreen: false,
        hovertip: true,
        developer: /[?&]xy\b/.test(window.location.href),
        maxscale: 1.5,
        clearbutton: true,
        zoom: true,
        zoombuttons: true
      });
    }
    gdShopInit();
    
    function gdShopReset() {
      $('#gd-floorplan-container').unbind();
      $('#mapplic').unbind();
      $('#gd-floors, #gd-shops, #mapplic').empty();
      window.gdFloorData = null;
      $('#gd-shop-detail-title').text('');
      $('#gd-shop-detail-where').text('');
      $('#gd-shop-detail-wkt').text('');
      $('#gd-shop-detail-wdt').text('');
      $('#gd-shop-detail-addr').text('');
      $('#gd-shop-detail-href').attr('href', 'javascript:;');
    }
    

    function gdAddShops(floorNo) {
      var gdShopHtml  = '';
      var gdMenuStatus;
      var gdHighLight;
      for (var looper = 0; looper < window.gdFloorData.levels[floorNo].locations.length; looper++ ) {
        if (looper === 0) {
          gdMenuStatus = 'class ="active"';
          gdHighLight = window.gdFloorData.levels[floorNo].locations[looper].id;
        } else {
          gdMenuStatus = '';
        }
        gdShopHtml += '<a ' + gdMenuStatus + 'data-shopid="' + looper + '" data-location="' + window.gdFloorData.levels[floorNo].locations[looper].id + '" href="javascript:;"><span class="title">' + window.gdFloorData.levels[floorNo].locations[looper].title + '</span><span class="id">' + window.gdFloorData.levels[floorNo].locations[looper].area + '</span></a>';
      }
      if ($(window).width() < 768) {
        // For mobile
        $('#gd-shops').after($('.gd-mainimage-brief'));
      }
      $('#gd-shops').empty().append(gdShopHtml);
      if (gdIE8) {
        $('#mapplic [data-location=' + gdHighLight + ']').trigger('mouseenter');
      } else {
        $('#mapplic [id=' + gdHighLight + ']').trigger('mouseenter');
      }
      gdShowShopDetail();
    }

    function gdShowShopDetail() {
      var gdFloorId  = $('#gd-floors>.active').data('floor');
      var gdShopId   = $('#gd-shops>.active').data('shopid');
      var gdShop     = window.gdFloorData.levels[gdFloorId].locations[gdShopId];
      $('#gd-shop-detail-title').text(gdShop.title);
      $('#gd-shop-detail-where').text(gdShop.wherelocation);
      $('#gd-shop-detail-wkt').text(gdShop.workdayhours);
      $('#gd-shop-detail-wdt').text(gdShop.weekendhours);
      $('#gd-shop-detail-addr').text(gdShop.address);
      $('#gd-shop-detail-href').attr('href', gdShop.href);
      
      // For mobile
      if ($(window).width() < 768) {
        $('#gd-shops>.active').after($('.gd-mainimage-brief'));
      }
    }

    var gdCheckData = function() {
      if (window.gdFloorData) {
        var gdFloorHtml  = '';
        var gdFloorHtmlM = '';
        var gdMenuStatus;
  
        for (var looper = window.gdFloorData.categories.length - 1; looper >= 0; looper-- ) {
          if (looper === 0) {
            gdMenuStatus = 'class ="active"';
          } else {
            gdMenuStatus = '';
          }
          gdFloorHtml  += '<a ' + gdMenuStatus + ' data-floor="' + looper + '" href="javascript:;">' + window.gdFloorData.categories[looper].title + '</a>';
          gdFloorHtmlM = '<option value="' + looper + '">' + window.gdFloorData.categories[looper].title + '</option>' + gdFloorHtmlM;
        }
        $('#gd-floors').append(gdFloorHtml);
        
        // For mobile
        $('#gd-fp-levela-m').append(gdFloorHtmlM);
  
        gdAddShops(0);
      } else {
        setTimeout(function() {
          gdCheckData();
        }, 300);
      }
    };
    gdCheckData();

    function gdFloorPlanBind() {
      $('#gd-floorplan-container').on('click', '#gd-floors>a', function() {
        var gdFloorPos = $(this).data('floor');
        var gdMapplicCtrlPos = $('#gd-floors>a').length - 1 - gdFloorPos;
        $('.mapplic-clear-button').click();
        $('.mapplic-levels-select').val($('.mapplic-levels-select option').eq(gdMapplicCtrlPos).val()).change();
        $(this).addClass('active').siblings().removeClass('active');
        gdAddShops(gdFloorPos);
      });
  
      $('#gd-floorplan-container').on('mouseenter', '#gd-shops', function() {
        if ($(this).height() > $(this).parent().height()) {
          $(this).parent().css({'overflow-x':'hidden', 'overflow-y':'scroll'});
        } else {
          $(this).parent().removeAttr('style');
        }
      })
  
      $('#gd-floorplan-container').on('click', '.gd-fp-levelb a', function() {
        var gdTargetId = $(this).data('location');
        $(this).addClass('active').siblings().removeClass('active');
        if (gdIE8) {
          $('#mapplic [data-location=' + gdTargetId + ']').click();
        } else {
          $('#mapplic [id=' + gdTargetId + ']').click();
        }
        gdShowShopDetail();
      });
  
      $('#gd-floorplan-container').on('click', '.mapplic-tooltip a[href]', function() {
        var gdTargetUrl = $(this).attr('href');
        window.location.href = gdTargetUrl;
      })
  
      $('#gd-floorplan-container').on('click', '#mapplic [id], #mapplic [data-location]', function(event) {
        event.stopPropagation();
        var gdMenuId = $(this).attr('id') || $(this).data('location');
        $('.gd-fp-levelb a[data-location=' + gdMenuId + ']').addClass('active').siblings().removeClass('active');
        gdShowShopDetail();
      })
    }
    gdFloorPlanBind();
    
    
    // Change building
    $('.gd-pagetitle-mapmenu li').click(function() {
      var gdMapLogo     = $(this).parents('.gd-pagetitle-mapmenu').find('img');
      $(this).addClass('active').siblings().removeClass('active');
      gdMapLogo.attr('src', '../files/landmark/images/floorguide/floorguide-icon-' + $(this).index() + '.jpg');
      $('.gd-floorplan-img-m img').attr('src', '../files/landmark/images/floorguide/examplefloorplan' + $(this).index() + '.jpg');
      
      gdShopReset();
      gdFloorPlanJson = $(this).find('a').data('src');
      gdFloorPlanIE8  = $(this).find('a').data('src-ie8')
      gdShopInit();
      gdCheckData();
      gdFloorPlanBind();
    });
    
    // For mobile 
    $('#gd-fp-levela-m').on('change', function() {
      $('#gd-floors>[data-floor=' + $(this).val() + ']').click();
    });
    
    $('#gd-fp-building-m').on('change', function() {
      $('.gd-pagetitle-mapmenu ul>li').eq($(this).val()).click();
    });
  }
  
  /**********************************************************************************************************
   * T22-2 SVG Floorplan
   **********************************************************************************************************/
  if ($('body').hasClass('t22-svg')) {
    var gdFloorPlanT22 = $('#mapplic-t22').data("src");
    
    if (isIE8()) {
      gdFloorPlanT22 = $('#mapplic-t22').data("src-ie8");
    }
    
    $('#mapplic-t22').mapplic({
      source: gdFloorPlanT22,
      mapfill: false,
      height: 300,
      animate: false,
      sidebar: false,
      minimap: false,
      locations: true,
      deeplinking: false,
      fullscreen: false,
      hovertip: false,
      developer: false,
      maxscale: 1,
      clearbutton: false,
      zoom: false,
      zoombuttons: false
    });
    
    function updateInfiniteLoad(gdAreaId) {
      $('#gd-art-gallery').empty();
      var nextpageurl = $('#gd-art-gallery + .navigation a').attr('href').replace(/\?building=[^&]*/, '?building=' + gdAreaId);
      $('#gd-art-gallery + .navigation a').attr('href', nextpageurl);
      $("#gd-art-gallery").infinitescroll('updatePath', nextpageurl);
      $("#gd-art-gallery").infinitescroll('retrieve');
    }
    $('#gdfloorlist .list-group-item').click(function() {
      var gdAreaId = $(this).data('location');
      $('#mapplic-t22 [data-location]').attr('fill', '#6A6B6B');
      $('#mapplic-t22 [data-location=' + gdAreaId + ']').attr('fill', '#CEA562');
      $(this).addClass('active').siblings().removeClass('active');
      $('.gd-artlist > h3').text($.trim($('#gdfloorlist [data-location=' + gdAreaId + ']').text().slice(1)));
      updateInfiniteLoad(gdAreaId);
    });
    
    $('body').on('click', '#mapplic-t22 [data-location]', function() {
      var gdAreaId = $(this).data('location');
      if (!isIE8()) {
        $('#mapplic-t22 [data-location]').attr('fill', '#6A6B6B');
        $(this).attr('fill', '#CEA562');
      }
      $('#gdfloorlist [data-location=' + gdAreaId + ']').addClass('active').siblings().removeClass('active');
      $('#gd-list-locations .gd-controls-m select').val(gdAreaId);
      $('.gd-artlist > h3').text($.trim($('#gdfloorlist [data-location=' + gdAreaId + ']').text().slice(1)));
      updateInfiniteLoad(gdAreaId);
    });
    
    $('body').on('click', '#mapplic-t22 text', function() {
      var gdTextValue = $(this).text();
      if ($.isNumeric(gdTextValue)) {
        $('#gd-list-locations > ul > li[data-location=gdfpa' + gdTextValue + ']').click();
      }
    });
    
    $('#gd-list-locations .gd-controls-m select').change(function() {
      var gdValue = $(this).val();
      $('#gd-list-locations > ul > li[data-location=' + gdValue + ']').click();
    });
  }
  
  /**********************************************************************************************************
   * T22 T22-2 Infinite scroll
   **********************************************************************************************************/
  if ($('body').hasClass('t22-is')){
    // for t22-is
    function rearrangeItems(page){
      var i, j,
          items = [], 
          items_L = $('#gd-art-gallery > .col-sm-6').eq(page*2).children(), 
          items_R = $('#gd-art-gallery > .col-sm-6').eq(page*2 + 1).children(),
          col_L = $('#gd-art-gallery > .col-sm-6:eq(0)'),
          col_R = $('#gd-art-gallery > .col-sm-6:eq(1)');
      // generate item array
      for(i = 0; i < items_L.length; i++){
        items.push(items_L[i]);
        if (i < items_R.length) items.push(items_R[i]);
      }
      
      if (page == 0) {
        // if sorting the first page, remove items on page
        $('#gd-art-gallery > .col-sm-6').empty();
      } else {
        // if loading next page, remove loaded columns
        $('#gd-art-gallery > .col-sm-6:gt(1)').remove();
      }

      if (isPhone()){
        col_L.append(items);
      } else {
        for(i = 0; i < items.length; i++){
          // append to the shorter column
          if(col_L.height()<=col_R.height()){
            col_L.append(items[i])
          } else {
            col_R.append(items[i])
          }
        }
      }
      $('#gd-art-gallery > .col-sm-6:gt(1)').remove();

      $(window).lazyLoadXT();
    }
    
    rearrangeItems(0);
  }

  if ($('body').hasClass('t22')) {
    $('#gd-art-gallery').infinitescroll({
      navSelector  : "div.gd-promo-more",
      nextSelector : "div.gd-promo-more a:last",
      itemSelector : "#gd-art-gallery>*",
      loadingText  : "Loading ...",
      animate      : false
    },function(){
      $(window).unbind('.infscr');
      $(window).lazyLoadXT();
      if ($('body').hasClass('t22-is')){
        rearrangeItems(1);
      }
    });
    $(window).unbind('.infscr');

    $('.gd-artlist-more').live('click', function(){
      var $list = $(this).prev('.row');
      if ($(this).data('isLess')){
        $list.html($(this).data('lesscontent'));
      } else {
        $(this).data('lesscontent', $list.html());
        $list.load($(this).attr('href'), function(){
          $(window).lazyLoadXT();
        });
      }

      $(this).data('isLess', !$(this).data('isLess'))

      var toggletext = $(this).data('toggletext');
      var orgtext = $(this).html();
      $(this).html($(this).data('toggletext'));
      $(this).data('toggletext', orgtext);

      return false;
    })

    // disable scroll event
    $(window).unbind('.infscr');

    // manually trigger when clicking "SEE MORE +"
    $(".navigation a").on('click', function(e){
      $("#gd-art-gallery").infinitescroll('retrieve');
      return false;
    });
  }


  /**********************************************************************************************************
   * T32 Infinite scroll
   **********************************************************************************************************/
  if ($('body').hasClass('t32')) {
    $('.gd-search-body>ol').infinitescroll({
      navSelector  : "div.navigation",
      nextSelector : "div.navigation a:first",
      itemSelector : ".gd-search-body>ol>*",
      loadingText  : "Loading ...",
      animate      : false,
      extraScrollPx: 50,
      donetext     : "No more results." ,
      bufferPx     : 40,
      errorCallback: function(){},
      localMode    : true
      },function(arrayOfNewElems){
        $(window).lazyLoadXT();
    });
  }


  /**********************************************************************************************************
   * GD Lightbox
   **********************************************************************************************************/
  $('.gd-lightbox-link.gd-lightbox-video').append('<div class="mejs-overlay-button"></div>');
  $('body').on('click', '.gd-lightbox-link', function(event) {
    event.preventDefault();
    var $link = $(this);
    var gdLightMax     = $(window).height() - 150;
    var gdLightSrc     = $(this).data('lightsrc');
    var gdLightTitle   = $(this).data('lighttitle') || 'Lightbox';
    var gdLightPoster  = $(this).data('lightposter')
    var gdLightContent = '';

    // update the logic to support video popup & floor plan with pin
    if ($(this).hasClass('gd-lightbox-map')) {
        gdLightContent = '<div class="gd-location-map" data-lat="' + $(this).data('lat') + '" data-lng="' + $(this).data('lng') + '"><iframe src="/files/landmark/iframes/map-simple-iframe.html" frameborder="0"></iframe></div>';
    } else if ($(this).hasClass('gd-lightbox-video')){
      if(isiOS() || isAndroid()){
        scTrackVideo(gdLightSrc);
        window.open(gdLightSrc);
        return false;
      } else{
        gdLightContent = '<video class="hide" src="'+gdLightSrc+'" poster="'+gdLightPoster+'" type="video/mp4" width="100%" height="100%" controls="controls" preload="none"></video>';
      }
    } else if ($(this).hasClass('gd-lightbox-pin')) {
      gdLightContent = '<img class="gd-lightbox-img" src="'+gdLightSrc+'" /><div class="pin icomoon-location2"></div>';
    } else {
      gdLightContent = '<img class="gd-lightbox-img" src="'+gdLightSrc+'" />';
    }

    var gdLightboxHtml = '<div class="modal fade gd-lightbox" id="gd-lightbox">' +
                '<div class="modal-dialog">' +
                  '<div class="modal-content">' +
                    '<div class="modal-header">' +
                      '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span class="icomoon-thin-cross2" aria-hidden="true"></span></button>' +
                      '<h4 class="modal-title">' + gdLightTitle + '</h4>' +
                    '</div>' +
                    '<div class="modal-body" style="height: ' + gdLightMax + 'px">' +
                      gdLightContent +
                    '</div>' +
                  '</div>' +
                '</div>' +
              '</div>';
    $('body').append(gdLightboxHtml);

    var $modal = $('#gd-lightbox');
    $modal.modal({
      show: true
    })
    .on('hidden.bs.modal', function(){
      if ($modal.data('player')){
        $modal.data('player').pause();
      }
      $('#gd-lightbox').remove();
    })
    .on('shown.bs.modal', function(){
      // handle pin location
      if ($link.hasClass('gd-lightbox-pin')){      
        var parts = $link.data('pin').split(','),
            pinX = parseFloat(parts[0]),
            pinY = parseFloat(parts[1]),
            $pin = $('#gd-lightbox .pin'),
            $img = $('#gd-lightbox .modal-body .gd-lightbox-img');
            
        setTimeout(function(){
          var x = pinX * $img.width(), y = pinY *  $img.height();
          $('#gd-lightbox .pin').css({
            left: ($img.parent().width() - $img.width()) /2 + x - $pin.width() / 2 + 'px',
            top: y - $pin.height() + 'px'
          });
        },200)
      }

      // handle video
      if ($link.hasClass('gd-lightbox-video')) {
        var videoW = $modal.find('.modal-content').width() - 50,
            videoH = Math.min(videoW / 16 * 9, $modal.find('.modal-content').height() - 100);
        $('#gd-lightbox video')
          .removeClass('hide')
          .attr('width', videoW)
          .attr('height', videoH);
        var player = new MediaElementPlayer('#gd-lightbox video',
        {
          iPadUseNativeControls: true,
          iPhoneUseNativeControls: true,
          AndroidUseNativeControls: true,
          plugins: ['flash','silverlight'],
          success: function (mediaElement, domObject) {
            mediaElement.addEventListener('play', function(e) {
              scTrackVideo(gdLightSrc);
            }, false);
            setTimeout(function(){
              mediaElement.play();
            }, 500);
          }
        });
        $modal.data('player', player);
      } 
      // handle custom scrollbar
      if (!$link.hasClass('gd-lightbox-map') && !$link.hasClass('gd-lightbox-video')) {
        $('#gd-lightbox .modal-body').mCustomScrollbar({theme:"dark-2"});
      }
    });


    if(isIE8()||isIE9()){
      setTimeout(function(){
        $modal.trigger('shown.bs.modal')
      }, 200)
    }

  });

  /**********************************************************************************************************
   * Story Detail Video
   **********************************************************************************************************/
  $('.gd-video').each(function(){
    var self = this,
        $self = $(this)
        videoSrc = $(this).attr('src');
    if(isiOS() || isAndroid()){
      $(this).replaceWith('<a href="'+videoSrc+'" target="_blank" onclick="scTrackVideo(\''+videoSrc+'\')"><div class="mejs-overlay-button"></div><img src="'+ $(this).attr('poster') +'"></a>');
    } else {
      $self.attr({
        width: isIE8() ? 980 : $self.width(),
        height: isIE8() ? 552 : $self.height()
      });
      var player = new MediaElementPlayer(self,
      {
        iPadUseNativeControls: true,
        iPhoneUseNativeControls: true,
        AndroidUseNativeControls: true,
        plugins: ['flash','silverlight'],
        success: function (mediaElement, domObject) {
          mediaElement.addEventListener('play', function(e) {
            scTrackVideo(videoSrc);
          }, false);        
        }
      });
    }
  })

  /**********************************************************************************************************
   * Audio Player
   **********************************************************************************************************/
  $('body').on('click', '[data-player]', {}, function() {
    var gdAudioFile;
    var gdAudioFrame;
    if ($('.gd-audio-wrapper').length) {
      $('.gd-audio-frame').remove();
      $('.gd-audio-wrapper').removeClass('gd-audio-wrapper');
      //return false;
    }

    gdAudioFile = $(this).data('player');
    gdAudioFrame= '<div class="gd-audio-frame" data-audiofile="' + gdAudioFile + '"><iframe src="/files/landmark/audioplayer/index.html" frameborder="0"></iframe></div>';
    $(this).parent().addClass('gd-audio-wrapper').append(gdAudioFrame);
  });
  
  /**********************************************************************************************************
   * T35 Google Map
   **********************************************************************************************************/
  if ($('body').hasClass('t35')) {
    $('#gd-map-switcher>a').click(function() {
      var gdMapFile = $(this).data('iframe');
      $('#gd-map-container').attr('src', gdMapFile);
      $(this).addClass('active').siblings().removeClass('active');
    });
    
    $('#gd-map-switcher-m select').change(function() {
      $('#gd-map-container').removeAttr('style');
    });
  }

  /**********************************************************************************************************
   * Showcase
   **********************************************************************************************************/
  if ($('.gd-showcase').length) {
    $.each($('.gd-showcase-content'), function(index, item) {
      var gdScViewWidth = $(item).width();
      var gdScItemWidth;
      if ($(window).width() > 700) {
        gdScItemWidth = gdScViewWidth / 4;
      } else {
        gdScItemWidth = gdScViewWidth;
      }
      $(item).find('.gd-showcase-items>li').css({'width': gdScItemWidth + 'px'});
    });
  
    $('.gd-showcase-control').click(function() {
      var self = this, $self = $(this);
      var gdShowcase      = $(this).parent();
      var gdShowcaseItems = gdShowcase.find('.gd-showcase-items');
      var gdShowcaseNo    = gdShowcase.find('.gd-showcase-items>li').length;
      var gdShowcaseIndex = Number(gdShowcase.data('position')) || 0;
      var gdShowcaseMove;
      var gdShowcaseViewW = gdShowcase.find('.gd-showcase-content').width();
      var gdShowcaseItemW;
      var gdShowcaseStopNo;
      var gdShowcaseNextUrl = gdShowcase.data('nexturl');
      
      if ($(window).width() > 700) {
        gdShowcaseItemW = gdShowcaseViewW / 4;
        gdShowcaseStopNo= gdShowcaseNo - 4;
      } else {
        gdShowcaseItemW = gdShowcaseViewW;
        gdShowcaseStopNo= gdShowcaseNo - 1;
      }
  
      if ($(this).hasClass('gd-showcase-left')) {
          if (gdShowcaseIndex > 0) {
              gdShowcaseIndex -= 1;
          }
      } else {
          if (gdShowcaseIndex < gdShowcaseStopNo) {
              gdShowcaseIndex += 1;
          } else {
            // retrieve by ajax then trigger event again
            $.get(gdShowcaseNextUrl.replace(/([&?])startindex=\d+$/, '$1startindex=' + gdShowcaseNo), function(html){
              var items = $(html);
              items.css('width', gdShowcaseItems.find('>li:first').css('width'));
              gdShowcaseItems.append(items);
              $(window).lazyLoadXT();
              $self.trigger('click');
            });
          }
      }
      
      gdShowcase.data('position', gdShowcaseIndex);
      gdShowcaseMove = -gdShowcaseIndex * gdShowcaseItemW;
      gdShowcaseItems.css({'left': gdShowcaseMove});

      gdShowcase.find('.gd-hover-img').click();
      setTimeout(function() {
        $('body').trigger('scroll');
      }, 200);
    });
    
    $('.gd-showcase').swipe( {
      excludedElements: "button, input, select, textarea, .noSwipe",
      allowPageScroll:'auto',
      swipe:function(event, direction, distance, duration, fingerCount, fingerData) {
        if (direction === 'right') {
          $(event.target).closest('.gd-showcase').find('.gd-showcase-left').click();
        } else if (direction === 'left') {
          $(event.target).closest('.gd-showcase').find('.gd-showcase-right').click();
        }
      }
    });
  }


  /**********************************************************************************************************
   * Brand Switcher
   **********************************************************************************************************/
  $('.shopdetails-brand .shopdetails-brand-header a').click(function() {
    var gdShopBrandActived = $(this).parent().index();
    $(this).addClass('active').parent().siblings().find('a').removeClass('active');
    $(this).parents('.shopdetails-brand')
      .find('.shopdetails-brand-body>.row').addClass('hidden')
      .eq(gdShopBrandActived).removeClass('hidden');
    $('body').trigger('scroll');
  });

  /**********************************************************************************************************
   * Long Article & Long Article with fixed lines
   **********************************************************************************************************/
  var gdTextOn   = $('.gd-longarticle').eq(0).data('text-on');
  var gdTextOff  = $('.gd-longarticle').eq(0).data('text-off');
  
  var gdTextLines = (function() {
    if ($('.gd-longarticle p').length > 1) {
      var gdTextSample = $('.gd-longarticle p').eq(0);
      var gdTextLh     = gdTextSample.css('lineHeight').indexOf('px') > 0 ? (gdTextSample.css('lineHeight').slice(0, -2) / gdTextSample.css('fontSize').slice(0, -2)) : Number(gdTextSample.css('lineHeight'));
      return gdTextLh * gdSettings.LongVersionContentNumberOfLines;
    } else {
      return gdSettings.LongVersionContentNumberOfLines;
    }
  })();

  $('.gd-longarticle').trunk8({
    lines: gdTextLines,
    fill: '<a id="read-more" href="#">' + gdTextOn + '</a>',
    tooltip: false
  });
  
  var gdTextFunc = function() {
    var gdTextPar = $('#read-more').parent();
    var gdTextOri = gdTextPar.html().toString();
    var gdTextBrk = gdTextOri.indexOf('<a id="read-more');
    var gdTextBef = gdTextOri.slice(0, gdTextOri.lastIndexOf(' ', gdTextBrk));
    var gdTextAft = gdTextOri.slice(gdTextBrk);
    gdTextPar.html(gdTextBef + gdTextAft);
  };
  
  var gdTrunkCheckTimer;
  function gdTrunkCheck() {
    if ($('#read-more').length > 0) {
      clearTimeout(gdTrunkCheckTimer);
      gdTextFunc();
    } else {
      gdTrunkCheckTimer = setTimeout(function() {
        gdTrunkCheck();
      }, 200);
    }
  };
  gdTrunkCheck();
  
  $('#read-more').live('click', function (event) {
    $(this).closest('.gd-longarticle').trunk8('revert').append(' <a id="read-less" href="javascript:;">' + gdTextOff + '</a>');
    
    return false;
  });
  
  $('#read-less').live('click', function (event) {
    $(this).closest('.gd-longarticle').trunk8();
    
    return false;
  });
  
  /**********************************************************************************************************
   * Text in bottom slider
   **********************************************************************************************************/
  var gdBottomSliderFunc = function(textHeight) {
    $('#gd-carousel-info .gd-carousel-detail p').dotdotdot({
      ellipsis	: '... ',
      wrap		: 'word',
      fallbackToLetter: true,
      after		: null,
      watch		: true,
      tolerance	: 0,
      height: textHeight,
      lastCharacter	: {
        remove		: [ ' ', ',', ';', '.', '!', '?' ],
        noEllipsis	: []
      }
    });
  }
  if (!$('body').hasClass('t34') && $('#gd-carousel-info .gd-carousel-detail>p').length) {
    var gdBottomText       = $('#gd-carousel-info .gd-carousel-detail>p').eq(0);
    var gdBottomTextFZ     = gdBottomText.css('fontSize');
        gdBottomTextFZ     = gdBottomTextFZ.indexOf('px') > 0 ? gdBottomTextFZ.slice(0, -2) : gdBottomTextFZ;
    var gdBottomTextLH     = gdBottomText.css('lineHeight');
        gdBottomTextLH     = gdBottomTextLH.indexOf('px') > 0 ? gdBottomTextLH.slice(0, -2) : (gdBottomTextLH * gdBottomTextFZ);
    var gdBottomTextHeight = gdBottomTextLH * gdSettings.ShortVersionContentNumberOfLines;

    gdBottomSliderFunc( gdBottomTextHeight );
  }
  
  /**********************************************************************************************************
   * Text in Magazine
   **********************************************************************************************************/
  $('.gd-longarticle-fixedlines p').dotdotdot({
    ellipsis	: '... ',
    wrap		: 'word',
    fallbackToLetter: true,
    after		: null,
    watch		: true,
    tolerance	: 0,
    height: (isIE8() ? 28 : parseInt($('.gd-longarticle-fixedlines p').css('lineHeight'))) * gdSettings.LongVersionContentNumberOfLines,
    lastCharacter	: {
      remove		: [ ' ', ',', ';', '.', '!', '?' ],
      noEllipsis	: []
    }
  });

  /**********************************************************************************************************
   * Carousel image
   **********************************************************************************************************/
  if ($('.gd-carousel-info').length) {
    if ( isIE8() ) {
      // for bottom slider, use normal background iamge
      $('.gd-carousel-info:not(.gd-mainimage) .carousel-image').each(function() {
        $(this).addClass('fireonce').css({'background': 'url(' + $(this).data('bgsrc') + ') center 0 no-repeat', 'background-size': 'cover !important' });
      });
      
      // Information of emulating target's wrapper
      var gdCoverWrapper  = $('[data-cover-height]').eq(0);
      var gdCoverWrapperParam    = {
        'height': gdCoverWrapper.data('cover-height'),
        'width':  gdCoverWrapper.width(),
        'ratio':  gdCoverWrapper.width() / gdCoverWrapper.height()
      }

      // for main slider, emulate background : cover
      $('.gd-mainimage .carousel-image').each(function() {
        $(this).addClass('carousel-image-ie8')
                .append('<img src="' + $(this).data('bgsrc') + 
                '" style="min-width:' + gdCoverWrapperParam.width + 'px; min-height:' + gdCoverWrapperParam.height + 'px;" />');
      });
      
      // Function for image size & position adjustment
      function gdCoverImgae(coverImage) {
        var gdCoverImg  = $(coverImage);
        var gdCoverImgW = gdCoverImg.width();
        var gdCoverImgH = gdCoverImg.height();

        if (gdCoverImgH === gdCoverWrapperParam.height) {
          gdCoverImg.css({
            'left': (gdCoverWrapperParam.width - gdCoverImg.width()) / 2
          });
        }
        
        if (!(gdCoverImgW === gdCoverWrapperParam.width || gdCoverImgH === gdCoverWrapperParam.height)) {
          if (gdCoverImgW / gdCoverImgH >= gdCoverWrapperParam.ratio) {
            gdCoverImg.height(gdCoverWrapperParam.height);
            gdCoverImg.css({
              'left': (gdCoverWrapperParam.width - gdCoverImg.width()) / 2
            });
          } else {
            gdCoverImg.width(gdCoverWrapperParam.width);
          }
        }
      }
      
      // Add image loaded marker for every slider
      $.each($('.item', gdCoverWrapper), function(index, item) {
        gdCoverWrapper.eq(index).imagesLoaded(function() {
          $(item).data('cover-loaded', true);
          // Adjust after first slider loaded
          if (index === 0) {
            gdCoverImgae($('img', $(item)));
            $(item).data('cover-adjusted', true);
          }
        });
      });
      
      // Adjust after slider actived
      gdCoverWrapper.on('slid.bs.carousel', function(event) {
        if (!$(event.relatedTarget).data('cover-adjusted') && $(event.relatedTarget).data('cover-loaded')) {
          gdCoverImgae($('img', $(event.relatedTarget)));
          $(event.relatedTarget).data('cover-adjusted', true);
        }
      });
    } else {
      $('.gd-carousel-info .carousel-image').each(function() {
        $(this).addClass('fireonce').css({'background': 'url(' + $(this).data('bgsrc') + ') center 0 no-repeat', 'background-size': 'cover !important' });
      });
    }
    
    $('.gd-carousel-info').on('slide.bs.carousel', function(event) {
      $(this).find('.item.active .fireonce').removeClass('fireonce');
    });
    
    $(".gd-carousel-info").swipe( {
      allowPageScroll:"auto",
      swipe:function(event, direction, distance, duration, fingerCount, fingerData) {
        if (direction === 'right') {
          $(event.target).closest('.gd-carousel-info').carousel('prev');
        } else if (direction === 'left') {
          $(event.target).closest('.gd-carousel-info').carousel('next');
        }
      }
    });
    
  }

  /**********************************************************************************************************
   * Back To Top Button
   **********************************************************************************************************/
  var gdBackTopPosX = 20;

  $(window).on('resize', function() {
    if ($(window).width() > 1920) {
      gdBackTopPosX = ($(window).width() - 1920) / 2 + 20;
    } else {
      gdBackTopPosX = 20;
    }
  });
  $(window).trigger('resize');

  $(window).on('scroll', function() {
    if ($('body').height() > $(window).height() && $(window).scrollTop() / $(window).height() > 0.25) {
      if (!isIE8() && $(window).scrollTop() + $(window).height() > $('body').height() - $('.site-footer').height()) {
        $backToTopBtn.removeAttr('style').css({'display': 'block'}).addClass('bottom');
      } else {
        $backToTopBtn.removeClass('bottom').css({'right': gdBackTopPosX});
      }

      if (!$backToTopBtn.is(':visible') && !$backToTopBtn.data('animating')) {
        $backToTopBtn.data('animating', true).fadeIn(500, function() {
          $(this).data('animating', false);
        });
      }
    } else {
      if ($backToTopBtn.is(':visible') && !$backToTopBtn.data('animating')) {
        $backToTopBtn.data('animating', true).blur().fadeOut(500, function() {
          $(this).data('animating', false);
        });
      }
    }
  });
  
  /**********************************************************************************************************
   * GD Pagetitle for mobile
   **********************************************************************************************************/
  $('.gd-pagetitle>.gd-controls-m select').change(function() {
    if ($('body').hasClass('t35')) {
      $('#gd-map-container').attr('src', $(this).val());
    } else {
      window.location.href = $(this).val();
    }
  });

  /**********************************************************************************************************
   * Image mask
   **********************************************************************************************************/
   if ($('body').hasClass('t29') || $('body').hasClass('t30')) {
    $('body').on('mouseenter', '.gd-hover-img', function() {
      var gdImgWidth = $(this).find('img').width();
      var gdImgMask  = $(this).find('.gd-hover-img-mask');
      gdImgMask.css({'width': gdImgWidth, 'left': ($(this).width() - gdImgWidth) / 2});
    });
   }


  /**********************************************************************************************************
   * T9
   **********************************************************************************************************/
  if ($('body').hasClass('t9')) {
    $('.gd-contact-heading').click(function() {
      var gdHeadIcon = $(this).find('span[class^=icomoon-thin-arrow]');
      if (gdHeadIcon.hasClass('icomoon-thin-arrow-D')) {
        gdHeadIcon.removeClass().addClass('icomoon-thin-arrow-R');
      } else {
        gdHeadIcon.removeClass().addClass('icomoon-thin-arrow-D');
      }

      $(this).next().animate({height: ['toggle']}, 100);
    });
    

    var gdValidator = new FormValidator('gd-contact-form', [{
        name: 'Title',
        display: 'required',
        rules: 'required'
    }, {
        name: 'FirstName',
        rules: 'required|max_length[50]'
    }, {
        name: 'LastName',
        rules: 'required|max_length[50]'
    }, {
        name: 'Telephone',
        rules: 'numeric|max_length[20]'
    }, {
        name: 'Email',
        rules: 'required|valid_email|max_length[254]'
    }, {
        name: 'EnquiryType',
        rules: 'required'
    }, {
        name: 'Message',
        rules: 'required'
    }, {
        name: 'ValidateCode',
        rules: 'required|callback_check_captcha'
    }], function(errors, event) {
        if (errors.length > 0) {
            // Show the errors
            $('.gd-contact-form .gd-form-error').remove();
            var gdErrorMsg = '<ol class="gd-form-error">';
            for (var looper = 0; looper < errors.length; looper++) {
              gdErrorMsg += '<li>' + $('[name=' + errors[looper].name + ']').data('error') + '</li>';
            }
            gdErrorMsg += '</ol>';
            $('.gd-contact-form').append(gdErrorMsg);
        }
    });

    gdValidator.registerCallback('check_captcha', function(value) {
      var isMatch = false;
      $.ajax({
        async: false,
        url: $('form[name=gd-contact-form]').data('captchaUrl'),
        data: {captcha: value},
        success: function(result){
          isMatch = result;
        },
        dataType: 'json'
      });

      return isMatch;
    });
  }
  
  
  /**********************************************************************************************************
   * T31
   **********************************************************************************************************/
  if ($('body').hasClass('t31')) {
    var gdEPselected = false;
    var gdIsHK       = false;
    var gdValidator;
    
    gdValidator = new FormValidator('gd-contact-form', [{
        name: 'title',
        rules: 'required'
    }, {
        name: 'firstname',
        rules: 'required|max_length[50]'
    }, {
        name: 'lastname',
        rules: 'required|max_length[50]'
    }, {
        name: 'email',
        rules: 'required|valid_email|max_length[254]'
    }, {
        name: 'room',
        rules: 'required|max_length[50]',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'building',
        rules: 'required|max_length[50]',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'street',
        rules: 'required|max_length[50]',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'area',
        rules: 'max_length[50]',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'district',
        rules: 'required',
        depends: function() {
          return gdIsHK && gdEPselected;
        }
    }, {
        name: 'city',
        rules: 'required|max_length[50]',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'state',
        rules: 'max_length[50]',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'postcode',
        rules: 'max_length[10]'
    }, {
        name: 'country',
        rules: 'required',
        depends: function() {
          return gdEPselected;
        }
    }, {
        name: 'interests',
        rules: 'required',
        depends: function() {
          return $('[name=interests]').val() === '';
        }
    }, {
        name: 'others',
        rules: 'required',
        depends: function() {
          return $(this).prev('.gd-checkbox').hasClass('active');
        }
    }, {
        name: 'legal',
        rules: 'required'
    }, {
        name: 'optin',
        rules: 'required'
    }, {
        name: 'verifycode',
        rules: 'required|callback_check_captcha'
    }], function(errors, event) {
        if (errors.length > 0) {
          // Show the errors
          $('.gd-contact-form .gd-form-error').remove();
          var gdErrorMsg = '<ol class="gd-form-error">';
          for (var looper = 0; looper < errors.length; looper++) {
            gdErrorMsg += '<li>' + $('[name=' + errors[looper].name + ']').data('error') + '</li>';
          }
          gdErrorMsg += '</ol>';
          $('.gd-contact-form').append(gdErrorMsg);
        }
    });

    gdValidator.registerCallback('check_captcha', function(value) {
      var isMatch = false;
      $.ajax({
        async: false,
        url: $('form[name=gd-contact-form]').data('captcha-url'),
        data: {captcha: value},
        success: function(result){
          isMatch = result;
        },
        dataType: 'json'
      });

      return isMatch;
    });
    
    
    $('#gd-preferred-comm .dropdown-menu>li>a').click(function() {
      $('#gd-subforms>.tab-content>.tab-pane').eq($(this).data('value')).addClass('active in').siblings().removeClass('active in');
      if ($(this).data('value') == '1') {
        gdEPselected = true;
      } else {
        gdEPselected = false;
      }
      
      $('.gd-contact-form .gd-form-error').remove();
    });
    
    $('#gd-country .dropdown-menu>li>a').click(function() {
      if ($(this).data('code') && $(this).data('code').toLowerCase() === 'hk') {
        gdIsHK = true;
        $('[name=district]').closest('.form-group').removeClass('hidden');
      } else {
        gdIsHK = false;
        $('[name=district]').closest('.form-group').addClass('hidden');
      }
    });
    
    $('#gd-others').click(function(event) {
      if ($(event.target).hasClass('form-control')) {
        return false;
      } else {
        var gdOtherCtl = $(this).find('input.form-control');
        if (gdOtherCtl.attr('disabled') !== 'disabled') {
          gdOtherCtl.attr('disabled', 'disabled').val('');
        } else {
          gdOtherCtl.removeAttr('disabled');
        }
      }
    });

    $('[name=others]').change(function(){
      var val = $(this).val();
      $(this).prev('.gd-checkbox').data('value', val).trigger('update');
    })
  }
  
  
  /**********************************************************************************************************
   * T3 & T15 & T26
   **********************************************************************************************************/
  if ($('body').hasClass('t3') || $('body').hasClass('t15') || $('body').hasClass('t26')) {
    var gdAllPromos = $('.gd-promo-body').eq(0);
    var gdFilter;
    var gdFilt = function(param) {
      if (param !== '*') {
        param =  '.' + param;
      }
      gdAllPromos.isotope({filter: param});
    }
    var gdUpdateFilters = function(){
      $('.gd-promo-filter a[data-filter]').each(function(){
        var filter = $(this).data('filter');
        if (filter !== '*') {
          filter =  '.' + filter;
        }
        var myItems = $(gdAllPromos).find(filter);
        $(this).toggleClass('disabled', myItems.length == 0);
      });
    }
    gdUpdateFilters();

    gdAllPromos.find('img[data-src]').each(function(){
      $(this).on('lazyload', function() {
        if (gdAllPromos.data('isotope')){
          gdAllPromos.isotope('layout');
        } else {        
          gdAllPromos.isotope({
            itemSelector: '.gd-promo-body>*',
            masonry: {
              columnWidth: '.gd-promo-body>[class*=gd-promo-box]'
            }
          });
        }
      });
    });
    
    gdAllPromos.infinitescroll({
      navSelector  : "div.navigation",
      nextSelector : "div.navigation a:first",
      itemSelector : ".gd-promo-body>*",
      loadingText  : "Loading ...",
      animate      : false
      
    },function(items){
      gdAllPromos.append(items).isotope( 'appended', items );
      gdUpdateFilters();

      gdAllPromos.imagesLoaded(function() {
        gdAllPromos.isotope('layout');
      });
      
      $(window).unbind('.infscr');
    });
    $(window).unbind('.infscr');
    
    // manually trigger when clicking "SEE MORE +"
    $(".navigation a").on('click', function(e){
      $(".gd-promo-body").infinitescroll('retrieve');
      return false;
    });
    
    $('body').on('click', function(event) {
      if ($(event.target).parents('.gd-promo-brands').length < 1) {
        $('.gd-promo-brands').removeClass('active');
      }
    });
    
    $('.gd-promo-filter a[data-filter]').click(function(event) {
      event.preventDefault();
      if (!$(this).hasClass('disabled')) {
        $(this).parent().addClass('active').siblings().removeClass('active');
        $(this).closest('.gd-promo-brands').removeClass('active');
        gdFilter = $(this).data('filter');
        gdFilt(gdFilter);
      }
    });
    
    $('.gd-promo-filter select').change(function() {
      gdFilter = $(this).val();
      gdFilt(gdFilter);
    });
  }
  
  
  /**********************************************************************************************************
   * T26
   **********************************************************************************************************/
  if ($('body').hasClass('t26')) {
    $('.gd-promo-brands-btn').click(function() {
      $(this).parent().toggleClass('active');
    });
  }
  


  /**********************************************************************************************************
   * T37
   **********************************************************************************************************/
  if ($('body').hasClass('t37')) {
    var gdPromoBox = $('.gd-promotion-box-area').eq(0);

    gdPromoBox.infinitescroll({
      navSelector  : "div.gd-promo-more",
      nextSelector : "div.gd-promo-more a:first",
      itemSelector : ".gd-promotion-box-area>*",
      loadingText  : "Loading ...",
      animate      : false
    },function(){
      $(window).unbind('.infscr');
      $(window).lazyLoadXT();
    });
    $(window).unbind('.infscr');
    
    // manually trigger when clicking "SEE MORE +"
    $(".gd-promo-more a").on('click', function(e){
      $(".gd-promotion-box-area").infinitescroll('retrieve');
      return false;
    });
  }


  /**********************************************************************************************************
   * Style modification operation on clients request
   **********************************************************************************************************/
  // t4
  if ($('body').hasClass('t4')) {
    if ($(window).width() < 767) {
      $('.gd-nd-details-area>.gd-nd-details').after($('.gd-nd-details-area>.col-sm-4'));
    }
  }
  // t22-2
  if ($('body').hasClass('t22-svg') && (isIE8() || isIE9())) {
    var gdFloorWrap  = $('#gd-list-locations');
    var gdFloorItems = $('#gdfloorlist').detach();
    var gdFloorDivid = Math.ceil(gdFloorItems.find('li').length / 2);
    var looper;
    for (looper = gdFloorDivid; looper < gdFloorItems.find('li').length; looper++) {
      gdFloorItems.find('li').eq((looper - gdFloorDivid) * 2).after(gdFloorItems.find('li').eq(looper));
    }
    gdFloorItems.css({'overflow': 'hidden'});
    gdFloorItems.find('li').css({'width':'50%', 'float':'left'});
    gdFloorWrap.append(gdFloorItems);
  }
  


  /**********************************************************************************************************
   * GD Dropdown control
   **********************************************************************************************************/
  $('body').on('click', '.gd-dropdown>.dropdown-menu>li>a', function() {
    var gdDropdownCtrl = $(this).closest('.gd-dropdown');
    var gdCurrentVal   = gdDropdownCtrl.find('.current');
    var gdUserChosen   = gdDropdownCtrl.find('input[type=hidden]');
    var gdUserChoice   = $(this).data('value');
    gdCurrentVal.text($(this).text());
    gdUserChosen.val(gdUserChoice);
    gdDropdownCtrl.removeClass('open');
    if (gdCurrentVal.text().toLowerCase() !== 'please select') {
      gdCurrentVal.css({'color': '#222222'});
    } else {
      gdCurrentVal.css({'color': '#999999'});
    }
    return false;
  });


  /**********************************************************************************************************
   * GD Checkbox control
   **********************************************************************************************************/
  $('body').on('click update', '.gd-checkbox', function(e) {
    var gdCheckbox   = $(this).closest('.gd-checkbox-wrapper');
    var gdUserChosen = gdCheckbox.find('input[type=hidden]');
    var gdChosen    = [];
    if (e.type == 'click'){
      if (gdCheckbox.data('type') === 'radio') {
        $(this).addClass('active').siblings().removeClass('active');
      } else {
        $(this).toggleClass('active');
      }
    }
    $(this).blur();
    gdCheckbox.find('.active').each(function() {
      gdChosen.push($(this).data('value'));
    });
    gdUserChosen.val(gdChosen.join(','));
  });


  /**********************************************************************************************************
   * slick slider
   **********************************************************************************************************/
  $('.gd-slick-slider').on('setPosition', function(slick) {
    var gdSlickControlsM = $(this).closest('.gd-slick-slider-wrapper').find('.gd-slick-controls-m');
    gdSlickControlsM.find('.gd-slick-no-all').text($(this).find('.gd-slick-item').not('.slick-cloned').length);
    gdSlickControlsM.find('.gd-slick-no-current').text(Number($(this).find('.slick-current').data('slick-index')) + 1);
  });
  
  $('.gd-slick-controls-m span').click(function() {
    var gdSlickSlider = $(this).closest('.gd-slick-slider-wrapper').find('.gd-slick-slider');
    if ($(this).hasClass('icomoon-thin-arrow-L')) {
      gdSlickSlider.slick('prev');
    } else if ($(this).hasClass('icomoon-thin-arrow-R')) {
      gdSlickSlider.slick('next');
    }
  });
   
  $('.gd-slick-slider').slick({
    dots: false,
    infinite: false,
    speed: 300,
    slidesToShow: 1,
    slidesToScroll: 1,
    variableWidth: true,
    prevArrow: '<button type="button" class="slick-prev" tabindex="0" role="button"><span class="icomoon-thin-arrow-L"></span></button>',
    nextArrow: '<button type="button" class="slick-next" tabindex="0" role="button"><span class="icomoon-thin-arrow-R"></span></button>',
    
    responsive: [
      {
        breakpoint: 480,
        settings: {
          centerMode: true
        }
      }
    ]
  });


  /**********************************************************************************************************
   * GD Promotion Tabcontrol
   **********************************************************************************************************/
  $('.gd-tab-intro').on('click', '.tab-pane-switch', function() {
    var gdCurrentPane    = $(this).next();
    if (gdCurrentPane.hasClass('active')) {
      $(this).find('span').attr('class', 'off');
    } else {
      $(this).find('span').attr('class', 'on');
    }
    gdCurrentPane.toggleClass('in active');
    $('body').trigger('scroll');
  });
  
  $('.gd-tab-intro').on('shown.bs.tab',  function() {
    $('body').trigger('scroll');
  });



});
});





















