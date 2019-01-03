var speed = 5000;
canTick = true;

$(document).ready(function() {
	$('.ticker-container ul').each(function(i) {
		if ($(window).width() >= 500) {
			$(this).find('li').width($(window).width() - parseInt($(this).css('left')));
		}
		if (i == 0) {
			$(this).addClass('ticker-active');
		} else {
			$(this).addClass('not-active');
		}
		if ($(this).find('li').height() > 30) {
			$(this).find('li').css({
				'height': '20px',
				'width': '200%',
				'text-align': 'left',
				'padding-left': '5px'
			});
			$(this).find('li').css('width', $(this).find('li span').width());
		}
	});
	startTicker();
	animateTickerElementHorz();
});

$(window).resize(function() {
	$('.ticker-container ul').each(function(i) {
		if ($(this).find('li').height() > 30) {
			$(this).css({
				'height': '20px',
				'width': '200%',
				'text-align': 'left',
				'padding-left': '5px'
			});
			$(this).find('li').css('width', $(this).find('li span').width());
		}
	});
});

function startTicker() {
	setInterval(function() {
		if (canTick) {
			$('.ticker-container ul li.ticker-active')
				.removeClass('ticker-active')
				.addClass('remove');
			if ($('.ticker-container ul li.remove').next().length) {
				$('.ticker-container ul li.remove')
					.next()
					.addClass('next');
			} else {
				$('.ticker-container ul li')
					.first()
					.addClass('next');
			}
			$('.ticker-container ul li.next')
				.removeClass('not-active next')
				.addClass('ticker-active');
			setTimeout(function() {
				$('.ticker-container ul li.remove')
					.css('transition', '0s ease-in-out')
					.removeClass('remove')
					.addClass('not-active finished');
				if ($(window).width() < 500) {
					if ($('.ticker-container ul li.finished li').width() < $(window).width()) {
						$('.ticker-container ul li.finished').removeClass('finished');
					}
				} else {
					if ($('.ticker-container ul li.finished li').width() < ($(window).width() - (parseInt($('.ticker-container ul div.ticker-active').css('left')) + 15))) {
						$('.ticker-container ul li.finished').removeClass('finished');
					}
				}
				setTimeout(function() {
					$('.ticker-container ul div')
						.css('transition', '0.25s ease-in-out');
				}, 75);
				animateTickerElementHorz();
			}, 250);
		}
	}, speed);
}

function animateTickerElementHorz() {
	if ($(window).width() < 500) {
		if ($('.ticker-container ul li.ticker-active li').width() > $(window).width()) {
			setTimeout(function() {
				$('.ticker-container ul li.ticker-active li').animate({
					'margin-left': '-' + (($('.ticker-container ul li.ticker-active li').width() - $(window).width()) + 15)
				}, speed - (speed / 5), 'swing', function() {
					setTimeout(function() {
						$('.ticker-container ul li.finished').removeClass('finished').find('li').css('margin-left', 0);
					}, ((speed / 5) / 2)); 
				});
			}, ((speed / 5) / 2));
		}
	} else {
		if ($('.ticker-container ul li.ticker-active li').width() > ($(window).width() - parseInt($('.ticker-container ul div.ticker-active').css('left')))) {
			setTimeout(function() {
				$('.ticker-container ul li.ticker-active li').animate({
					'margin-left': Math.abs($('.ticker-container ul li.ticker-active li').width() - ($(window).width() - parseInt($('.ticker-container ul div.ticker-active').css('left'))) + 15) * -1
				}, speed - (speed / 5), 'swing', function() {
					setTimeout(function() {
						$('.ticker-container ul li.finished').removeClass('finished').find('li').css('margin-left', 0);
					}, ((speed / 5) / 2)); 
				});
			}, ((speed / 5) / 2));
		}
	}
}

$('.ticker-container').on('mouseover', function() {
	canTick = false;
});

$('.ticker-container').on('mouseout', function() {
	canTick = true;
});