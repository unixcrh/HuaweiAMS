using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Validators
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property
        | AttributeTargets.Field,
        AllowMultiple = true,
        Inherited = false)]
    public class DateTimeLessThanCompareValidatorAttribute : ValidatorAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperBoundPropertyName"></param>
        public DateTimeLessThanCompareValidatorAttribute(string upperBoundPropertyName)
        {
            this.UpperBoundPropertyName = upperBoundPropertyName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowerBoundPropertyName"></param>
        /// <param name="upperBoundPropertyName"></param>
        public DateTimeLessThanCompareValidatorAttribute(string lowerBoundPropertyName, string upperBoundPropertyName)
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

        protected override Validator DoCreateValidator(Type targetType)
        {
            return new DateTimeLessThanCompareValidator(this.LowerBoundPropertyName, this.UpperBoundPropertyName, this.MessageTemplate, this.Tag);
        }
    }
}
