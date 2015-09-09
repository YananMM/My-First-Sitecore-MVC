jQuery.noConflict();;
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
    function handleMegaMenu(){  // wait for IE8 ready
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
          .off('click', '> a').on('click', '> a', function(e){  // for touch devices
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
    socialShareDescription = $('head meta[property="og:description"]').attr('content') || $('head meta[name="Description"]').attr('content') || "";

  // FB Share
  Modernizr.load({
    load: '//connect.facebook.net/en_US/sdk.js',
    callback: function(){
      window.fbAsyncInit = function() {
            FB.init({
              appId      : window.fbAppId,
              xfbml      : true,
              version    : 'v2.4'
            });
         };

      $('.header-social .icomoon-facebook').click(function(e){
        e.preventDefault();
        FB.ui(
        {
          method: 'share',
          href: socialShareUrl
        });
      });
    }
  });

  // Twitter Share
  Modernizr.load({
    load: 'https://platform.twitter.com/widgets.js',
    callback: function(){
      $('.header-social .icomoon-twitter').each(function(){  
        $(this).attr('href', 'https://twitter.com/intent/tweet?url='+ encodeURIComponent(socialShareUrl));
      });
    }
  });

  // Weibo Share
  Modernizr.load({
    load: 'http://tjs.sjs.sinajs.cn/open/api/js/wb.js?appkey=' + window.wbAppKey,
    callback: function(){
      WB2.anyWhere(function(W){
        var $btn = isDesktop()
          ? $('.header-social .icomoon-sina-weibo')
          : $('.header-social .icomoon-sina-weibo');

        $btn.attr('href', 'http://service.weibo.com/share/share.php?url='+encodeURIComponent(socialShareUrl)+'&title='+socialShareDescription)
          .attr('id', 'wb_publish');
        
        W.widget.publish({
          id : "wb_publish",
          default_text : socialShareDescription + '\r\n' + socialShareUrl,
          callback : function(data) {
            // do something...
          }
        });
      });
    }
  });

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
        developer: false,
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
    var gdFloorPlanT22 = 't22-2/floorplan.json';
    
    if (isIE8()) {
      gdFloorPlanT22 = 't22-2/floorplan-ie8.json';
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
  /*if ($('body').hasClass('t22')) {
    // Get height and width of browser's visible area
    var winSize = function() {
    var e = window,
        a = 'inner';
    if (!('innerWidth' in window )){
      a = 'client';
      e = document.documentElement || document.body;
    }
      return { width : e[ a+'Width' ] , height : e[ a+'Height' ] };
    };
    
    // Check columns' height, incase there's a blank between columns
    var gdCheckColumns = function(parentIdentifier, sonIdentifier) {
      var gdColHigh   = 0;
      var gdColLow    = 0;
      var gdWinHeight = winSize().height;
      var gdItemHeight;
      
      if (winSize().width >= 768) {
        $.each($(parentIdentifier + '>' + sonIdentifier), function(index, item) {
          gdItemHeight = $(item).height();
          if (index === 0) {
            gdColHigh = gdItemHeight;
            gdColLow  = gdItemHeight;
          } else {
            if (gdItemHeight > gdColHigh) {
              gdColHigh = gdItemHeight;
            } else if (gdItemHeight < gdColLow) {
              gdColLow  = gdItemHeight;
            }
          }
        });
        if ((gdColHigh - gdColLow) / gdWinHeight > 0.4) {
          return true;
        } else {
          return false;
        }
      }
    }

    // Load more when reached bottom
    var gdArtsData;
    var gdArtsLength;
    var gdArtsInserting = 0;
    var gdArtsAdd       = 0;
    var gdArtsLocked    = false;

    var gdArtsFunc1 = function() {
      var gdArtsLeft  = $('#gd-art-gallery>.col-sm-6').eq(0);
      var gdArtsRight = $('#gd-art-gallery>.col-sm-6').eq(1);
      gdArtsLength    = gdArtsData.length;

      if (gdArtsAdd === 3) {
        gdArtsAdd = 0;
      }
      if (gdArtsInserting === gdArtsLength) {
        $('#gd-art-gallery-loading').addClass('hidden');
      } else {
        //$('.gd-artlist-toggle.active').removeClass('active').next().find('.gd-artlist-more').text('SEE MORE +');

        while (!gdArtsLocked && gdArtsInserting < gdArtsLength && gdArtsAdd < 3) {
          var gdArtsDataTemp = gdArtsData[gdArtsInserting];
          var gdArtsHtmlTemp = '';

          gdArtsLocked = true;
          gdArtsHtmlTemp += '<div class="art-g-box">'+
                            '<div class="art-g-box-header"><img src="'+gdArtsDataTemp.avatar+'">'+
                            '<div class="art-g-box-header-text"><h3>'+gdArtsDataTemp.name+'<span>'+gdArtsDataTemp.date+'</span></h3>'+
                            '<a href="'+gdArtsDataTemp.link+'">ARTIST BOIGRAPHY<span class="icomoon-chevron-small-right"></span></a>'+
                            '</div></div><div class="gd-artlist"><div class="row">';
          for (var loop2 = 0; loop2 < gdArtsDataTemp.work.length; loop2++) {

            gdArtsHtmlTemp += '<div class="col-sm-6"><article><a href="'+gdArtsDataTemp.work[loop2].link+'">'+
                              '<img src="'+gdArtsDataTemp.work[loop2].url+'"></a>'+
                              '<h4>'+gdArtsDataTemp.work[loop2].title+'</h4><p>'+gdArtsDataTemp.work[loop2].des+'</p></article></div>';
            if (loop2 === 1) {
              gdArtsHtmlTemp += '<div class="gd-artlist-toggle">';
            }
            if (loop2 > 1 && loop2 === gdArtsDataTemp.work.length - 1) {
              gdArtsHtmlTemp += '</div><div class="col-sm-6"><a href="javascript:;" class="gd-artlist-more">SEE MORE +</a></div>';
            }
          }
          gdArtsHtmlTemp += '</div></div></div>';

          if ($(window).width() >= 768) {
            if (gdArtsLeft.height() < gdArtsRight.height()) {
              gdArtsLeft.append(gdArtsHtmlTemp);
            } else {
              gdArtsRight.append(gdArtsHtmlTemp);
            }
          } else {
            gdArtsRight.append(gdArtsHtmlTemp);
          }

          gdArtsAdd += 1;
          gdArtsInserting += 1;
          gdArtsLocked = false;
        }
      }
    }

    var gdArtsFunc2 = function() {
      var gdCurrentFloor = $('#gdfloorlist>.active').index();
      var gdArtsHtmlTemp = '<div class="row">';
      var gdArtsDataTemp;
      gdArtsLength  = gdArtsData[gdCurrentFloor].length;

      if (gdArtsAdd === 4) {
        gdArtsAdd = 0;
      }
      if (gdArtsInserting === gdArtsLength) {
        $('#gd-art-gallery-loading').addClass('hidden');
      } else {
        if (!gdArtsLocked) {
          gdArtsLocked = true;
          while (gdArtsInserting < gdArtsLength && gdArtsAdd < 4) {
            gdArtsDataTemp = gdArtsData[gdCurrentFloor][gdArtsInserting];
            gdArtsHtmlTemp += '<div class="col-sm-3"><a href="' + gdArtsDataTemp.link + '"><img src="' + gdArtsDataTemp.src + '">' +
                              '<h4>' + gdArtsDataTemp.title + '</h4><p>' + gdArtsDataTemp.des + '</p></a></div>';
            gdArtsAdd += 1;
            gdArtsInserting += 1;
          }
          gdArtsHtmlTemp += '</div>';
          $('#gd-art-gallery').append(gdArtsHtmlTemp);
          gdArtsLocked = false;
        }
      }
    }

    if ($('body').hasClass('t22-is')) {
        $.getJSON('/en/t22/arts.json', function (data) {
        gdArtsData = data;
      })
      .fail(function() {
        alert('Failed to load arts data.');
      });
    }

    if ($('body').hasClass('t22-svg')) {
      $.getJSON('/en/t22-2/arts.json', function(data) {
        gdArtsData = data;
        $(window).trigger(scroll);
      })
      .fail(function() {
        alert('Failed to load arts data.');
      });
    }

    $(window).on('scroll resize', function() {
      var gdReachedBottom = $('#gd-art-gallery-loading').offset().top < winSize().height + $(window).scrollTop();
      if ($('body').hasClass('t22-is') && (gdReachedBottom || gdCheckColumns('#gd-art-gallery', '.col-sm-6'))) {
        //For page T22

        //Remove this line below in production environment
        gdArtsInserting = 0;
        //Remove this line above in production environment
        
        gdArtsFunc1();
      } else if ($('body').hasClass('t22-svg') && gdReachedBottom) {
        //For page T22-2

        //Remove this line below in production environment
        gdArtsInserting = 0;
        //Remove this line above in production environment
        
        gdArtsFunc2();
      }
      
    });
    
    $('body').on('click', '.gd-artlist .gd-artlist-more', function() {
      if ($(this).text() === 'SEE MORE +') {
        $(this).text('LESS -');
      } else {
        $(this).text('SEE MORE +');
      }
      $(this).parent().prev().toggleClass('active');
    });
  }*/


  if ($('body').hasClass('t22')) {
    $('#gd-art-gallery').infinitescroll({
      itemSelector : "#gd-art-gallery>*"
    },function(){
      $(window).lazyLoadXT();
      if ($('body').hasClass('t22-is')){
        $('#gd-art-gallery > .col-sm-6:eq(0)').append($('#gd-art-gallery > .col-sm-6:eq(2)').children());
        $('#gd-art-gallery > .col-sm-6:eq(1)').append($('#gd-art-gallery > .col-sm-6:eq(3)').children());
        $('#gd-art-gallery > .col-sm-6:gt(1)').remove();
      }
    });

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
    $("#gd-art-gallery + .navigation a").on('click', function(e){
      $("#gd-art-gallery").infinitescroll('retrieve');
      return false;
    });
  }


  /**********************************************************************************************************
    * T32 Infinite scroll
   **********************************************************************************************************/
  if ($('body').hasClass('t32')) {
      $('.gd-search-body>ol').infinitescroll({
          navSelector: "div.navigation",
          nextSelector: "div.navigation a:first",
          itemSelector: ".gd-search-body>ol>*",
          loadingImg: "/img/loading.gif",
          loadingText: "Loading ...",
          animate: true,
          extraScrollPx: 50,
          donetext: "No more results.",
          bufferPx: 40,
          errorCallback: function () { },
          localMode: true
      }, function (arrayOfNewElems) {
      });
  }


    /**********************************************************************************************************
   * GD Lightbox
   **********************************************************************************************************/
  $('.gd-lightbox-link.gd-lightbox-video').append('<div class="mejs-overlay-button"></div>');
  $('body').on('click', '.gd-lightbox-link', function (event) {
    event.preventDefault();
    var $link = $(this);
    var gdLightMax = $(window).height() - 150;
    var gdLightSrc     = $(this).data('lightsrc');
    var gdLightTitle = $(this).data('lighttitle') || 'Lightbox';
    var gdLightPoster  = $(this).data('lightposter')
    var gdLightContent = '';

      // update the logic to support video popup & floor plan with pin
    if ($(this).hasClass('gd-lightbox-map')) {
        gdLightContent = '<div class="gd-location-map" data-lat="' + $(this).data('lat') + '" data-lng="' + $(this).data('lng') + '"><iframe src="map-simple-iframe.html" frameborder="0"></iframe></div>';
    } else if ($(this).hasClass('gd-lightbox-video')) {
        gdLightContent = '<video class="hide" src="' + gdLightSrc + '" poster="' + gdLightPoster + '" type="video/mp4" width="100%" height="100%" controls="controls" preload="none"></video>';

    } else if ($(this).hasClass('gd-lightbox-pin')) {
        gdLightContent = '<img class="gd-lightbox-img" src="' + gdLightSrc + '" /><div class="pin icomoon-location2"></div>';
    } else {
        gdLightContent = '<img class="gd-lightbox-img" src="' + gdLightSrc + '" />';
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
                pinX = parseInt(parts[0]),
                pinY = parseInt(parts[1]),
                $pin = $('#gd-lightbox .pin'),
                $img = $('#gd-lightbox .modal-body .gd-lightbox-img');
            
            tmpimg = new Image();
            tmpimg.onload = function(){
                setTimeout(function(){
                    var imgW = tmpimg.width, imgH = tmpimg.height,
                        scaleX = $img.width() / imgW,  scaleY = $img.height() / imgH,
                        x = pinX * scaleX, y = pinY * scaleY;
                    $('#gd-lightbox .pin').css({
                        left: ($img.parent().width() - $img.width()) /2 + x - $pin.width() / 2 + 'px',
                        top: y - $pin.height() + 'px'
                    });
                },200)
            };
            tmpimg.src = gdLightSrc;
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
                enablePluginDebug: true,
                plugins: ['flash', 'silverlight'],
                success: function (mediaElement, domObject) {
                    mediaElement.addEventListener('play', function (e) {
                        scTrackVideo(gdLightSrc);
                    }, false);
                    /*setTimeout(function(){
                      mediaElement.play();
                    }, 500);*/
                }
            });
            $modal.data('player', player);
        }
        // handle custom scrollbar
        if (!$link.hasClass('gd-lightbox-map') && !$link.hasClass('gd-lightbox-video')) {
            $('#gd-lightbox .modal-body').mCustomScrollbar({ theme: "dark-2" });
        }
    });


      if (isIE8()) {
          setTimeout(function () {
              $modal.trigger('shown.bs.modal')
          }, 200)
      }

  });
  /**********************************************************************************************************
   * Audio Player
   
   **********************************************************************************************************/
  $('body').on('click', '[data-player]', function() {
    var gdAudioFile;
    var gdAudioFrame;
    if ($('.gd-audio-frame').length) {
      return false;
    } else {
      gdAudioFile = $(this).data('player');
      gdAudioFrame= '<div class="gd-audio-frame" data-audiofile="' + gdAudioFile + '"><iframe src="/files/landmark/audioplayer/index.html" frameborder="0"></iframe></div>';
      $(this).parent().addClass('gd-audio-wrapper').append(gdAudioFrame);
    }
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
  
    $('.gd-showcase-control').click(function () {
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
          if (gdShowcaseIndex < 0) {
              gdShowcaseIndex -= 1;
          }
      } else {
          if (gdShowcaseIndex < gdShowcaseStopNo) {
              gdShowcaseIndex += 1;
          } else {
              // retrieve by ajax then trigger event again
              $.get(gdShowcaseNextUrl.replace(/([&?])startindex=\d+$/, '$1startindex=' + gdShowcaseNo), function (html) {
                  var items = $(html);
                  gdShowcaseItems.append(items);
                  items.css('width', gdShowcaseItems.find('>li:first').css('width'));
                  $(window).lazyLoadXT();
                  $self.trigger('click');
              });
          }
      }
      
      gdShowcase.data('position', gdShowcaseIndex);
      gdShowcaseMove = gdShowcaseIndex * gdShowcaseItemW;
      gdShowcaseItems.css({'left': gdShowcaseMove});
      
      $('body').trigger('scroll');
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
   * Long Article
   **********************************************************************************************************/
  // var gdArticleFunc = function() {
  //   $('.gd-longarticle').dotdotdot({
  //     ellipsis	: '... ',
  //     wrap		: 'word',
  //     fallbackToLetter: true,
  //     after		: 'a.gd-longarticle-switcher',
  //     watch		: true,
  //     tolerance	: 0,
  //     callback	: function( isTruncated, orgContent ) {
  //       if ( isTruncated ) {
  //         $(this).find('.gd-longarticle-switcher').text('READ MORE +');
  //       }
  //     },
  //     lastCharacter	: {
  //       remove		: [ ' ', ',', ';', '.', '!', '?' ],
  //       noEllipsis	: []
  //     }
  //   });
  // }
  // gdArticleFunc();
  
  // $('body').on('click', '.gd-longarticle-switcher', function () {
  //   var isTruncated = $(this).closest('.gd-longarticle').triggerHandler("isTruncated");
  //   if ( isTruncated ) {
  //     $(this).closest('.gd-longarticle').trigger("destroy").find('.gd-longarticle-switcher').text('LESS -');
  //   } else {
  //     gdArticleFunc();
  //   }
  // });
  var gdTextOn  = $('.gd-longarticle').eq(0).data('text-on');
  var gdTextOff = $('.gd-longarticle').eq(0).data('text-off');
  
  $('.gd-longarticle').trunk8({
      lines: gdSettings.LongVersionContentNumberOfLines + 1,
    fill: '&hellip; <a id="read-more" href="#">' + gdTextOn + '</a>'
  });
  
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
  var gdBottomSliderFunc = function() {
    $('#gd-carousel-info .gd-carousel-detail p').dotdotdot({
      ellipsis	: '... ',
      wrap		: 'word',
      fallbackToLetter: true,
      after		: null,
      watch		: true,
      tolerance: 0,
      height: parseInt($('#gd-carousel-info .gd-carousel-detail p').css('lineHeight')) * gdSettings.ShortVersionContentNumberOfLines,
      lastCharacter	: {
        remove		: [ ' ', ',', ';', '.', '!', '?' ],
        noEllipsis	: []
      }
    });
  }
  gdBottomSliderFunc();

  /**********************************************************************************************************
   * Carousel image
   **********************************************************************************************************/
  if ($('.gd-carousel-info').length) {
    $('.gd-carousel-info .carousel-image').each(function() {
      $(this).css({'background': 'url(' + $(this).data('bgsrc') + ') center 0 no-repeat' });
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
  $(window).on('scroll', function() {

    if ($('body').height() > $(window).height() && $(window).scrollTop() / $(window).height() > 0.25) {

      if (!isIE8() && $(window).scrollTop() + $(window).height() > $('body').height() - $('.site-footer').height()) {
        $backToTopBtn.addClass('bottom');
      } else {
        $backToTopBtn.removeClass('bottom');
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

      $(this).next().toggle('slow');
    });
  }
  

    /**********************************************************************************************************
     * T31
     **********************************************************************************************************/
  if ($('body').hasClass('t31')) {
      $('#gd-preferred-comm .dropdown-menu>li>a').click(function () {
          $('#gd-subforms>.tab-content>.tab-pane').eq($(this).data('value')).addClass('active in').siblings().removeClass('active in');
      });
  }



    /**********************************************************************************************************
     * T3
     **********************************************************************************************************/
  if ($('body').hasClass('t3')) {
      var gdPromoWrapper = $('.gd-promo-body').eq(0);
      gdPromoWrapper.masonry({
          itemSelector: '.gd-promo-box'
      });

      gdPromoWrapper.infinitescroll({
          navSelector: "div.navigation",
          nextSelector: "div.navigation a:first",
          itemSelector: ".gd-promo-body>*"
      }, function (items) {
          gdPromoWrapper.append(items).masonry('appended', items);
          $(window).unbind('.infscr');
      });
      $(window).unbind('.infscr');

      // manually trigger when clicking "SEE MORE +"
      $(".navigation a").on('click', function (e) {
          $(".gd-promo-body").infinitescroll('retrieve');
          return false;
      });
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
    return false;
  });


    /**********************************************************************************************************
     * GD Checkbox control
     **********************************************************************************************************/
  $('body').on('click', '.gd-checkbox', function () {
      var gdCheckbox = $(this).closest('.gd-checkbox-wrapper');
      var gdUserChosen = gdCheckbox.find('input[type=hidden]');
      var gdChosen = [];
      $(this).toggleClass('active');
      gdCheckbox.find('.active').each(function () {
          gdChosen.push($(this).data('value'));
      });
      gdUserChosen.val(gdChosen.join(','));
  });


    /**********************************************************************************************************
     * slick slider
     **********************************************************************************************************/
  $('.gd-slick-slider').slick({
      dots: false,
      infinite: true,
      speed: 300,
      slidesToShow: 3,
      slidesToScroll: 1,
      centerMode: false,
      variableWidth: true,
      prevArrow: '<button type="button" class="slick-prev" tabindex="0" role="button"><span class="icomoon-thin-arrow-L"></span></button>',
      nextArrow: '<button type="button" class="slick-next" tabindex="0" role="button"><span class="icomoon-thin-arrow-R"></span></button>',

      responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }
      ]
  });


    /**********************************************************************************************************
     * GD Promotion Tabcontrol
     **********************************************************************************************************/
  $('body').on('click', '.tab-pane-switch', function () {
      var gdCurrentPane = $(this).next();
      if (gdCurrentPane.hasClass('active')) {
          $(this).find('span').text('+');
      } else {
          $(this).find('span').text('-');
      }
      gdCurrentPane.toggleClass('in active');
  });

});
});





















