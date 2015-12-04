using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Executors
{
    /// <summary>
    /// 编辑一般数据实体的执行器
    /// </summary>
    public class AMSEditEntityExecutor<TEntity> : AMSExecutorBase
    {
        private TEntity _Entity;
        private bool _NeedValidation = true;
        private Action<TEntity> _DataAction = null;

        public AMSEditEntityExecutor(TEntity entity, Action<TEntity> dataAction, AMSOperationType operation)
            : base(operation)
        {
            entity.NullCheck("entity");

            this._Entity = entity;
            this._DataAction = dataAction;
        }

        public TEntity Entity
        {
            get
            {
                return this._Entity;
            }
        }

        public bool NeedValidation
        {
            get
            {
                return this._NeedValidation;
            }
            set
            {
                this._NeedValidation = value;
            }
        }

        protected override object DoOperation(AMSOperationContext context)
        {
            if (this._DataAction != null)
                this._DataAction(this._Entity);

            return this._Entity;
        }

        /// <summary>
        /// 重载了准备数据，进行校验
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareData(AMSOperationContext context)
        {
            base.PrepareData(context);

            this.Validate();
        }

        protected override string GetOperationDescription()
        {
            string opDesp = EnumItemDescriptionAttribute.GetDescription(OperationType);
            string entityDesp = typeof(TEntity).Name;

            DescriptionAttribute despAttr = AttributeHelper.GetCustomAttribute<DescriptionAttribute>(typeof(TEntity));

            if (despAttr != null)
                entityDesp = despAttr.Description;

            return string.Format("{0}-{1}", opDesp, entityDesp);
        }

        protected override void PrepareOperationLog(AMSOperationContext context)
        {
            UserOperationLog log = new UserOperationLog();

            log.Subject = this.GetOperationDescription();
            log.ResourceID = GetLogResourceID();

            log.FillHttpContext();

            context.Logs.Add(log);
        }

        /// <summary>
        /// 验证当前数类型
        /// </summary>
        protected virtual void Validate()
        {
            if (this._NeedValidation)
            {
                ValidationResults validationResults = new ValidationResults();

                DoValidate(validationResults);

                ExceptionHelper.TrueThrow(validationResults.ResultCount > 0, validationResults.ToString());
            }
        }

        /// <summary>
        /// 通常重载此方法来进行校验工作
        /// </summary>
        /// <param name="validationResults"></param>
        protected virtual void DoValidate(ValidationResults validationResults)
        {
            Validator validator = ValidationFactory.CreateValidator(typeof(TEntity));

            ValidationResults innerResults = validator.Validate(this.Entity);

            foreach (ValidationResult innerResult in innerResults)
                validationResults.AddResult(innerResult);
        }

        private string GetLogResourceID()
        {
            StringBuilder strB = new StringBuilder();

            Dictionary<string, object> pairs = ORMapping.GetPrimaryKeyValuePairs(this.Entity);

            foreach (KeyValuePair<string, object> pair in pairs)
                strB.Append(pair.Value);

            return strB.ToString();
        }
    }
}
