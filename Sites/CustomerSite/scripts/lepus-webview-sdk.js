var SDK = {};
SDK.Lepus = {};
var createFnKey = function(key) {
	var timestamp = new Date().getTime();
	var fnKey = LepusUtils.md5(key + "" + timestamp);
	return fnKey;
}

var sysCallbackFn = {
	onResume : {},
	onPause : {}
};
var sysCallback = function(callbacks) {
	// console.log("-------callback sysCallback-------");
	for ( var m in callbacks) {
		var fn = callbacks[m];
		// console.log("-------callback sysCallback : " + m + "-------")
		if (fn) {
			fn();
		}
	}
}
var onResume = function() {
	// console.log("-------callback onResume-------");
	sysCallback(sysCallbackFn.onResume);
}
var onPause = function() {
	sysCallback(sysCallbackFn.onPause);
}
// 3.1 应用相关(Sys)
SDK.Lepus.Sys = {
	// Android平台
	PLATFORM_ANDROID : 0,
	// iOS平台
	PLATFORM_IOS : 1,
	// 其他平台
	PLATFORM_OTHER : 10,
	// 天兔应用
	ACTIVITY_LEPUS : 1,
	// W3M应用
	ACTIVITY_W3M : 2,
	// 3.1.1 应用版本号
	getAppVersionCode : function() {
		try {
			return window.lepus.getAppVersionCode();
		} catch (e) {
			return 0;
		}
	},
	// 3.1.2 应用版本名称
	getAppVersionName : function() {
		try {
			return window.lepus.getAppVersionName();
		} catch (e) {
			return "";
		}
	},
	// 3.1.3 设备平台
	getPlatform : function() {
		try {
			var usa = navigator.userAgent;
			if (usa.indexOf("Android") > -1) {
				return this.PLATFORM_ANDROID;
			} else if (usa.indexOf("iPhone") > -1 || usa.indexOf("iPad") > -1
					|| usa.indexOf("iPod") > -1) {
				return this.PLATFORM_IOS;
			} else if (usa.indexOf("Windows Phone") > -1
					|| usa.indexOf("WPDesktop") > -1) {
				// p = "winphone";
			} else {
				// p = "pc";
			}
			return this.PLATFORM_OTHER;
		} catch (e) {
			return this.PLATFORM_OTHER;
		}
	},
	// 3.1.4 判断是否是在天兔应用内浏览
	isLepus : function() {
		try {
			return window.lepus.isLepus();
		} catch (e) {
			return false;
		}
	},
	// 3.1.5 关闭当前浏览器
	close : function() {
		try {
			window.lepus.close();
		} catch (e) {
			LepusUtils.LOG("------当前版本不支持此close方法----");
		}
	},
	// 3.1.6 判断网络是否正常
	isNetConnect : function() {
		try {
			window.lepus.isNetConnect();
		} catch (e) {
			LepusUtils.LOG("------当前版本不支持此isNetConnect方法----");
		}
	},
	onResume : function(callback) {
		// console.log("-------callback sys onResume-------");
		var fnKey = createFnKey("onResume");
		sysCallbackFn.onResume[fnKey] = callback;
	},
	onPause : function(callback) {
		var fnKey = createFnKey("onPause");
		sysCallbackFn.onPause[fnKey] = callback;
	},
	// 设置当前页面的标题，未实现
	setTitle : function(title) {
		window.lepus.setTitle(title);
	},
	// 显示加载loading圈
	showProgress : function() {
		window.lepus.showProgress();
	},
	// 隐藏加载loading圈
	hideProgress : function() {
		window.lepus.hideProgress();
	},
	// 页面初始化完成
	initComplete : function() {
		window.lepus.initComplete();
	},
	// 打开指定的Activity,param为JSON Object
	startActivity : function(activityPkg, type, params, title) {
		var json = typeof params == 'object' ? JSON.stringify(params) : params;
		LepusUtils.LOG("activityPkg：" + activityPkg + "   type:" + type
				+ "  params:" + json + "  title:" + title);
		try {
			window.lepus.startActivity(activityPkg, type, json, title ? title
					: "");
		} catch (e) {
			LepusUtils.LOG("------当前版本不支持此startActivity方法----");
		}
	},
	// 是否有升级权限（灰度升级）
	hasUpdate : function() {
		try {
			return window.lepus.hasUpdate();
		} catch (e) {
			LepusUtils.LOG("------当前版本不支持此hasUpdate方法----");
		}
		return false;
	}
}

// 3.2 内置浏览器更多按钮操作（或分享）
var moreCallback = {};
// 3.2.2 更多选项中点击每项回调js方法
var moreItemClick = function(shareInfo, callbackData, tag) {
	var o = moreCallback[tag];
	if (o) {
		var fn = o.fn || o;
		var shareJson = null;
		try {
			shareJson = eval('(' + shareInfo + ')');
		} catch (e) {
		}
		fn(shareJson, callbackData);
		if (!o.notAllowDel) {
			delete moreCallback[tag];
		}
	}
}

