using MCS.Library.Core;
using MCS.Library.Reflection;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Validators
{
    public class DateTimeLessThanCompareValidator : Validator, IClientValidatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowerBoundPropertyName"></param>
        /// <param name="upperBoundPropertyName"></param>
        /// <param name="messageTemplate"></param>
        /// <param name="tag"></param>
        public DateTimeLessThanCompareValidator(string lowerBoundPropertyName, string upperBoundPropertyName, string messageTemplate, string tag)
            : base(messageTemplate, tag)
        {
            this.LowerBoundPropertyName = lowerBoundPropertyName;
            this.UpperBoundPropertyName = upperBoundPropertyName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LowerBoundPropertyName
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string UpperBoundPropertyName
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToValidate">当前属性值</param>
        /// <param name="currentTarget">当前属性所属的对象实例</param>
        /// <param name="key"></param>
        /// <param name="validationResults"></param>
        protected override void DoValidate(object objectToValidate,
            object currentTarget,
            string key,
            ValidationResults validationResults)
        {
            bool isValid = false;

            if (objectToValidate != null)
            {
                DateTime lowerTime;

                if (this.LowerBoundPropertyName.IsNotEmpty())
                    lowerTime = (DateTime)DynamicPropertyValueAccessor.Instance.GetValue(currentTarget, this.LowerBoundPropertyName);
                else
                    lowerTime = (DateTime)objectToValidate;

                DateTime upperTime = (DateTime)DynamicPropertyValueAccessor.Instance.GetValue(currentTarget, this.UpperBoundPropertyName);

                isValid = lowerTime < upperTime;
            }

            if (isValid == false)
                RecordValidationResult(validationResults, this.MessageTemplate, currentTarget, key);
        }

        public string ClientValidateMethodName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public Dictionary<string, object> GetClientValidateAdditionalData(object info)
        {
            return new Dictionary<string, object>
                    {
                        {"lowerBoundPropertyName", this.LowerBoundPropertyName},
                        {"upperBoundPropertyName", this.UpperBoundPropertyName}
                    };
        }

        public string GetClientValidateScript()
        {
            return ResourceHelper.LoadStringFromResource(Assembly.GetExecutingAssembly(), "MCS.Library.Cloud.AMS.Data.Validators.DateTimeLessThanCompareValidator.js");
        }
    }
}
