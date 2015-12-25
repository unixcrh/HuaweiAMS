var ams = {}
var menuUrl = "list/MoreMenu.aspx";

ams.initMenu = function () {
    if (SDK.Lepus.Sys.isLepus()) {
        var menuItem = {
            operateType: SDK.Lepus.More.OPERATE_OPEN_NEW_URL,
            content: applicationRoot + menuUrl,
            icon: "",
            shareType: ""
        };

        var jsonData = JSON.stringify({ menuMore: [menuItem] });

        SDK.Lepus.More.initMore(jsonData, function () {
            return true;
        });
    }
}

function appendTimeOffsetToUrl(url) {
    var offset = jstz.get_date_offset(new Date());

    if (url.indexOf("?") >= 0)
        url += "&";
    else
        url += "?";

    return url + "timeOffset=" + offset;
}

var showBack = function () {
    function init() {
        var backHtml = "<div class=\"back\"></div>" +
            "<div class=\"warm\"><div class='spinnerContainer'><div class='mui-spinner'></div></div> <div class='message'></div> </div>";

        var newNode = document.createElement("div");
        newNode.setAttribute("id", "back");
        newNode.innerHTML = backHtml;
        return newNode;
    }

    var instance;
    return {
        show: function () {
            if (instance) {
                $("#back").show();
            } else {
                instance = init();
                document.body.appendChild(instance);
            }
        }, hide: function () {
            $("#back").hide();
        }, message: function (txt, showSpinner) {
            showBack.show();

            if (showSpinner)
                $("#back").children('.warm').children('.spinnerContainer').show();
            else
                $("#back").children('.warm').children('.spinnerContainer').hide();

            $("#back").children('.warm').children('.message').text(txt);
            $("#back").children('.warm').show();
        },
        error: function (txt) {
            showBack.show();
            $("#back").children('.warm').children('.spinnerContainer').hide();
            $("#back").children('.warm').children('.message').text(txt);
            $("#back").children('.warm').show();

            //显示两秒之后刷新
            setTimeout(function () {
                $("#back").children('.warm').fadeOut('slow', function () {
                    $("#back").hide();
                });
            }, 2000)
        }
    }
}();