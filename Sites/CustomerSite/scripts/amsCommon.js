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