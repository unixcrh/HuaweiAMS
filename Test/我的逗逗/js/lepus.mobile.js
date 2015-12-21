var $event = {
	on : {},
	unbind : {
		"tap" : function($) {
			// var el = $.get(0);
			var tabCount = $.length || 1;
			if ($.handler["touchstart"] && $.handler["touchend"]
					&& $.handler["touchmove"]) {
				for (var i = 0; i < tabCount; i++) {
					var el = $.get(i);
					el.removeEventListener('touchstart',
							$.handler["touchstart"], false);
					el.removeEventListener('touchend', $.handler["touchend"],
							false);
					el.removeEventListener('touchmove', $.handler["touchmove"],
							false);
				}
			}
		}
	}
}
$.fn.extend({
			handler : {},
			lepusUnbind : function(e) {
				var fn = $event.unbind[e];
				if (fn) {
					fn(this);
				}
			},
			lepusOn : function(event, fn,dispatcher) {
				var timeSta, timeEnd, timeSpace = 200, isMove = false;
				this.handlerEvent = fn;
				if (event == "tap") {
					this.handler["touchstart"] = function eventDown(e) {
						timeSta = new Date().getTime();
						if (dispatcher && dispatcher["touchstart"]) {
							e.preventDefault();
						}
					}
					this.handler["touchend"] = function eventUp(e) {
						timeEnd = new Date().getTime();
						var tem = timeEnd - timeSta;
						if (tem <= timeSpace && !isMove) {
							e.preventDefault();
							if (fn) {
								fn(e);
							};
						}
						isMove = false;
						if (dispatcher && dispatcher["touchend"]) {
							e.preventDefault();
						}
					}
					this.handler["touchmove"] = function eventMove(e) {
						isMove = true;
						if (dispatcher && dispatcher["touchmove"]) {
							e.preventDefault();
						}
					}
					// var tab = this.get(0);
					var tabCount = this.length || 1;
					for (var i = 0; i < tabCount; i++) {
						var tab = this.get(i);
						tab.addEventListener('touchstart',
								this.handler["touchstart"]);
						tab.addEventListener('touchend',
								this.handler["touchend"]);
						tab.addEventListener('touchmove',
								this.handler["touchmove"]);
					}
				}
			}
		})