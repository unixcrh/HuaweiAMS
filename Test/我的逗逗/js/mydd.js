var language = "cn";
$(document).ready(init);

function init() {
	if (!LepusUtils.DEBUG) {
		SDK.Lepus.DF.stopGravity();
		SDK.Lepus.OF.customerEvent("lepus_operation_shake_viewmydd",
				"viewmydd", null);
		//注册onResume 用于更新统计数据
		SDK.Lepus.Sys.onResume(statisticalData);
		
		//逗逗第1个对应客户端版本不用发秘密和通知
		if (!SDK.Lepus.Version.isAfterShakeVersionOne()) {
			$("#secret").hide();
			$("#notice").hide();
			$("#card").attr("style", "margin-top:4%;");
		}
	}
	
	// getdefaultHisList();
	// 统计数据
	statisticalData();
	language = $("#language").val();
	$("#card").lepusOn("tap", function() {
				clickEvent(2);
			});
	$("#history").lepusOn("tap", function() {
				clickEvent(1);
			});
	$("#notice").lepusOn("tap", function() {
				clickEvent(3);
			});
	// 发秘密
	$("#secret").lepusOn("tap", function() {
				clickEvent(4);
			});
	// 初始化开关
	$("#soundbtn").lepusOn("tap", function() {
				soundswitch();
			});
	switchCss(localStorage.soundswitch == "false" ? false : true);
}

function clickEvent(type) {
	var url, pageName;
	switch (type) {
		case 1 :
			pageName = "historList";
			break;
		case 2 :
			pageName = "cardList";
			break;
		case 3 :
			pageName = "adviseHistorList";
			break;
		case 4 :
			pageName = "secret";
			break;
		default :
			break;
	}
	if (language != "cn") {
		pageName = pageName + "_en";
	}
	url = LepusUtils.serverUrl + "lepusoperation/doudou/" + pageName + ".html";
	if (!LepusUtils.DEBUG) {
		SDK.Lepus.OF.openBrowser(url, SDK.Lepus.OF.BROWSER_SELF, openResult);
	} else {
		window.location.href = url;
	}
}

function soundswitch() {
	var flag = true;
	var value = $("#soundbtn").val();
	if (value == "true") {
		flag = false;
	}
	switchCss(flag);
}
// 开关样式控制
function switchCss(type) {
	if (type) {
		$("#soundimg").attr("src", "img/dou_43.png");
	} else {
		$("#soundimg").attr("src", "img/dou_44.png");
	}
	$("#soundbtn").val(type);
	localStorage.soundswitch = type;
}

// 统计数据
function statisticalData() {
	var tempurl = LepusUtils.serverUrl
			+ "lepusoperation/services/shake/extend/shakestatistic";
	$.lepusAjax({
				type : "GET",
				url : tempurl,
				dataType : "json",
				success : function(resultData) {
					if (LepusUtils.check.checkNull(resultData)
							|| LepusUtils.check.checkNull(resultData.data)) {
						LepusUtils.LOG(jqXHR, "-----服务器获取统计数据为空-----");
						return;
					}
					var resultCode = resultData.resultCode;
					var msg = resultData.msg;
					var result = resultData.data;
					// 通知数量
					var adviseCount = result.adviseCount;
					// 卡包数量
					var prizeCount = result.prizeCount;
					// 页面显示
					if(adviseCount > 0){
						$("#noticeCount").html(adviseCount).show();
					}else {
						$("#noticeCount").hide();
					}
					if(prizeCount > 0){
						$("#cardCount").html(prizeCount).show();
					}else {
						$("#cardCount").hide();
					}
				},
				error : function(jqXHR) {
					LepusUtils.LOG(jqXHR, "-----获取统计数据出错-----");
				}
			});
}

function openResult(result) {
	return;
}