// 3.2.4 直接调起分享对话框回调js函数
var showShareCallback = function(shareInfo, callbackData, tag) {
	moreItemClick(shareInfo, callbackData, tag);
}

SDK.Lepus.More = {
	// 分享
	OPERATE_SHARE : 0,
	// 删除
	OPERATE_DEL : 1,
	// 打开一个新的网页，content字段为网页地址
	OPERATE_OPEN_NEW_URL : 2,
	// 按钮
	OPERATE_BUTTON : 3,
	// 分享支持类型：秘密
	SHARE_TYPE_SECRET : 100,
	// 分享支持类型：连接
	SHARE_TYPE_LINK : 101,
	// 分享支持类型：文章
	SHARE_TYPE_ARTICLE : 102,
	// 分享支持类型：资讯
	SHARE_TYPE_NEWS : 103,
	// 分享支持类型：卡券
	SHARE_TYPE_COUPON : 104,
	// 分享支持类型：图文直播分享
	SHARE_TYPE_LIVE : 105,
	// 分享支持类型：espace分享
	SHARE_TYPE_ESPACE : 106,
	// 3.2.1 初始化更多选项(包含分享)
	initMore : function(jsonData, callback) {
		var fnKey = createFnKey("initMore_" + jsonData);
		var o = {};
		o.fn = callback;
		o.notAllowDel = true;
		moreCallback[fnKey] = o;
		window.lepus.initMore(jsonData, fnKey);
	},
	// 3.2.2 直接调起分享对话框架
	showShare : function(content, icon, callbackData, callback) {
		var fnKey = createFnKey("showShare_" + content + callbackData);
		var o = {};
		o.fn = callback;
		o.notAllowDel = true;
		moreCallback[fnKey] = o;
		window.lepus.showShare(content, icon, callbackData, fnKey);
	}
}

SDK.Lepus.Version = {
	// 老的客户端版本
	SHAKE_VERSION_ONE : 19,
	isAfterShakeVersionOne : function() {
		return SDK.Lepus.Sys.getAppVersionCode() > this.SHAKE_VERSION_ONE ? true
				: false;
	}
}
// 3.4 用户相关 (User USER)
SDK.Lepus.USER = {
	// 获取用户标识，最好为用户的W3账号
	getUserMark : function() {
		try {
			LepusUtils.LOG("-----getUserMark : " + window.lepus.getUserMark()
					+ "-----");
			return window.lepus.getUserMark();
		} catch (e) {
			LepusUtils.LOG("-----手机版本过低，请升级客户端版本-----");
		}
		return "test1";
	},
	// 3.4.2 用户行为分析
	customerEvent : function(event, lavel, extendData) {
		if(SDK.Lepus.Sys.isLepus()) {
			try {
				window.lepus.onEvent(event, lavel, extendData);
			} catch (e) {
				LepusUtils.LOG("手机版本过低，请升级客户端版本");
			}
		}else {
			ha('trackEvent', event, {opr_wf_n: lavel});
		}
	},
	// 3.4.3 获取用户的角色
	getUserRoles : function() {
		try {
			var rolesArr = window.lepus.getUserRoles();
			return JSON.parse(rolesArr);
		} catch (e) {
			LepusUtils.LOG("-------获取用户角色出错：当前版本不支持此方法-------");
		}
		return [];
	}
}

// 3.5 其他功能(Other Function OF)

var ofCallback = {};
// 3.5.4 打开新浏览器窗口成功与否都需要调用的js方法
var openBrowser = function(result, tag) {
	var fn = ofCallback[tag];
	if (fn) {
		fn(result);
		delete ofCallback[tag];
	}
}

