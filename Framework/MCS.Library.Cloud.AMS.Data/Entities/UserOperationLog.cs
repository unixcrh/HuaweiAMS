using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace MCS.Library.Cloud.AMS.Data.Entities
{
    [Serializable]
    [DataContract]
    [ORTableMapping("AMS.UserOperationLogs")]
    public class UserOperationLog
    {
        private string _CorrelationID = string.Empty;

        [DataMember]
        [ORFieldMapping("ID", IsIdentity = true, PrimaryKey = true)]
        public long ID
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(0, 36, MessageTemplate = "ResourceID应为Guid的String类型")]
        public string ResourceID
        {
            get; set;
        }

        [DataMember]
        [StringLengthValidator(512, MessageTemplate = "Subject长度不能超过512")]
        public string Subject
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(0, 36, MessageTemplate = "ProcessID应为Guid的String类型")]
        public string ProcessID
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(0, 36, MessageTemplate = "ActivityID应为Guid的String类型")]
        public string ActivityID
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(64, MessageTemplate = "ActivityName长度不能超过64")]
        public string ActivityName
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(64, MessageTemplate = "ApplicationName长度不能超过32")]
        public string ApplicationName
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(64, MessageTemplate = "ProgramName长度不能超过32")]
        public string ProgramName
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(0, 36, MessageTemplate = "CorrelationID应为Guid的String类型")]
        public string CorrelationID
        {
            get
            {
                if (this._CorrelationID.IsNotEmpty())
                {
                    CorrelationManager cm = Trace.CorrelationManager;
                    this._CorrelationID = cm.ActivityId.ToString();
                }
                return this._CorrelationID;
            }
            set
            {
                this._CorrelationID = value;
            }
        }

        [DataMember]
        public string HttpContextInfo
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(32, MessageTemplate = "OperationName长度不能超过32")]
        public string OperationName
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(36, MessageTemplate = "OperatorID长度不能超过32")]
        public string OperatorID
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(64, MessageTemplate = "OperatorName长度不能超过32")]
        public string OperatorName
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(36, MessageTemplate = "RealOperatorID长度不能超过32")]
        public string RealOperatorID
        {
            get;
            set;
        }

        [DataMember]
        [StringLengthValidator(64, MessageTemplate = "RealOperatorName长度不能超过32")]
        public string RealOperatorName
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        public string CreateTime
        {
            get;
            set;
        }

        [DataMember]
        public string Url
        {
            get;
            set;
        }

        [DataMember]
        public string ClientAddress
        {
            get;
            set;
        }

        [DataMember]
        public string ServerAddress
        {
            get;
            set;
        }

        public void FillHttpContext()
        {
            if (EnvironmentHelper.Mode == InstanceMode.Web)
            {
                if (HttpContext.Current != null)
                {
                    HttpRequest request = HttpContext.Current.Request;

                    this.ClientAddress = request.UserHostAddress;
                    this.ServerAddress = request.Headers.GetValue("Host", string.Empty);
                    this.Url = request.Url.ToUriString();

                    this.HttpContextInfo = PrepareHttpContextInfo(request).OuterXml;
                }
            }
        }

        private static XmlDocument PrepareHttpContextInfo(HttpRequest request)
        {
            XmlDocument xmlDoc = XmlHelper.CreateDomDocument("<HttpContext />");
            XmlNode xmlNode = xmlDoc.DocumentElement;

            string userAgent = string.Empty;

            if (request.UserAgent != null)
            {
                userAgent = request.UserAgent;
            }

            XmlHelper.AppendNode(xmlNode, "ContentEncoding", request.ContentEncoding.ToString());
            XmlHelper.AppendNode(xmlNode, "ContentLength", request.ContentLength.ToString());
            XmlHelper.AppendNode(xmlNode, "UserAgent", userAgent);
            XmlHelper.AppendNode(xmlNode, "UserHostName", request.UserHostName);

            return xmlDoc;
        }
    }

    [Serializable]
    [DataContract]
    public class UserOperationLogCollection : EditableDataObjectCollectionBase<UserOperationLog>
    {
    }
}