// 默认显示的历史列表
function getdefaultHisList() {
	// 当前页
	var currentPage = 1;

	// 每一页条数
	var pageNum = 3;
	// 摇到的类型：段子/奖券
	var type = 1;
	var resultList;
	var length;
	var dataList;
	var tempurl = LepusUtils.serverUrl
			+ "lepusoperation/services/shake/shakehistory/list/" + pageNum
			+ "/" + currentPage + "?type=" + type;
	$.ajax({
				type : "GET",
				url : tempurl,
				dataType : "json",
				success : function(data) {
					LepusUtils.LOG("我的逗逗-段子历史数据返回");
					dataList = data;
					if (LepusUtils.check.checkNull(dataList)) {
						length = 0;
					} else {
						var resultCode = dataList.resultCode;
						var msg = dataList.msg;
						var resultTemp = dataList.data;
						if (LepusUtils.check.checkNull(resultTemp)) {
							LepusUtils.LOG("段子无历史数据，赶紧摇一摇");
							return;
						}
						resultList = dataList.data.result;
						length = resultList.length;
						if (length > 3) {
							length = 3;
						}
					}

					if (length > 0) {
						for (var i = 0; i < length; i++) {
							var result = resultList[i];
							createHistoryDiv(result, type);
						}
					}
				},
				error : function(jqXHR) {
					LepusUtils.LOG(jqXHR, "获取历史数据失败");
				}
			});
}

function createHistoryDiv(result, type) {

	// 奖品或段子图标
	var icon;
	// 提供者
	var provider;
	// 领取期
	var period;
	// 背景颜色
	var bgColor;
	// 内容
	var content;
	bgColor = result.bgColor;
	content = result.context;
	var iconSrc = result.attachmentId;
	if (type == 1) {
		icon = "img/dou_6.png";
	} else {
		icon = LepusUtils.serverUrl + "lepusoperation/services/shake/image/"
				+ iconSrc;
	}
	period = result.period;
	var time = result.shakeTime;
	shakeTime = LepusUtils.check.localDate(time, 1);
	var title = result.title;
	var id = result.id;

	var hisDiv = document.createElement("div");
	hisDiv.setAttribute("class", "hisDiv");
	hisDiv.setAttribute("id", id);
	var hisImgDiv = document.createElement("div");
	hisImgDiv.setAttribute("class", "hisImgDiv");
	var hisImg = document.createElement("img");
	hisImg.setAttribute("id", "hisImg");
	hisImg.setAttribute("class", "hisImg");
	hisImg.setAttribute("src", icon);
	hisImgDiv.appendChild(hisImg);
	var hisInfoDiv = document.createElement("div");
	hisInfoDiv.setAttribute("class", "hisInfoDiv");
	var hisContent = document.createElement("div");
	hisContent.setAttribute("class", "hisContent");
	hisContent.innerHTML = content;
	var hisTime = document.createElement("div");
	hisTime.setAttribute("class", "hisTime");
	hisTime.innerHTML = shakeTime;
	hisInfoDiv.appendChild(hisContent);
	hisInfoDiv.appendChild(hisTime);
	hisDiv.appendChild(hisImgDiv);
	hisDiv.appendChild(hisInfoDiv);
	$(hisDiv).lepusOn("tap", function(e) {
				lookHisDetail(type, e.currentTarget.id);
			});
	$("#his")[0].appendChild(hisDiv);

	var doudouItem = $(".his-item .hisDiv");
	var divImg = doudouItem.find(".hisImgDiv");
	var height = divImg.height();
	divImg.css("width", height + "px");
	// 设置图片居中
	var iconMage = divImg.find("img");
	var top = (height - iconMage.height()) / 2;
	iconMage.css("margin-top", top + "px");
}

// 单条历史
function lookHisDetail(type, id) {
	LepusUtils.LOG("我的逗逗，打开历史中单个段子详情");
	var url;
	var ticket = language == "cn" ? "ticketDetail" : "ticketDetail_en";
	var episode = language == "cn" ? "episodeDetail" : "episodeDetail_en";
	if (type == 2) {
		url = LepusUtils.serverUrl + "lepusoperation/doudou/" + ticket
				+ ".html?id=" + id;
	} else {
		url = LepusUtils.serverUrl + "lepusoperation/doudou/" + episode
				+ ".html?id=" + id;
	}
	if (!LepusUtils.DEBUG) {
		SDK.Lepus.OF.openBrowser(url, SDK.Lepus.OF.BROWSER_SELF, openHisResult);
	} else {
		window.location.href = url;
	}
}

function openHisResult(result) {
	return;
}