SDK.Lepus.OF = {
	// 默认
	BROWSER_SELF : 0,
	// QQ浏览
	BROWSER_QQ : 1,
	// UC浏览
	BROWSER_UC : 2,
	// BAIDU浏览
	BROWSER_BAIDU : 3,
	// 360浏览
	BROWSER_360 : 4,
	// 猎豹浏览
	BROWSER_LIEBAO : 5,
	// 系统默认的浏览器
	BROWSER_SYS_DEF : 6,
	// 摇一摇
	RING_SHAKE_SOUND_MALE : 0,
	// 摇后无匹配或惊喜
	RING_SHAKE_NO_MATCH : 1,
	// 摇后有匹配或惊喜
	RING_SHAKE_MATCH : 2,
	// 3.5.3 打开新浏览器窗口
	openBrowser : function(url, target, callback, title) {
		var fnKey = createFnKey("openBrowser_" + url);
		ofCallback[fnKey] = callback;
		if (!title) {
			window.lepus.openBrowser(url, target, fnKey);
		} else {
			window.lepus.openBrowser(url, target, fnKey, title);
		}
	},
	// 3.5.5 吐司信息提示
	toast : function(msg) {
		window.lepus.toast(msg);
	},
	// 3.5.7 用户行为分析
	customerEvent : function(event, lavel, extendData) {
		try {
			// window.lepus.onEvent(event, lavel, extendData);
			window.lepus.customerEvent(event, lavel, extendData);
		} catch (e) {
			LepusUtils.LOG("此版本不支持该方法，请使用SDK.Lepus.USER.customerEvent");
		}
	}
}
var networkCallback = {};
var httpRequest = function(result, tag) {
	// result = result.replace(/([^:\{\},])(\")([^:\{\},])/g,"$1\\$2$3");

	console.log("----httpRequest: " + result + " ----");
	var o = networkCallback[tag];
	if (o) {
		var success = o.success;
		if (success) {
			if (o.dataType == SDK.Lepus.NW.HTTP_DATA_JSON) {
				// var data = eval('(' + result + ')');

				var data = {};
				try {
					data = JSON.parse(result);
				} catch (e) {
					console.log("----JSON.parse error 1----");
					try {
						var tempresult = result.replace(
								/([^:\{\},\[\]])(\")(?=[^:\{\},\[\]])/gi,
								"$1\\$2");
						data = JSON.parse(tempresult);
					} catch (e) {
						console.log("----JSON.parse error 2----");
						try {
							var tempresult2 = result.replace(
									"in adjacent markets,",
									"in adjacent markets, ").replace(
									/([^:\{\},\[\]])(\")(?=[^:\{\},\[\]])/gi,
									"$1\\$2");
							data = JSON.parse(tempresult2);
						} catch (e) {
							console.log("----JSON.parse error 3----");
							try {
								var tempresult3 = result.replace(
										"in adjacent markets,",
										"in adjacent markets, ").replace(
										/(")/gi, "\\$1").replace(
										/([:\{\},\[\]])(\\)(")/gi, "$1$3")
										.replace(/(\\)(")([:\{\},\[\]])/gi,
												"$2$3");
								data = JSON.parse(tempresult3);
							} catch (e) {
								console.log("----JSON.parse error4----");
								try {
									var tempresult4 = result
											.replace(
													/\\u([\u4E00-\u9FA5]|[\uFE30-\uFFA0])/g,
													function() {
														return unescape(RegExp["$1"]
																.replace(/\u/g,
																		"%u"));
													});
									data = JSON.parse(tempresult4);
								} catch (e) {
									console.log("----JSON.parse error5----");
								}
							}
						}
					}
				}
				success(data);
			} else if (o.dataType == SDK.Lepus.NW.HTTP_DATA_STRING) {
				success(result);
			}
		}
		delete networkCallback[tag];
	}
}
var httpRequestError = function(status, msg, tag) {
	var o = networkCallback[tag];
	if (o) {
		var error = o.error;
		if (error) {
			error({
				status : status,
				message : msg
			});
		}
		delete networkCallback[tag];
	}
}
SDK.Lepus.NW = {
	// 请求方式：GET,预留，客户端未实现
	HTTP_GET : 1,
	// 请求方式：POST，预留，客户端未实现
	HTTP_POST : 2,
	// 请求方式：PUT
	HTTP_PUT : 3,
	// 请求方式：DELETE
	HTTP_DELETE : 4,
	// 请求方式：REST风格 GET
	HTTP_GET_REST : 5,
	// 请求方式：REST风格POST
	HTTP_POST_REST : 6,
	// 调用者需要的返回数据格式为JSON
	HTTP_DATA_JSON : 1,
	// 调用者需要的返回数据格式为String
	HTTP_DATA_STRING : 2,
	httpRequest : function(url, param, type, header, dataType, success, error) {
		var fnKey = createFnKey(url);
		networkCallback[fnKey] = {
			dataType : dataType,
			success : success,
			error : error
		};
		window.lepus.httpRequest(url, param, type, header, fnKey);
	}
}

var msgListener = {};
var sendMsg = function(msgData, msgId) {
	var msgDataArr = [];
	try {
		msgDataArr = JSON.parse(msgData);
	} catch (e) {
		LepusUtils.LOG("--------转换消息数组出错-----");
	}
	var msgIdArr = [];
	try {
		msgIdArr = JSON.parse(msgId);
	} catch (e) {
		LepusUtils.LOG("--------转换消息标识数组出错-----");
	}
	for ( var m in msgListener) {
		var fn = msgListener[m];
		if (fn) {
			fn(msgDataArr, msgIdArr);
		}
	}
}
// 消息提醒
SDK.Lepus.MSG = {
	// 消息通知
	TYPE_NEW_MSG : 1,
	// 注册接收消息监听
	registerMsgReceiver : function(listener) {
		var key = createFnKey("msg_register");
		msgListener[key] = listener;
	},
	// 通知原生应用消息已接收
	notify : function(msgId, type) {
		window.lepus.reply(msgId, type);
	},
	// 主动接收消息
	readMsg : function(curPage, size, type) {
		var msg = window.lepus.readMsg(curPage, size, type);
		if (!msg) {
			return [];
		}
		return JSON.parse(msg);
	},
	// 主动发送消息
	sendMsg : function(msgObject, type) {
		var msg = JSON.stringify(msgObject);
		window.lepus.sendMsg(msg, type);
	}
}