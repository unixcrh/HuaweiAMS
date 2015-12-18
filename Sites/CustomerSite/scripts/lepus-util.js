/**
 * js工具类
 * 
 */
var LepusUtils = {
	ENVIRONMENT_LOCAL : 0,
	ENVIRONMENT_SIT : 1,
	ENVIRONMENT_UAT : 2,
	// 运行环境,便于进行本地调试
	environment : 2,
	testCookie : "WSJSESSIONID=aa:f2e1c9c7; v1st=14D0986B5654A1FD; 2D-4A-29-3E-6C-E6-DF-76-1D-A3-0E-21-2C-88-0E-05=09-24-5A-B6-4D-24-38-CB-27-96-5D-64-D2-F9-17-71-29-6D-DF-C4-77-D4-89-52-DA-E5-67-F0-55-89-29-75-9C-03-E7-55-A5-0D-02-8C-21-C1-D8-0F-2C-6F-31-03; hwssotinter=A1-07-9D-95-46-0C-5E-8B-B2-92-09-73-14-54-1E-F0; hwssotinter3=24664242512256; logFlag_test=out; hwssotest=test1hw%40ssostest1hw%40ssostest1+est1hw%40ssosHWEhw%40ssostest1hw%40ssostest1%40huawei.comhw%40ssos%E6%B5%8B%E8%AF%95_test1hw%40ssosTest_test1hw%40ssostest+test1hw%40ssosuuid%7EdGVzdDE%3Dhw%40ssoshw%40ssoshw%40ssoshw%40ssosuid%3Dtest1%2Cou%3Dcorpusers%2Co%3Dinternet%2Cdc%3Dhuawei%2Cdc%3Dcom; uid=CC-31-CF-CB-3F-C2-CF-41; uid_test=CC-31-CF-CB-3F-C2-CF-41; lang=zh; hwssot=DD-0A-6C-95-82-C2-FF-E0-21-C6-B8-41-BA-3B-C3-74; hwssot3=24664256502410; login_sip=57-FD-EF-47-61-35-99-4B-E3-6E-5B-4D-FD-1D-8B-35; hwsso_login=43D8658010465B3733048306322457A456A4594F0453B2765E1B1895DEB83755B9DB8B0CC8B9DD52549A89D2DAC32E486B6C1BEEDC2865EE6444397F598C4839AB8425A2F3EF235CE0E2D7DDF99C34D4F24BE60571259E62724C8B230888B5C688ACC0B669DBB6D3CE36CA219B1B423FFC6590BAF2007B6EAA2CDE2DAE8A89EB; hwsso_am=77-22-F7-0E-84-9F-31-CD; _sid_=0F-D9-D0-39-29-EA-C1-19-DF-66-29-4E-E3-D9-34-6A-D9-A5-59-8C-DB-7E-CD-7D-58-C1-DA-7C-CE-CF-ED-69-2B-A5-EF-2A-A0-1E-9F-7E; login_sid=0F-D9-D0-39-29-EA-C1-19-DF-66-29-4E-E3-D9-34-6A-D9-A5-59-8C-DB-7E-CD-7D-58-C1-DA-7C-CE-CF-ED-69-2B-A5-EF-2A-A0-1E-9F-7E; login_uid=CC-31-CF-CB-3F-C2-CF-41; login_logFlag=in; suid=CC-31-CF-CB-3F-C2-CF-41; w3Token=95-92-E6-B5-BB-12-6F-55-7A-63-20-D6-F8-5F-EC-51-B2-9E-EF-B7-4F-21-2F-82-BD-B0-89-4D-C4-54-80-37; LtpaToken=cZOL//MWulNmHmXWDocm2OwiybK3ViMGZW532f1+PN1ClbHLCr9P/QWEHarPPmtjZeKFgat3RL6NZ5SnuYLGC3vhp1Sq8HxnnVBcPeCg1f3vRDTtRZbaoPDo5oT8SsAkLacODq/t5MuVD/Jp2I789Z60V+Jz9rdvxio5FFOm7vbLYg4iO+A7yyl+lBq5Hr8WqKwUQA/os2ruvl/MMRb8ecg+X2yP9Y7EVKn0JsOzoU3hm1Sw0DrpyAWTehAaC2Sg7UZcXN/otgG5eaL/0vCE992bxDpMY1T5qPtZDAz132DlL03xfipU30SIMZ9FZh2PbTqNZT/P9RIHFe8JUX/tEJAEPr+cbyXU5AcWE5h9dYbQGgNBxNMdLw==; JSESSIONIDnkgtsv4001rhl7=00007le1tX6DVs5uopDWLev99hp:nkgtsv4001rhl7; WSJSESSIONID=:f2e1c9c7",
	DEBUG : false,
	// 是否显示土司打印log
	TOAST_LOG : false,
	LOG : function(o, moduleMsg) {
		var str = "";
		if (typeof o == 'string') {
			str = o;
		} else {
			str = moduleMsg + "：" + o.status;
		}

		if (!this.DEBUG && this.TOAST_LOG) {
			SDK.Lepus.OF.toast(str);
		}
		console.info(str);
	},
	getUserMark : function() {
		return this.DEBUG ? "TEST" : SDK.Lepus.USER.getUserMark();
	},
	// 获取本地存储的储存对象储,如果不传值默认为当前用户标识为键值
	getObject : function(key) {
		if (!key) {
			key = this.getUserMark();
		}
		if (window.localStorage) {
			var str = window.localStorage[key];
			if (str) {
				try {
					var o = eval('(' + str + ')');
					return o;
				} catch (e) {
					this.LOG(e.message);
				}
			}
		}
		return null;
	},
	// 设置本地存储的储存对象储
	setObject : function(key, value) {
		if (window.localStorage) {
			var str = JSON.stringify(value);
			window.localStorage[key] = str;
		}
	},
	// 添加本地存储对象储（或修改现有存储对象）
	addObject : function(key, value) {
		var o = this.getObject(key) || {};
		value = $.extend(o, value);
		this.setObject(key, value);
	},
	getString : function(key) {
		return window.localStorage[key];
	},
	setString : function(key, value) {
		window.localStorage[key] = value;
	},
	check : {
		checkNull : function(temp) {
			if (temp == undefined || temp == null
					|| (typeof temp == 'string' && temp.length == 0)) {
				return true;
			}
			return false;
		},

		// 时间转换
		localDate : function(time, flag) {
			var d = new Date(time);
			d.toLocaleString();
			var year = d.getFullYear();
			var month = d.getMonth() + 1;
			var date = d.getDate();

			var hour = d.getHours();
			var minute = d.getMinutes();
			var second = d.getSeconds();

			if (hour < 10) {
				hour = "0" + hour;
			}
			if (minute < 10) {
				minute = "0" + minute;
			}
			if (second < 10) {
				second = "0" + second;
			}

			var latestTime;
			switch (flag) {
				case 0 :
					latestTime = year + "-" + month + "-" + date;
					break;
				case 1 :
					latestTime = year + "-" + month + "-" + date + "  " + hour
							+ ":" + minute;
					break;
				case 2 :
					latestTime = year + "-" + month + "-" + date + "  " + hour
							+ ":" + minute + ":" + second;
					break;
				default:
					break;
			}
			return latestTime;
		}

	},
	// MD5 (Message-Digest Algorithm) http://www.webtoolkit.info/
	md5 : function(string) {

		function RotateLeft(lValue, iShiftBits) {
			return (lValue << iShiftBits) | (lValue >>> (32 - iShiftBits));
		}

		function AddUnsigned(lX, lY) {
			var lX4, lY4, lX8, lY8, lResult;
			lX8 = (lX & 0x80000000);
			lY8 = (lY & 0x80000000);
			lX4 = (lX & 0x40000000);
			lY4 = (lY & 0x40000000);
			lResult = (lX & 0x3FFFFFFF) + (lY & 0x3FFFFFFF);
			if (lX4 & lY4) {
				return (lResult ^ 0x80000000 ^ lX8 ^ lY8);
			}
			if (lX4 | lY4) {
				if (lResult & 0x40000000) {
					return (lResult ^ 0xC0000000 ^ lX8 ^ lY8);
				} else {
					return (lResult ^ 0x40000000 ^ lX8 ^ lY8);
				}
			} else {
				return (lResult ^ lX8 ^ lY8);
			}
		}

		function F(x, y, z) {
			return (x & y) | ((~x) & z);
		}
		function G(x, y, z) {
			return (x & z) | (y & (~z));
		}
		function H(x, y, z) {
			return (x ^ y ^ z);
		}
		function I(x, y, z) {
			return (y ^ (x | (~z)));
		}

		function FF(a, b, c, d, x, s, ac) {
			a = AddUnsigned(a, AddUnsigned(AddUnsigned(F(b, c, d), x), ac));
			return AddUnsigned(RotateLeft(a, s), b);
		};

		function GG(a, b, c, d, x, s, ac) {
			a = AddUnsigned(a, AddUnsigned(AddUnsigned(G(b, c, d), x), ac));
			return AddUnsigned(RotateLeft(a, s), b);
		};

		function HH(a, b, c, d, x, s, ac) {
			a = AddUnsigned(a, AddUnsigned(AddUnsigned(H(b, c, d), x), ac));
			return AddUnsigned(RotateLeft(a, s), b);
		};

		function II(a, b, c, d, x, s, ac) {
			a = AddUnsigned(a, AddUnsigned(AddUnsigned(I(b, c, d), x), ac));
			return AddUnsigned(RotateLeft(a, s), b);
		};

		function ConvertToWordArray(string) {
			var lWordCount;
			var lMessageLength = string.length;
			var lNumberOfWords_temp1 = lMessageLength + 8;
			var lNumberOfWords_temp2 = (lNumberOfWords_temp1 - (lNumberOfWords_temp1 % 64))
					/ 64;
			var lNumberOfWords = (lNumberOfWords_temp2 + 1) * 16;
			var lWordArray = Array(lNumberOfWords - 1);
			var lBytePosition = 0;
			var lByteCount = 0;
			while (lByteCount < lMessageLength) {
				lWordCount = (lByteCount - (lByteCount % 4)) / 4;
				lBytePosition = (lByteCount % 4) * 8;
				lWordArray[lWordCount] = (lWordArray[lWordCount] | (string
						.charCodeAt(lByteCount) << lBytePosition));
				lByteCount++;
			}
			lWordCount = (lByteCount - (lByteCount % 4)) / 4;
			lBytePosition = (lByteCount % 4) * 8;
			lWordArray[lWordCount] = lWordArray[lWordCount]
					| (0x80 << lBytePosition);
			lWordArray[lNumberOfWords - 2] = lMessageLength << 3;
			lWordArray[lNumberOfWords - 1] = lMessageLength >>> 29;
			return lWordArray;
		};

		function WordToHex(lValue) {
			var WordToHexValue = "", WordToHexValue_temp = "", lByte, lCount;
			for (lCount = 0; lCount <= 3; lCount++) {
				lByte = (lValue >>> (lCount * 8)) & 255;
				WordToHexValue_temp = "0" + lByte.toString(16);
				WordToHexValue = WordToHexValue
						+ WordToHexValue_temp.substr(WordToHexValue_temp.length
										- 2, 2);
			}
			return WordToHexValue;
		};

		function Utf8Encode(string) {
			string = string.replace(/\r\n/g, "\n");
			var utftext = "";

			for (var n = 0; n < string.length; n++) {

				var c = string.charCodeAt(n);

				if (c < 128) {
					utftext += String.fromCharCode(c);
				} else if ((c > 127) && (c < 2048)) {
					utftext += String.fromCharCode((c >> 6) | 192);
					utftext += String.fromCharCode((c & 63) | 128);
				} else {
					utftext += String.fromCharCode((c >> 12) | 224);
					utftext += String.fromCharCode(((c >> 6) & 63) | 128);
					utftext += String.fromCharCode((c & 63) | 128);
				}

			}

			return utftext;
		};

		var x = Array();
		var k, AA, BB, CC, DD, a, b, c, d;
		var S11 = 7, S12 = 12, S13 = 17, S14 = 22;
		var S21 = 5, S22 = 9, S23 = 14, S24 = 20;
		var S31 = 4, S32 = 11, S33 = 16, S34 = 23;
		var S41 = 6, S42 = 10, S43 = 15, S44 = 21;

		string = Utf8Encode(string);

		x = ConvertToWordArray(string);

		a = 0x67452301;
		b = 0xEFCDAB89;
		c = 0x98BADCFE;
		d = 0x10325476;

		for (k = 0; k < x.length; k += 16) {
			AA = a;
			BB = b;
			CC = c;
			DD = d;
			a = FF(a, b, c, d, x[k + 0], S11, 0xD76AA478);
			d = FF(d, a, b, c, x[k + 1], S12, 0xE8C7B756);
			c = FF(c, d, a, b, x[k + 2], S13, 0x242070DB);
			b = FF(b, c, d, a, x[k + 3], S14, 0xC1BDCEEE);
			a = FF(a, b, c, d, x[k + 4], S11, 0xF57C0FAF);
			d = FF(d, a, b, c, x[k + 5], S12, 0x4787C62A);
			c = FF(c, d, a, b, x[k + 6], S13, 0xA8304613);
			b = FF(b, c, d, a, x[k + 7], S14, 0xFD469501);
			a = FF(a, b, c, d, x[k + 8], S11, 0x698098D8);
			d = FF(d, a, b, c, x[k + 9], S12, 0x8B44F7AF);
			c = FF(c, d, a, b, x[k + 10], S13, 0xFFFF5BB1);
			b = FF(b, c, d, a, x[k + 11], S14, 0x895CD7BE);
			a = FF(a, b, c, d, x[k + 12], S11, 0x6B901122);
			d = FF(d, a, b, c, x[k + 13], S12, 0xFD987193);
			c = FF(c, d, a, b, x[k + 14], S13, 0xA679438E);
			b = FF(b, c, d, a, x[k + 15], S14, 0x49B40821);
			a = GG(a, b, c, d, x[k + 1], S21, 0xF61E2562);
			d = GG(d, a, b, c, x[k + 6], S22, 0xC040B340);
			c = GG(c, d, a, b, x[k + 11], S23, 0x265E5A51);
			b = GG(b, c, d, a, x[k + 0], S24, 0xE9B6C7AA);
			a = GG(a, b, c, d, x[k + 5], S21, 0xD62F105D);
			d = GG(d, a, b, c, x[k + 10], S22, 0x2441453);
			c = GG(c, d, a, b, x[k + 15], S23, 0xD8A1E681);
			b = GG(b, c, d, a, x[k + 4], S24, 0xE7D3FBC8);
			a = GG(a, b, c, d, x[k + 9], S21, 0x21E1CDE6);
			d = GG(d, a, b, c, x[k + 14], S22, 0xC33707D6);
			c = GG(c, d, a, b, x[k + 3], S23, 0xF4D50D87);
			b = GG(b, c, d, a, x[k + 8], S24, 0x455A14ED);
			a = GG(a, b, c, d, x[k + 13], S21, 0xA9E3E905);
			d = GG(d, a, b, c, x[k + 2], S22, 0xFCEFA3F8);
			c = GG(c, d, a, b, x[k + 7], S23, 0x676F02D9);
			b = GG(b, c, d, a, x[k + 12], S24, 0x8D2A4C8A);
			a = HH(a, b, c, d, x[k + 5], S31, 0xFFFA3942);
			d = HH(d, a, b, c, x[k + 8], S32, 0x8771F681);
			c = HH(c, d, a, b, x[k + 11], S33, 0x6D9D6122);
			b = HH(b, c, d, a, x[k + 14], S34, 0xFDE5380C);
			a = HH(a, b, c, d, x[k + 1], S31, 0xA4BEEA44);
			d = HH(d, a, b, c, x[k + 4], S32, 0x4BDECFA9);
			c = HH(c, d, a, b, x[k + 7], S33, 0xF6BB4B60);
			b = HH(b, c, d, a, x[k + 10], S34, 0xBEBFBC70);
			a = HH(a, b, c, d, x[k + 13], S31, 0x289B7EC6);
			d = HH(d, a, b, c, x[k + 0], S32, 0xEAA127FA);
			c = HH(c, d, a, b, x[k + 3], S33, 0xD4EF3085);
			b = HH(b, c, d, a, x[k + 6], S34, 0x4881D05);
			a = HH(a, b, c, d, x[k + 9], S31, 0xD9D4D039);
			d = HH(d, a, b, c, x[k + 12], S32, 0xE6DB99E5);
			c = HH(c, d, a, b, x[k + 15], S33, 0x1FA27CF8);
			b = HH(b, c, d, a, x[k + 2], S34, 0xC4AC5665);
			a = II(a, b, c, d, x[k + 0], S41, 0xF4292244);
			d = II(d, a, b, c, x[k + 7], S42, 0x432AFF97);
			c = II(c, d, a, b, x[k + 14], S43, 0xAB9423A7);
			b = II(b, c, d, a, x[k + 5], S44, 0xFC93A039);
			a = II(a, b, c, d, x[k + 12], S41, 0x655B59C3);
			d = II(d, a, b, c, x[k + 3], S42, 0x8F0CCC92);
			c = II(c, d, a, b, x[k + 10], S43, 0xFFEFF47D);
			b = II(b, c, d, a, x[k + 1], S44, 0x85845DD1);
			a = II(a, b, c, d, x[k + 8], S41, 0x6FA87E4F);
			d = II(d, a, b, c, x[k + 15], S42, 0xFE2CE6E0);
			c = II(c, d, a, b, x[k + 6], S43, 0xA3014314);
			b = II(b, c, d, a, x[k + 13], S44, 0x4E0811A1);
			a = II(a, b, c, d, x[k + 4], S41, 0xF7537E82);
			d = II(d, a, b, c, x[k + 11], S42, 0xBD3AF235);
			c = II(c, d, a, b, x[k + 2], S43, 0x2AD7D2BB);
			b = II(b, c, d, a, x[k + 9], S44, 0xEB86D391);
			a = AddUnsigned(a, AA);
			b = AddUnsigned(b, BB);
			c = AddUnsigned(c, CC);
			d = AddUnsigned(d, DD);
		}

		var temp = WordToHex(a) + WordToHex(b) + WordToHex(c) + WordToHex(d);

		return temp.toLowerCase();
	},
	cookie : {
		// name=cookie名称，value=cookie值，expire=过期时间，单位秒
		addCookie : function(name, value, expire) { // 添加cookie
			var str = name + "=" + escape(value);
			if (expire > 0) { // 为时不设定过期时间，浏览器关闭时cookie自动消失
				var date = new Date();
				var ms = expire;
				date.setTime(date.getTime() + ms);
				str += "; expires=" + date.toGMTString();
			}
			document.cookie = str;
		},
		// name=cookie名称，value=cookie值
		setCookie : function(name, value)// 两个参数，一个是cookie的名子，一个是值
		{
			var Days = 30; // 此 cookie 将被保存 30 天

			var exp = new Date(); // new Date("December 31, 9998");

			exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);

			document.cookie = name + "=" + escape(value) + ";expires="
					+ exp.toGMTString();
		},
		getCookie : function(name)// 取cookies函数
		{

			var arr = document.cookie.match(new RegExp("(^| )" + name
					+ "=([^;]*)(;|$)"));

			if (arr != null)
				return unescape(arr[2]);
			return null;

		},
		delCookie : function(name)// 删除cookie
		{
			var exp = new Date();

			exp.setTime(exp.getTime() - 1);

			var cval = this.getCookie(name);

			if (cval != null)
				document.cookie = name + "=" + cval + ";expires="
						+ exp.toGMTString();
		}
	},
	// 生成指定范围内的随机数
	random : function(min, max) {
		return Math.floor(min + Math.random() * (max - min));
	},
	// 把图片地址转换成base64字符串，此方法不支持跨域图片
	image2Base64 : function(imgUrl, callback) {
		var canvas = document.createElement('CANVAS');
		var ctx = canvas.getContext('2d');
		var img = new Image;
		img.src = imgUrl;
		// img.crossOrigin = '*';
		img.onload = function() {
			canvas.height = img.height;
			canvas.width = img.width;
			ctx.drawImage(img, 0, 0);
			var dataURL = canvas.toDataURL();
			// Clean up
			canvas = null;
			if (callback) {
				callback(dataURL);
			}
		};
	}
}

var getServerUrl = function() {
	if (LepusUtils.environment == LepusUtils.ENVIRONMENT_LOCAL) {
		return "http://web-test.huawei.com/";
	} else {
		return window.location.protocol + "//" + window.location.host + "/";
	}
};
LepusUtils.serverUrl = getServerUrl(),
// 对接本地调试
$.lepusAjax = function(opts) {
	if (LepusUtils.environment == LepusUtils.ENVIRONMENT_LOCAL) {
		var params;
		if (typeof opts.data == 'object') {
			JSON.stringify(opts.data)
		} else if (opts.data) {
			params = opts.data;
		}
		SDK.Lepus.NET.loadData(opts.url, params, LepusUtils.testCookie,
				opts.type ? opts.type : "get", function(data) {
					// SDK.Lepus.OF.toast("data:" + data);
					if (opts.success) {
						var result = data;
						if (opts.dataType == "json") {
							result = eval('(' + data + ')');
						}
						opts.success(result);
					}
				})
	} else {
		$.ajax(opts);
	}
}
