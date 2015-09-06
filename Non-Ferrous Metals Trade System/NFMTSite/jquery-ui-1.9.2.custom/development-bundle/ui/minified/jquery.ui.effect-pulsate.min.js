/*! jQuery UI - v1.9.2 - 2014-05-12
* http://jqueryui.com
* Copyright 2014 jQuery Foundation and other contributors; Licensed MIT */

(function(e){e.effects.effect.pulsate=function(t,i){var s,a=e(this),n=e.effects.setMode(a,t.mode||"show"),r="show"===n,o="hide"===n,l=r||"hide"===n,h=2*(t.times||5)+(l?1:0),u=t.duration/h,d=0,c=a.queue(),p=c.length;for((r||!a.is(":visible"))&&(a.css("opacity",0).show(),d=1),s=1;h>s;s++)a.animate({opacity:d},u,t.easing),d=1-d;a.animate({opacity:d},u,t.easing),a.queue(function(){o&&a.hide(),i()}),p>1&&c.splice.apply(c,[1,0].concat(c.splice(p,h+1))),a.dequeue()}})(jQuery);