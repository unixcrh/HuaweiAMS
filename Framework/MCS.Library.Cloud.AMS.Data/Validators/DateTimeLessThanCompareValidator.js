//注意，此客户端类的名称必须与服务端ClientValidateMethodName相同,默认为类名,且必须包含validate方法
$HGRootNS.ValidatorManager.DateTimeLessThanCompareValidator = function () {
    this.validate = function (cvalue, additionalData, allValues) {
        var isValidate = false;
        var lowerTime = cvalue;
        var upperTime = allValues[additionalData.upperBoundPropertyName];

        if (additionalData.LowerBoundPropertyName)
            lowerTime = allValues[additionalData.lowerBoundPropertyName];

        if (lowerTime != "" && upperTime != "") {
            if (typeof (lowerTime) == "string")
                lowerTime = new Date(lowerTime);

            if (typeof (upperTime) == "string")
                upperTime = new Date(upperTime);

            isValidate = lowerTime < upperTime;
        }

        return isValidate;
    }
};