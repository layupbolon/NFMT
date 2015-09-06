/*! jQuery UI - v1.9.2 - 2014-05-12
* http://jqueryui.com
* Copyright 2014 jQuery Foundation and other contributors; Licensed MIT */

(function(e){e.effects.effect.clip=function(t,i){var s,a,n,r=e(this),o=["position","top","bottom","left","right","height","width"],l=e.effects.setMode(r,t.mode||"hide"),h="show"===l,u=t.direction||"vertical",d="vertical"===u,c=d?"height":"width",p=d?"top":"left",m={};e.effects.save(r,o),r.show(),s=e.effects.createWrapper(r).css({overflow:"hidden"}),a="IMG"===r[0].tagName?s:r,n=a[c](),h&&(a.css(c,0),a.css(p,n/2)),m[c]=h?n:0,m[p]=h?0:n/2,a.animate(m,{queue:!1,duration:t.duration,easing:t.easing,complete:function(){h||r.hide(),e.effects.restore(r,o),e.effects.removeWrapper(r),i()}})}})(jQuery);